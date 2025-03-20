using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Revenu(string category, string desc, DateTime dt, double budget)
    {
        public string Category { get; set; } = category;
        public string Desc { get; set; } = desc;
        public DateTime EndDate { get; set; } = dt;
        public double Budget { get; set; } = budget;
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}
