using FinanceApp.Models;
namespace FinanceApp;

public partial class AllEpargne : ContentPage
{
    public AllEpargne(Epargne epargne)
    {
        InitializeComponent();
        BindingContext = this;
    }
}