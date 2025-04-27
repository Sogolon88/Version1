namespace FinanceApp.Models;

public partial class ActiviteModele : ContentView
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(ActiviteModele), string.Empty, propertyChanged: OnTitleChanged);
    public static readonly BindableProperty IconProperty =
        BindableProperty.Create(nameof(Icon), typeof(ImageSource), typeof(ActiviteModele), default(ImageSource), propertyChanged: OnIconChanged);
    public ActiviteModele()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ActiviteModele activiteModele)
            activiteModele.TitleLabel.Text = (string)newValue;
    }
    private static void OnIconChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ActiviteModele activiteModele)
            activiteModele.ImageIcon.Source = (ImageSource)newValue;
    }
}