using FinanceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FinanceApp.Models
{
    public class EpargneDataView
    {
        public string Titre { get; set; } = "Titre";
        public string CurrentAmount { get; set; } = "1200";
        public string Description { get; set; } = "Description";
        public string Montant { get; set; } = "50000";
        public string Progression { get; set; } = "0.3";
        public string Pourcentage { get; set; } = "34%";
        public EpargneDataView(Epargne epargne)
        {
            Titre = epargne.Titre;
            Description = epargne.Description;
            CurrentAmount = epargne.MonatantCourant.ToString() + " MAD";
            Montant = epargne.MontantFinal.ToString() + "MAD";
            Progression = (epargne.MonatantCourant / epargne.MontantFinal).ToString("0.00");
            Pourcentage = epargne.Pourcentage.ToString() + " %";
        }
    }
}
