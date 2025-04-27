namespace MauiApp1.Views;
using MauiApp1.ViewModels;

public partial class LoginViewPage : ContentPage
{
    public LoginViewPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        this.BindingContext = loginViewModel;
    }
}