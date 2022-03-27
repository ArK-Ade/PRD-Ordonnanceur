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
        private string numberProduct;

        public OF()
        { }

        public OF(int idOF, DateTime starting_hour, int next_step, List<Step> stepSequence, DateTime earliestDate, DateTime latestDate, string numberProduct)
        {
            this.IdOF = idOF;
            this.Starting_hour = starting_hour;
            this.Next_step = next_step;
            this.StepSequence = stepSequence;
            this.EarliestDate = earliestDate;
            this.LatestDate = latestDate;
            this.NumberProduct = numberProduct;
        }

        public OF(OF oF)
        {
            this.IdOF = oF.idOF;
            this.Starting_hour = oF.starting_hour;
            this.Next_step = oF.next_step;
            this.StepSequence = oF.stepSequence;
            this.EarliestDate = oF.earliestDate;
            this.LatestDate = oF.latestDate;
            this.NumberProduct = oF.numberProduct;
        }

        public int IdOF { get => idOF; set => idOF = value; }
        public DateTime Starting_hour { get => starting_hour; set => starting_hour = value; }
        public int Next_step { get => next_step; set => next_step = value; }
        public List<Step> StepSequence { get => stepSequence; set => stepSequence = value; }
        public DateTime EarliestDate { get => earliestDate; set => earliestDate = value; }
        public DateTime LatestDate { get => latestDate; set => latestDate = value; }
        public string NumberProduct { get => numberProduct; set => numberProduct = value; }
    }
}