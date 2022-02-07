using System;

namespace PRD_Ordonnanceur.Data
{
    public class Operator
    {
        private DateTime beginning;

        private DateTime end;

        private Object[] calendar;

        public Operator() { }

        public Operator(DateTime beginning, DateTime end, object[] calendar)
        {
            this.beginning = beginning;
            this.end = end;
            this.calendar = calendar;
        }

        public DateTime Beginning { get => beginning; set => beginning = value; }
        public DateTime End { get => end; set => end = value; }
        public object[] Calendar { get => calendar; set => calendar = value; }
    }
}