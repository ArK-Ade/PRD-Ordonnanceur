using System;

namespace PRD_Ordonnanceur.Data
{
    public struct Status
    {
        public DateTime starting_hour { get; set; }
        public string next_step { get; set; }
    };

    // TODO Changer les accesseurs get et set de toutes les classes
    public class OF
    {
        private int idOF;

        private Status status;

        private string[] stepSequence; 

        private DateTime earliestDate;

        private DateTime latestDate; 

        private Tank[] setTank; 

        private string numberProduct; 

        private string[][] consommableQuantity;

        public OF() { }

        public OF(int idOF, Status status, string[] stepSequence, DateTime earliestDate, DateTime latestDate, Tank[] setTank, string numberProduct, string[][] consommableQuantity)
        {
            this.idOF = idOF;
            this.status = status;
            this.StepSequence = stepSequence;
            this.EarliestDate = earliestDate;
            this.LatestDate = latestDate;
            this.SetTank = setTank;
            this.NumberProduct = numberProduct;
            this.ConsommableQuantity = consommableQuantity;
        }

        public int IdOF { get => idOF; set => idOF = value; }
        public Status Status { get => status; set => status = value; }
        public string[] StepSequence { get => stepSequence; set => stepSequence = value; }
        public DateTime EarliestDate { get => earliestDate; set => earliestDate = value; }
        public DateTime LatestDate { get => latestDate; set => latestDate = value; }
        public string NumberProduct { get => numberProduct; set => numberProduct = value; }
        public string[][] ConsommableQuantity { get => consommableQuantity; set => consommableQuantity = value; }
        internal Tank[] SetTank { get => setTank; set => setTank = value; }
    }
}
