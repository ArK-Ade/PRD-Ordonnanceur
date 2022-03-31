using System;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// Represente the calendar of an operator or a machine for one job
    /// </summary>
    public struct Calendar
    {
        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="day"></param>
        /// <param name="beginning_hour"></param>
        /// <param name="finishing_hour"></param>
        public Calendar(DateTime day, DateTime beginning_hour, DateTime finishing_hour)
        {
            this.Day = day;
            this.BeginningHour = beginning_hour;
            this.FinishingHour = finishing_hour;
        }

        /// <summary>
        /// Represente the day
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// Represente the beginning of the job in the calendar
        /// </summary>
        public DateTime BeginningHour { get; set; }

        /// <summary>
        /// Represente the end of the job in the calendar
        /// </summary>
        public DateTime FinishingHour { get; set; }
    }
}