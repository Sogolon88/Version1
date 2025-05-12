using FinanceApp.ViewModels;
namespace FinanceApp.Views;

public partial class DashBoardViewPage : ContentPage
{
	public DashBoardViewPage()
	{
		InitializeComponent();
		this.BindingContext = new DashBoardViewModel();
	}
}