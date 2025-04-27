using SQLite;

namespace FinanceApp.Models
{
    public class Epargne
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public double MontantFinal { get; set; }
        public double Pourcentage { get; set; }
        public double MonatantCourant { get; set; }
        public DateTime SaveDate { get; set; }
        public DateTime EndDate { get; set; }

        public Epargne()
        {
        }

        public Epargne(string titre, string description, double montant, double pourcentage, DateTime DT)
        {
            Titre = titre;
            MontantFinal = montant;
            Description = description;
            Pourcentage = pourcentage;
            MonatantCourant = 0;
            EndDate = DT;
            SaveDate = DateTime.Now;
        }
    }
}
