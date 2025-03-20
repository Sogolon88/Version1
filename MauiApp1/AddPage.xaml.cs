using System.Threading.Tasks;

namespace MauiApp1;

public partial class AddPage : ContentPage
{
    private string StrCategory { get; set; }
    private string StrDesc { get; set; }
    private double Budget { get; set; }
    private DateTime EndDate { get; set; }
    
    public static string[] allCategory = {"Salaire", "Famille", "Achat", "Vente", "Autre", "Transport", "Traitement"};

    public AddPage()
	{
		InitializeComponent();

		DateSetter.Date = DateTime.Now;
        DateSetter.MinimumDate = DateTime.Now;
		DateSetter.MaximumDate = DateTime.Now.AddYears(1);
    }


    public async void OnClickAddButton(object sender, EventArgs e)
	{
        if (string.IsNullOrWhiteSpace(CategoryLabel.Text) ||
            string.IsNullOrWhiteSpace(DescLabel.Text)     ||
            string.IsNullOrWhiteSpace(SoldeLabel.Text)    ||
            string.IsNullOrWhiteSpace(DescLabel.Text)
            )
        {
            await DisplayAlert("Alert", "Champs Vide", "OK");
        }

        StrCategory = CategoryLabel.Text;
        StrDesc = DescLabel.Text;
        Budget = Double.Parse(SoldeLabel.Text);
        EndDate = DateSetter.Date;

        if (RBrevenu.IsChecked)
            AddRevenu(new Revenu(StrCategory, StrDesc, EndDate, Budget));
        else
            AddDepense(new Depense(StrCategory, StrDesc, Budget));
    }

    public async void OnClickedCategoryAddButton(object sender, EventArgs e)
    {
        string CategoryChosed = await DisplayActionSheet("Category", "Annuler", null, allCategory);
        CategoryLabel.Text = CategoryChosed;
    }

    private async Task AddDepense(Depense depense)
    {
        await DisplayAlert("confimation", "Ajour d'une nouvelle depense !", "OK");
        //await Navigation.PushAsync(new Transaction(depense));
    }

    public async void AddRevenu(Revenu revenu)
    {
        await DisplayAlert("confimation", "Ajout d'un nouveau revenu !", "OK");
        await Navigation.PopAsync(new Transaction(revenu));
    }


}