using Microsoft.Maui.Controls;

namespace MauiApp1.ModelApp;

public partial class ActionModele : ContentView
{
	public static readonly BindableProperty TitleProperty =
		BindableProperty.Create(nameof(Title), typeof(string), typeof(ActionModele), string.Empty, propertyChanged:OnTitleChanged);
	public static readonly BindableProperty DescProperty =
		BindableProperty.Create(nameof(Description), typeof(string), typeof(ActionModele), string.Empty, propertyChanged: OnDescChanged);
    public static readonly BindableProperty SommeProperty =
        BindableProperty.Create(nameof(Somme), typeof(string), typeof(ActionModele), string.Empty, propertyChanged: OnSommeChanged);


    public static readonly BindableProperty ImageProperty =
		BindableProperty.Create(nameof(ImageIcon), typeof(ImageSource), typeof(ActionModele), default(ImageSource), propertyChanged: OnIconChanged);

    public string Title
    {
		get => (string)GetValue(TitleProperty);
		set => SetValue(TitleProperty, value);
	}
	public string Description
	{
		get => (string)GetValue(DescProperty);
		set => SetValue(DescProperty, value);
	}
    public string Somme
    {
        get => (string)GetValue(SommeProperty);
        set => SetValue(SommeProperty, value);
    }
    public ImageSource ImageIcon
	{
		get => (ImageSource)GetValue(ImageProperty);
		set => SetValue(ImageProperty, value);
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

	private static void OnDescChanged(BindableObject bind, object oldValue, object newValue)
	{
		if (bind is ActionModele actModele)
			actModele.DescLabel.Text = (string)newValue;
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
}