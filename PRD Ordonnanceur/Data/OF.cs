using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public class OF
    {
        private int idOF;
        private DateTime starting_hour;
        private int next_step;
        private List<Step> stepSequence;
        private DateTime earliestDate;
        private DateTime latestDate;
        private List<Tank> setTank;
        private string numberProduct;
        private List<List<string>> consommableQuantity; // TODO surement supprimer cette fonction

        public OF() { }

        public OF(int idOF, DateTime starting_hour, int next_step, List<Step> stepSequence, DateTime earliestDate, DateTime latestDate, List<Tank> setTank, string numberProduct, List<List<string>> consommableQuantity)
        {
            this.IdOF = idOF;
            this.Starting_hour = starting_hour;
            this.Next_step = next_step;
            this.StepSequence = stepSequence;
            this.EarliestDate = earliestDate;
            this.LatestDate = latestDate;
            this.SetTank = setTank;
            this.NumberProduct = numberProduct;
            this.ConsommableQuantity = consommableQuantity;
        }

        public int IdOF { get => idOF; set => idOF = value; }
        public DateTime Starting_hour { get => starting_hour; set => starting_hour = value; }
        public int Next_step { get => next_step; set => next_step = value; }
        public List<Step> StepSequence { get => stepSequence; set => stepSequence = value; }
        public DateTime EarliestDate { get => earliestDate; set => earliestDate = value; }
        public DateTime LatestDate { get => latestDate; set => latestDate = value; }
        public List<Tank> SetTank { get => setTank; set => setTank = value; }
        public string NumberProduct { get => numberProduct; set => numberProduct = value; }
        public List<List<string>> ConsommableQuantity { get => consommableQuantity; set => consommableQuantity = value; }
    }
}
