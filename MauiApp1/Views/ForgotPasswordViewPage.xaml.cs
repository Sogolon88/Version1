using MauiApp1.ViewModels;
namespace MauiApp1.Views;

public partial class ForgotPasswordViewPage : ContentPage
{
    public ForgotPasswordViewPage(ForgotPasswordViewModel fgPasswordViewModel)
    {
        InitializeComponent();
        this.BindingContext = fgPasswordViewModel;
    }
}
