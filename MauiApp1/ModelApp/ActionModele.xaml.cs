using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics.Text;

namespace FinanceApp.ModelApp;

public partial class ActionModele : ContentView
{
	public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), typeof(string), typeof(ActionModele), string.Empty, propertyChanged:OnTitleChanged);
	public static readonly BindableProperty DescriptionProperty =
		BindableProperty.Create(nameof(Description), typeof(string), typeof(ActionModele), string.Empty, propertyChanged:OnDescChanged);
    public static readonly BindableProperty SommeProperty =
        BindableProperty.Create(nameof(Somme), typeof(string), typeof(ActionModele), string.Empty, propertyChanged:OnSommeChanged);
	public static readonly BindableProperty SoldeColorProperty =
		BindableProperty.Create(nameof(SoldeColor), typeof(Color), typeof(ActionModele), default(Color), propertyChanged:OnTextColorsChanged);
    public static readonly BindableProperty ImageIconProperty =
		BindableProperty.Create(nameof(ImageIcon), typeof(ImageSource), typeof(ActionModele), default(ImageSource), propertyChanged: OnIconChanged);
	public static readonly BindableProperty ImageBackColorProperty = BindableProperty.Create(nameof(ImageBorder), typeof(Color), typeof(ActionModele), default(Color), propertyChanged:OnBorderColorChanged);

    public string Title
    {
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}
	public string Description
    {
		get => (string)GetValue(DescriptionProperty);
		set => SetValue(DescriptionProperty, value);
	}
    public string Somme
    {
        get => (string)GetValue(SommeProperty);
        set => SetValue(SommeProperty, value);
    }

	public Color SoldeColor
    {
		get => (Color)GetValue(SoldeColorProperty);
		set => SetValue(SoldeColorProperty, value);
	}
    public ImageSource ImageIcon
	{
		get => (ImageSource)GetValue(ImageIconProperty);
		set => SetValue(ImageIconProperty, value);
	}
	public Color ImageBackColor
    {
        get => (Color)GetValue(ImageBackColorProperty);
        set => SetValue(ImageBackColorProperty, value);
    }

    public ActionModele()
	{
		InitializeComponent();
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ActionModele actionModele)
					actionModele.TitleLabel.Text = (string)newValue;
    }

	private static void OnDescChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is ActionModele actModele)
			actModele.DescriptionLabel.Text = (string)newValue;
	}
    private static void OnSommeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ActionModele actionModele)
            actionModele.SommeLabel.Text = (string)newValue;
    }

    private static void OnIconChanged(BindableObject bind, object oldValue, object newValue)
    {
        if (bind is ActionModele actModele)
            actModele.ImgIcon.Source = (ImageSource)newValue;
    }

	private static void OnTextColorsChanged(BindableObject bind, object oldValue, object newValue)
	{
		if (bind is ActionModele actModele)
			actModele.SommeLabel.TextColor = (Color)newValue;
	}

    private static void OnBorderColorChanged(BindableObject bind, object oldValue, object newValue)
    {
        if (bind is ActionModele actModele)
            actModele.ImageBorder.BackgroundColor = (Color)newValue;
    }
}