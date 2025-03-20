using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        string? translatedNumber;

        private void OnTranslate(Object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = MauiApp1.PhoneWordsTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "Call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "Call";
            }
        }

        async void OnCall(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                   "Dial a Number",
                   "Would you like to call " + translatedNumber + "?",
                   "Yes",
                   "No"))
            {
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
        private async void OnGoToOtherPage(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new OtherPage());
        }
    }
}