using System;

namespace PRD_Ordonnanceur.Data
{
    class Machine
    {
        public enum type_machine
        {
            blender,
            Mixer,
            disperser
        };

        public struct calendar
        {
            public DateTime day;
            public DateTime beginning_hour;
            public DateTime finishing_hour;
        }

        public DateTime duration_cleaning { get; set; }

        public Operator[] operator_available_cleaning;


        public Machine() { }

        public Machine(DateTime duration_cleaning, Operator[] operator_available_cleaning)
        {
            this.duration_cleaning = duration_cleaning;
            this.operator_available_cleaning = operator_available_cleaning ?? throw new ArgumentNullException(nameof(operator_available_cleaning));
        }
    }
}
