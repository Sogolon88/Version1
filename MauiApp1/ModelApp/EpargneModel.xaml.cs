
namespace FinanceApp.ModelApp;

public partial class EpargneModel : ContentView
{

	private static readonly BindableProperty TitreProperty =
		BindableProperty.Create(nameof(Titre), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged:OnTitreChanged);
	private static readonly BindableProperty DescriptionProperty =
		BindableProperty.Create(nameof(Description), typeof(string), typeof(string), string.Empty, propertyChanged:OnDescriptionChanged);
    private static readonly BindableProperty PourcentageProperty =
    BindableProperty.Create(nameof(Pourcentage), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged: OnPourcentageChanged);
    private static readonly BindableProperty MontantProperty =
        BindableProperty.Create(nameof(Montant), typeof(string), typeof(EpargneModel), string.Empty, propertyChanged: OnMontantChanged);



    public EpargneModel()
	{
		InitializeComponent();
	}

	public string Titre
	{
		get => (string)GetValue(TitreProperty);
		set => SetValue(TitreProperty, value);
	}

	public string Description
	{
		get => (string)GetValue(DescriptionProperty);
		set => SetValue(DescriptionProperty, value);
	}

	public string Pourcentage
	{
		get => (string)GetValue(PourcentageProperty);
		set => SetValue(PourcentageProperty, value);
	}


    public string Montant
	{
		get => (string)GetValue(MontantProperty);
		set => SetValue(MontantProperty, value);
	}

    private static void OnTitreChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if ( bindable is EpargneModel model)
		{
			model.titreLabel.Text = (string)newValue;
		}
    }
	
	private static void OnDescriptionChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is EpargneModel model)
		{
			model.descriptionLabel.Text = (string)newValue;
		}

	}
	private static void OnMontantChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is EpargneModel model)
		{
			model.montantLabel.Text = (string)newValue;
		}
	}

	private static void OnPourcentageChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is EpargneModel model)
		{
			model.pourcentageLabel.Text = (string)newValue;
		}
	}

}