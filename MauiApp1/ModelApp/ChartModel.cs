using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ModelApp
{
    public class ChartModel
    {
        public ObservableCollection<ChartData> data { get; set; }
        public ChartModel()
        {
            data = [
                new("Depenses",45),
                new("Revenus",55)
            ];
        }
    }
    public class ChartData
    {
        public string CategoryName { get; set; }
        public double ValueCategory { get; set; }

        public ChartData(string categoryName, double valueCategory )
        {
            this.CategoryName = categoryName;
            this.ValueCategory = valueCategory;
        }
    }
}
