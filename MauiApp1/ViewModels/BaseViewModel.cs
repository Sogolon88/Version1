using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp1.ViewModels
{
    public partial class BaseVieModel : ObservableObject
    {
        [ObservableProperty]
        public string _isBasy;
        [ObservableProperty]
        public string _title;


    }
}
