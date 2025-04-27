using MauiApp1.ViewModels;

namespace MauiApp1.Views;

public partial class NewEpargneViewPage : ContentPage
{
	public NewEpargneViewPage()
	{
		InitializeComponent();
        BindingContext = new NewEpargneModel();

        DateHandler.Date = DateTime.Now;
        DateHandler.MaximumDate = DateTime.Now.AddYears(5);
        DateHandler.MinimumDate = DateTime.Now.AddYears(-5);
    }
}