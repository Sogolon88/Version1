
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Models;
using FinanceApp.Services;
using Microcharts;
using SkiaSharp;
using SQLite;
using System.Collections.ObjectModel;

namespace FinanceApp.ViewModels
{
    public partial class StatistiquesViewModel : BaseViewModel
    {
        private readonly AppDatabase _databaseService;

        [ObservableProperty]
        private decimal _totalRevenu = 0;

        [ObservableProperty]
        private decimal _totalDepense = 0;

        [ObservableProperty]
        private decimal _solde = 0;



        [ObservableProperty]
        private ObservableCollection<CategoryStats> _depensesParCategorie;

        [ObservableProperty]
        private ObservableCollection<CategoryStats> _revenusParCategorie;

        [ObservableProperty]
        private DateTime _selectedMonth = DateTime.Today;

        [ObservableProperty]
        private ObservableCollection<DataTransaction> _allTransactions;

        [ObservableProperty]
        private ObservableCollection<TransactionViewModel> _recentTransactions;


        [ObservableProperty]
        private Chart _evolutionChart;

        public IAsyncRelayCommand LoadStatisticsCommand { get; }
        public StatistiquesViewModel()
        {
            Title = "Statistiques";
            _databaseService = new AppDatabase();
            _depensesParCategorie = [];
            _revenusParCategorie = [];
            _allTransactions = [];
            _recentTransactions = [];


            LoadStatisticsCommand = new AsyncRelayCommand(LoadStatisticsAsync);


            // Relancer le chargement des statistiques lorsque le mois sélectionné change
            Task.Run(async () => await LoadStatisticsAsync());
        }



        partial void OnSelectedMonthChanged(DateTime value)
        {
            // Relancer le chargement des statistiques lorsque le mois sélectionné change
            Task.Run(async () => await LoadStatisticsAsync());
        }

        private async Task LoadStatisticsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                var allTransactionsData = await _databaseService.GetAllTransactions();
                AllTransactions.Clear();

                foreach (var transaction in allTransactionsData)
                {
                    AllTransactions.Add(transaction);
                }



                var transactionsThisMonth = allTransactionsData
                    .Where(t => t.SaveDate.Year == SelectedMonth.Year && t.SaveDate.Month == SelectedMonth.Month)
                    .ToList();

                if (transactionsThisMonth.Count == 0)
                {
                    Solde = 0;
                    TotalRevenu = 0;
                    TotalDepense = 0;
                    DepensesParCategorie.Clear();
                    RevenusParCategorie.Clear();
                    RecentTransactions.Clear();
                    EvolutionChart = null; // Réinitialiser le graphique si aucune transaction n'est trouvée

                    await Shell.Current.DisplayAlert("Alerte", "Aucune transaction trouvée pour le mois sélectionné.", "OK");
                    return;
                }


                // Calculer les statistiques générales pour le mois sélectionné
                TotalRevenu = (decimal) transactionsThisMonth.Where(t => t.Type == "Revenu").Sum(t => t.Budget);
                TotalDepense = (decimal) transactionsThisMonth.Where(t => t.Type == "Depense").Sum(t => t.Budget);
                Solde = TotalRevenu - TotalDepense;

                // Calculer les dépenses par catégorie pour le mois sélectionné
                var depensesParCategorieGrouped = transactionsThisMonth
                    .Where(t => t.Type == "Depense")
                    .GroupBy(t => t.Category ?? "Autre")
                    .Select(g => new CategoryStats { Category = g.Key, Total = (decimal)(g.Sum(t => t.Budget)) })
                    .OrderByDescending(s => s.Total);

                DepensesParCategorie.Clear();
                foreach (var stat in depensesParCategorieGrouped)
                {
                    DepensesParCategorie.Add(stat);
                }

                // Calculer les revenus par catégorie pour le mois sélectionné
                var revenusParCategorieGrouped = transactionsThisMonth
                    .Where(t => t.Type == "Revenu")
                    .GroupBy(t => t.Category ?? "Autre")
                    .Select(g => new CategoryStats { Category = g.Key, Total =(decimal)( g.Sum(t => t.Budget) )})
                    .OrderByDescending(s => s.Total);

                RevenusParCategorie.Clear();
                foreach (var stat in revenusParCategorieGrouped)
                {
                    RevenusParCategorie.Add(stat);
                }

                RecentTransactions.Clear();
                // Récupérer les 5 dernières transactions
                var recentTransactions = allTransactionsData
                    .OrderByDescending(t => t.SaveDate)
                    .Take(Range.All)
                    .ToList();
                foreach (var transaction in recentTransactions)
                {
                    var transactionViewModel = new TransactionViewModel(transaction);
                    RecentTransactions.Add(transactionViewModel);
                }



                // Générer les données pour la courbe d'évolution du solde
                GenerateBalanceEvolutionChart([.. allTransactionsData.OrderBy(t => t.SaveDate)]);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur lors du chargement des statistiques: {ex.Message}");
                await Shell.Current.DisplayAlert("Erreur", "Impossible de charger les statistiques.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }





        private void GenerateBalanceEvolutionChart(List<DataTransaction> allTransactions)
        {
            var balanceHistory = new ObservableCollection<BalancePoint>();
            decimal currentBalance = 0;

            // Filtrer les transactions pour le mois sélectionné et ordonner par date
            var transactionsThisMonth = allTransactions
                .Where(t => t.SaveDate.Year == SelectedMonth.Year && t.SaveDate.Month == SelectedMonth.Month)
                .OrderBy(t => t.SaveDate);

            foreach (var transaction in transactionsThisMonth)
            {
                if (transaction.Type == "Revenu")
                {
                    currentBalance += (decimal) transaction.Budget;
                }
                else if (transaction.Type == "Depense")
                {
                    currentBalance -= (decimal) transaction.Budget;
                }
                balanceHistory.Add(new BalancePoint { Date = transaction.SaveDate, Balance = currentBalance });
            }

            // Créer les entrées pour Microcharts
            var entries = balanceHistory.Select(bp => new ChartEntry((float)bp.Balance)
            {
                Label = bp.Date.ToString("dd"),
                ValueLabel = $"{bp.Balance:N2}",
                Color = bp.Balance >= 0 ? SKColors.Green : SKColors.Red
            }).ToList();

            EvolutionChart = new LineChart { Entries = entries };
        }
    }

    public class CategoryStats
    {
        public string Category { get; set; }
        public decimal Total { get; set; }
    }

    public class BalancePoint
    {
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }

}



