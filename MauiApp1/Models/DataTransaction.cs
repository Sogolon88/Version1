using SQLite;

namespace FinanceApp.Models
{
    public class DataTransaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Type { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime SaveDate { get; set; }
        public double Budget { get; set; }

        public DataTransaction()
        {
        }
        public DataTransaction(string category, string desc, DateTime dt, double budget)
        {
            EndDate = dt;
            Budget = budget;
            Category = category;
            Type = string.Empty;
            SaveDate = DateTime.Now;
            Description = desc;
        }
    }
}
