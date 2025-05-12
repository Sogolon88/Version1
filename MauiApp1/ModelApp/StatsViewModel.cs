
namespace MauiApp1.ModelApp
{
    public class StatsViewModel
    {
        public string TotalRevenu { get; set; } = "0";
        public string TotalDepense { get; set; } = "0";
        public string Solde { get; set; } = "0";

        public StatsViewModel(Stats stats)
        {
            TotalRevenu = stats.TotalRevenu.ToString();
            TotalDepense = "-" + stats.TotalDepense.ToString();
            Solde = stats.Solde.ToString();
        }
    }
}
