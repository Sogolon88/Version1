using MauiApp1.ModelApp;

namespace MauiApp1;

public partial class Transaction : ContentPage
{
	public ActionModele? actionModele;
	public Transaction()
	{
		InitializeComponent();
	}
	public Transaction(Revenu r)
	{
        InitializeComponent();
		actionModele.Title = r.Category;
		actionModele.Description = r.Desc;
		actionModele.Somme = r.Budget.ToString();
		TnsctnContener.Add(actionModele);
    }

	private async void OnClickedAddButton(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddPage());
	}
}