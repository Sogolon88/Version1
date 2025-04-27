using FinanceApp.Services;
using MauiApp1.Views;

namespace FinanceApp
{
    public partial class MainPage : ContentPage
    {
        private readonly AppDatabase db;
        public MainPage()
        {
            db = new AppDatabase();
            InitializeComponent();
            ContentPageHandler.Title = "Bonjour M." + Preferences.Get("CurrentUser", "User Name").ToString().ToUpper();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateStats();
        }

        private async void GoToTransactionPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Transaction());
        }
        private async void GoToAddEpargne(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EpargneViewPage());
        }

        public async void UpdateStats()
        {
            double totalRevenu = 0;
            double totalDepense = 0;

            var transactions = await db.GetAllTransactions();
            foreach (var item in transactions)
            {
                if (item.Type == "Revenu")
                {
                    totalRevenu += item.Budget;
                }
                else
                {
                    totalDepense += item.Budget;
                }
            }
            AfficheRevenu.Text = totalRevenu.ToString();
            AfficheDepense.Text = "-" + totalDepense.ToString();
            AfficheSolde.Text = (totalRevenu - totalDepense).ToString();

            Preferences.Set("totalRevenu", (float)totalRevenu);
            Preferences.Set("totalDepense", (float)totalDepense);
            Preferences.Set("solde", (float)(totalRevenu - totalDepense));

            //double solde = totalRevenu - totalDepense;
            //Stats stats = new(totalRevenu, totalDepense, solde);
            //await db.UpdateStats(stats);
        }
    }
}