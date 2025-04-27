using FinanceApp.Models;
using FinanceApp.Services;
using MauiApp1.ViewModels;
namespace MauiApp1.Views;

public partial class EpargneViewPage : ContentPage
{
    public EpargneViewPage()
    {
        InitializeComponent();
        BindingContext = new EpargneViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if(BindingContext is EpargneViewModel epargneViewModel)
        {
            epargneViewModel.AfficheAllEpargnes();
        }
    }

    private async void EpargneItemTapped(object sender, EventArgs e)
    {
        AppDatabase database = new();
        string command = await DisplayActionSheet("Action", "Annuler", null, "Supprimer", "Modifier");
        if (sender is EpargneModel EModele && EModele.BindingContext is EpargneViewModel epargneViewModel)
        {
            switch (command)
            {
                case "Supprimer":
                    if (await database.DeleteEpargne(epargneViewModel.Id) > 0)
                    {
                        await DisplayAlert("Info", "Epargne Supprimer de la liste", "OK");
                        OnAppearing();
                    }
                    else await DisplayAlert("info", "Echec "+ epargneViewModel.Id.ToString(), "ok");
                    break;
                case "Modifier":
                    break;

                default:
                    break;
            }
        }
        else await DisplayAlert("Erreur", "on ne rentre pas dans la condition", "Ok");
    }
}
