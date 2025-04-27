using FinanceApp.Models;
using MauiApp1.Views;

namespace FinanceApp
{
    public partial class AddEpargne : ContentPage
    {
        public AddEpargne()
        {
            InitializeComponent();
        }

        private async void OnEnregistrerClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titreAjoutObjectif.Text) ||
                string.IsNullOrWhiteSpace(descriptionAjoutObjectif.Text) ||
                string.IsNullOrWhiteSpace(montantAjoutObjectif.Text) ||
                string.IsNullOrWhiteSpace(pourcentageAjoutObjectif.Text))
            {
                await DisplayAlert("Alert", "Veuillez remplir les champs vide", "Ok");
                return;
            }

            string titre = titreAjoutObjectif.Text;
            string description = descriptionAjoutObjectif.Text;

            if (!double.TryParse(montantAjoutObjectif.Text, out double montant))
            {
                await DisplayAlert("Alert", "Montant invalide !", "OK");
            }

            if (!double.TryParse(pourcentageAjoutObjectif.Text, out double pourcentage))
            {
                await DisplayAlert("Alert", "pourcentage invalide !", "OK");
            }

            var epargne = new Epargne(titreAjoutObjectif.Text, descriptionAjoutObjectif.Text, montant, pourcentage, dateAjoutObjectif.Date);
            await Navigation.PushAsync(new EpargneViewPage());
            Navigation.RemovePage(this);
        }



        private async void OnAnnulerClicked(object sender, EventArgs e)
        {
       

        }
    }
}

