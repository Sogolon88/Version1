using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using FinanceApp.Models;
using MauiApp1.Views;
using FinanceApp;
using FinanceApp.Services;

namespace MauiApp1.ViewModels
{
    public partial class NewEpargneModel
    {
        public string TitreText { get; set; } = string.Empty;
        public string DescText { get; set; } = string.Empty;
        public string AmountText { get; set; } = string.Empty;
        public string Taux { get; set; } = string.Empty;
        public DateTime DateSetter { get; set; }
        private AppDatabase? Database;
        public NewEpargneModel()
        {
            Database = new AppDatabase();
        }
        public NewEpargneModel(string titre, string description, double montant, double pourcentage, DateTime dateAjout)
        {

        }

        [RelayCommand]
        public async Task OnclickCancelButton()
        {
            TitreText = string.Empty;
            DescText = string.Empty;
            AmountText = string.Empty;
            Taux = string.Empty;
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }

        [RelayCommand]
        public async Task ClickSaveButton()
        {
            if (string.IsNullOrWhiteSpace(TitreText) || string.IsNullOrWhiteSpace(AmountText)
                || string.IsNullOrWhiteSpace(DescText))
            {
                await Shell.Current.DisplayAlert("Alert", "Veuillez remplir les champs vide", "Ok");
                return;
            }
            if (!double.TryParse(AmountText, out double montant))
            {
                await Shell.Current.DisplayAlert("Alert", "Montant invalide !", "OK");
            }
            if (!double.TryParse(Taux, out double pourcentage) || pourcentage > 100)
            {
                await Shell.Current.DisplayAlert("Alert", "pourcentage invalide !", "OK");
            }
            var epargne = new Epargne(TitreText, DescText, montant, pourcentage, DateSetter.Date);
            if(Database != null)
            {
                var epargnes = await Database.GetAllEpargnes();
                double totalTaux = 0;
                foreach(var item in epargnes)
                {
                    
                    totalTaux += item.Pourcentage;
                }

                if ((totalTaux + epargne.Pourcentage) > 100.0)
                {
                    epargne.Pourcentage = 100.0 - totalTaux;
                }
                else if( (totalTaux + epargne.Pourcentage) < 100.0)
                {
                    epargne.Pourcentage += 100.0 - (totalTaux + epargne.Pourcentage);
                }
                else await Shell.Current.DisplayAlert("Alert", "Taux tres grand !", "Ok");

                if (await Database.AddEpargne(epargne) > 0)
                {
                    await Shell.Current.DisplayAlert("Alert", "Epargne ajoutée avec succès !", "Ok");
                    await Shell.Current.GoToAsync($"//{nameof(EpargneViewPage)}");
                }
                else await Shell.Current.DisplayAlert("Alert", "Echec, reseillez plus tard !!!", "OK");
            }
        }
    }

}
