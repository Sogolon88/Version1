using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceApp.Models;
using FinanceApp.Views;
using FinanceApp.Services;
using System.Collections.ObjectModel;
using Microcharts;
using SkiaSharp;

namespace FinanceApp.ViewModels
{
    public partial class DashBoardViewModel : BaseViewModel
    {
        private readonly AppDatabase _databaseService;

        [ObservableProperty]
        private string _userName = "User"; // Default or load from Prefs/DB

        [ObservableProperty]
        private string _userImage = "dash.jpg";

        [ObservableProperty]
        private string _solde = "0.00 MAD"; // Formatted balance

        [ObservableProperty]
        private string _totalRevenuMonth = "0.00 MAD";

        [ObservableProperty]
        private string _totalDepenseMonth = "0.00 MAD";

        [ObservableProperty]
        private DateTime _selectedDate = DateTime.Today; // Bind to DatePicker

        [ObservableProperty]
        private ObservableCollection<TransactionViewModel> _recentTransactions;

        [ObservableProperty]
        private ObservableCollection<EpargneViewModel> _savingsGoalsSummary; // Or a simplified VM

        [ObservableProperty]
        private Chart chartDataIncomeExpense; // For Microcharts

        [ObservableProperty]
        private Chart chartDataIncomeRevenu;

        [ObservableProperty]
        private Chart _chartDataGoals; // For Microcharts

        public IAsyncRelayCommand LoadDataCommand { get; }

        public DashBoardViewModel()
        {
            Title = "Dashboard"; // From BaseViewModel
            _databaseService = new AppDatabase();
            _recentTransactions = new ObservableCollection<TransactionViewModel>();
            _savingsGoalsSummary = new ObservableCollection<EpargneViewModel>();

            LoadDataCommand = new AsyncRelayCommand(LoadDashboardDataAsync);

            // Initialize charts (optional, can be done in LoadDashboardDataAsync)
            chartDataIncomeExpense = new Microcharts.PieChart { Entries = new List<ChartEntry>() };
            chartDataIncomeRevenu = new Microcharts.PieChart { Entries = new List<ChartEntry>() };
            _chartDataGoals = new BarChart { Entries = new List<ChartEntry>() };

            // Load initial data
            Task.Run(async () => await LoadDashboardDataAsync()); // Or use a page appearing event command
        }

        //mettre a jour le dashboard lorsque la date change
        partial void OnSelectedDateChanged(DateTime value)
        {
            // Reload data when date changes
            Task.Run(async () => await LoadDashboardDataAsync());
        }

        private async Task LoadDashboardDataAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                // --- Load User Name ---
                UserName = Preferences.Get("CurrentUser", "User");
                // Maybe load image path too if dynamic


                // --- Load Transactions for Selected Month  ---
                var allTransactions = await _databaseService.GetAllTransactions();
                var transactionsThisMonth = allTransactions
                    .Where(t => t.SaveDate.Year == SelectedDate.Year && t.SaveDate.Month == SelectedDate.Month || t.SaveDate.Day == SelectedDate.Day)
                    .OrderByDescending(t => t.SaveDate)
                    .ToList();


                //var transactionsThisDayOfMonth = allTransactions
                //    .Where(d => d.SaveDate.Year == SelectedDate.Year && d.SaveDate.Month == SelectedDate.Month && d.SaveDate.Day == SelectedDate.Day)
                //    .OrderByDescending(d => d.SaveDate)
                //    .ToList();

                // --- Calculate Stats for Selected Month ---
                decimal income = transactionsThisMonth.Any() ? (decimal) transactionsThisMonth.Where(t => t.Type == "Revenu").Sum(t => t.Budget) : 0;
                decimal expense = (decimal) transactionsThisMonth.Where(t => t.Type == "Depense").Sum(t => t.Budget);
                TotalRevenuMonth = $"{income:N2} MAD";
                TotalDepenseMonth = $"{expense:N2} MAD";// TransactionViewModel already formats with +/-


                // --- Calculate Overall Balance (Solde) ---

                decimal totalIncomeAll = (decimal) allTransactions.Where(t => t.Type == "Revenu").Sum(t => t.Budget);
                decimal totalExpenseAll = (decimal) allTransactions.Where(t => t.Type == "Depense").Sum(t => t.Budget);
                decimal currentBalance = totalIncomeAll - totalExpenseAll;
                Solde = $"{currentBalance:N2} MAD";


                // --- Populate Recent Transactions ---
                RecentTransactions.Clear();
                var recent = transactionsThisMonth.Take(5); // Show last 5 for the month
                foreach (var transaction in recent)
                {
                    RecentTransactions.Add(new TransactionViewModel(transaction));
                }




