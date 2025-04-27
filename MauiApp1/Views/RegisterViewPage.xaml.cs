using MauiApp1.ViewModels;
namespace MauiApp1.Views;


public partial class RegisterViewPage : ContentPage
{
    public RegisterViewPage(RegisterViewModel RviewModele)
    {
        InitializeComponent();
        this.BindingContext = RviewModele;
    }
}