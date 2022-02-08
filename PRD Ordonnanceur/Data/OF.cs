using System;

namespace PRD_Ordonnanceur.Data
{
    public class OF
    {
        private int idOF;
        private DateTime starting_hour;
        private string next_step;
        private Step[] stepSequence; 
        private DateTime earliestDate;
        private DateTime latestDate; 
        private Tank[] setTank; 
        private string numberProduct; 
        private string[][] consommableQuantity;

        public OF() { }

        public OF(int idOF, DateTime starting_hour, string next_step, Step[] stepSequence, DateTime earliestDate, DateTime latestDate, Tank[] setTank, string numberProduct, string[][] consommableQuantity)
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
        public string Next_step { get => next_step; set => next_step = value; }
        public Step[] StepSequence { get => stepSequence; set => stepSequence = value; }
        public DateTime EarliestDate { get => earliestDate; set => earliestDate = value; }
        public DateTime LatestDate { get => latestDate; set => latestDate = value; }
        public Tank[] SetTank { get => setTank; set => setTank = value; }
        public string NumberProduct { get => numberProduct; set => numberProduct = value; }
        public string[][] ConsommableQuantity { get => consommableQuantity; set => consommableQuantity = value; }
    }
}
