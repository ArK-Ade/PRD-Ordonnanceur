using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// This class represents the production orders
    /// </summary>
    public class OF
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public OF()
        { }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="idOF"></param>
        /// <param name="starting_hour"></param>
        /// <param name="next_step"></param>
        /// <param name="stepSequence"></param>
        /// <param name="earliestDate"></param>
        /// <param name="latestDate"></param>
        /// <param name="numberProduct"></param>
        public OF(int idOF, DateTime starting_hour, int next_step, List<Step> stepSequence, DateTime earliestDate, DateTime latestDate, string numberProduct)
        {
            this.Uid = idOF;
            this.StartingHour = starting_hour;
            this.NextStep = next_step;
            this.StepSequence = stepSequence;
            this.EarliestDate = earliestDate;
            this.LatestDate = latestDate;
            this.NumberProduct = numberProduct;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="oF">OF to copy</param>
        public OF(OF oF)
        {
            this.Uid = oF.Uid;
            this.StartingHour = oF.StartingHour;
            this.NextStep = oF.NextStep;
            this.StepSequence = oF.StepSequence;
            this.EarliestDate = oF.EarliestDate;
            this.LatestDate = oF.LatestDate;
            this.NumberProduct = oF.NumberProduct;
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartingHour { get; set; }

        /// <summary>
        /// Represent the index of the next step if the OF has already started
        /// </summary>
        public int NextStep { get; set; }

        /// <summary>
        /// List of steps
        /// </summary>
        public List<Step> StepSequence { get; set; }

        /// <summary>
        /// Indicate the earliest date you can start the OF
        /// </summary>
        public DateTime EarliestDate { get; set; }

        /// <summary>
        /// Indicate the latest date you can start the OF
        /// </summary>
        public DateTime LatestDate { get; set; }

        /// <summary>
        /// ID of the product linked to the OF
        /// </summary>
        public string NumberProduct { get; set; }
    }
}