using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class Depense (string category, string desc, double budget)
    {
        private string Category { get; set; } = category;
        private string Desc { get; set; } = desc;
        private DateTime date { get; set; } = DateTime.Now;
        private double Budget { get; set; } = budget;      
    }
}
