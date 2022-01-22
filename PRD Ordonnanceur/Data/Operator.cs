using System;

namespace PRD_Ordonnanceur.Data
{
    class Operator
    {
        private DateTime Beginning { get; set; }

        private DateTime End { get; set; }

        private Object[] Calendar { get; set; }

        public Operator() { }

        public Operator(DateTime beginning, DateTime end)
        {
            this.Beginning = beginning;
            this.End = end;
        }

        public Operator(DateTime beginning, DateTime end, object[] calendar) : this(beginning, end)
        {
            Calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
        }
    }
}