                // --- Populate Savings Goals ---
                var allEpargnes = await _databaseService.GetAllEpargnes();
                SavingsGoalsSummary.Clear();
                foreach (var epargne in allEpargnes.Take(5)) // Show top 5 goals maybe
                {
                    // Ensure EpargneViewModel is accessible
                    SavingsGoalsSummary.Add(new EpargneViewModel(
                        epargne.Id,
                        epargne.Titre,
                        epargne.Description,
                        (double)(epargne.MontantFinal > 0 ? epargne.MonatantCourant / epargne.MontantFinal : 0), // Fix potential divide by zero
                        $"{epargne.MontantFinal:N2} MAD",
                         epargne.MonatantCourant >= epargne.MontantFinal ? "Terminé" : $"{epargne.MonatantCourant:N2} MAD",
                        $"{epargne.Pourcentage}%"
                    ));
                }


                // --- Prepare Chart Data (Income/Expense Pie Chart) ---
                var expenseCategories = transactionsThisMonth
                    .Where(t => t.Type == "Depense")
                    .GroupBy(t => t.Category ?? "Autre")
                    .Select(g => new ChartEntry((float)g.Sum(t => t.Budget))
                    {
                        Label = g.Key,
                        ValueLabel = $"{g.Sum(t => t.Budget):N0}", // Format value shown on slice
                        Color = SKColor.Parse(GetRandomColor())
                    })
                    .ToList();

                ChartDataIncomeExpense = new Microcharts.PieChart
                {
                    Entries = expenseCategories,
                    LabelTextSize = 25f, // Example size
                    BackgroundColor = SKColors.Transparent,
                    LabelMode = LabelMode.RightOnly // Or other modes
                };

                var revenuCategories = transactionsThisMonth
                   .Where(t => t.Type == "Revenu")
                   .GroupBy(t => t.Category ?? "Autre")
                   .Select(g => new ChartEntry((float)g.Sum(t => t.Budget))
                   {
                       Label = g.Key,
                       ValueLabel = $"{g.Sum(t => t.Budget):N0}",
                       Color = SKColor.Parse(GetRandomColor())
                   })
                   .ToList();




                ChartDataIncomeRevenu = new Microcharts.PieChart
                {
                    Entries = revenuCategories,
                    LabelTextSize = 25f, // Example size
                    BackgroundColor = SKColors.Transparent,
                    LabelMode = LabelMode.RightOnly // Or other modes
                };

                // --- Prepare Chart Data (Goals Bar Chart) ---
                var goalEntries = allEpargnes.Select(e => new ChartEntry((float)e.MonatantCourant)
                {
                    Label = e.Titre,
                    ValueLabel = $"{(e.MontantFinal > 0 ? (e.MonatantCourant / e.MontantFinal) * 100 : 0):N0}%", // Show percentage progress
                    Color = SKColor.Parse(GetRandomColor()), // Helper needed
                    ValueLabelColor = SKColors.Black
                }).ToList();

                ChartDataGoals = new BarChart
                {
                    Entries = goalEntries,
                    LabelTextSize = 25f,
                    BackgroundColor = SKColors.Transparent,
                    ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                    LabelOrientation = Microcharts.Orientation.Horizontal
                };



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading dashboard data: {ex.Message}");
                await Shell.Current.DisplayAlert("Erreur", "Impossible de charger les données du tableau de bord.", "OK");
            }
            finally
            {
                IsBusy = false;

            }
        }

        // Helper to get different colors for chart slices/bars
        private readonly Random _random = new Random();
        private string GetRandomColor()
        {
            return $"#{_random.Next(0x1000000):X6}";
        }

        [RelayCommand]
        private async Task GoToAddNewTransaction()
        {
            //await Shell.Current.DisplayAlert("Navigation", "Go to Add Transaction", "OK"); // Placeholder
            await Shell.Current.GoToAsync($"//{nameof(AddPage)}");
        }

        [RelayCommand]
        private async Task GoToSavings()
        {
            // Replace SavingsPageName with the actual page name
            await Shell.Current.GoToAsync($"//{nameof(EpargneViewPage)}"); 


        }

        [RelayCommand]
        private async Task GoToSettings()
        {
            // Replace SettingsPageName with the actual page name

            await Shell.Current.DisplayAlert("Navigation", "Go to Settings", "OK"); // Placeholder
            await Shell.Current.GoToAsync($"//{nameof(SettingViewPage)}");
        }

        [RelayCommand]
        private async Task GoToAllTransactions()
        {
            // Replace AllTransactionsPageName with the actual page name
            await Shell.Current.DisplayAlert("Navigation", "Go to All Transactions", "OK");
            await Shell.Current.GoToAsync($"//{nameof(StatistiquesViewPage)}");

        }




        [RelayCommand]
        private async Task GoToAllStats()
        {
            // Replace AllStatsPageName with the actual page name
            await Shell.Current.GoToAsync($"//{nameof(StatistiquesViewPage)}");
            await Shell.Current.DisplayAlert("Navigation", "Go to All Stats", "OK"); // Placeholder
        }

        [RelayCommand]
        private async Task ClickAddNewEpargne()
        {             // Replace AddEpargnePageName with the actual page name
            await Shell.Current.GoToAsync($"//{nameof(NewEpargneViewPage)}");
            await Shell.Current.DisplayAlert("Navigation", "Go to Add Epargne", "OK"); // Placeholder
        }

        [RelayCommand]
        private async Task BackButton()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}