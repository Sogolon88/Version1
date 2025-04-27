using SQLite;

namespace FinanceApp.Models
{
    public class Stats
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public double TotalRevenu { get; set; } = 0;
        public double TotalDepense { get; set; } = 0;
        public double Solde { get; set; } = 0;

        public Stats() { }
        public Stats(double TRevenu, double Tdepense, double solde)
        {
            TotalRevenu = TRevenu;
            TotalDepense = Tdepense;
            Solde = solde;
        }
    }
}
