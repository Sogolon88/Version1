namespace MauiApp1;
using Microsoft.Maui.Controls;

public partial class OtherPage : ContentPage
{
    public OtherPage()
    {
        InitializeComponent();
    }

    private async void GoToTransactionPage(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Transaction());
    }
}