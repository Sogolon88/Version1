using SQLite;

namespace FinanceApp.ModelApp
{
    public class RevenuDataModele
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Category { get; set; }
        public string Desc { get; set; }
        public DateTime EndDate { get; set; }
        public double Budget { get; set; }

    }
}
