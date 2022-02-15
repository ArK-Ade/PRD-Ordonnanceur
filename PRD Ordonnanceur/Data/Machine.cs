using System;

namespace PRD_Ordonnanceur.Data
{
   public enum TypeMachine
    {
        blender,
        Mixer,
        disperser
    };

    public struct Calendar
    {
        private DateTime day;
        private DateTime beginning_hour;
        private DateTime finishing_hour;

        public Calendar(DateTime day, DateTime beginning_hour, DateTime finishing_hour)
        {
            this.day = day;
            this.beginning_hour = beginning_hour;
            this.finishing_hour = finishing_hour;
        }

        public DateTime Day { get => day; set => day = value; }
        public DateTime Beginning_hour { get => beginning_hour; set => beginning_hour = value; }
        public DateTime Finishing_hour { get => finishing_hour; set => finishing_hour = value; }

 
    }

    public class Machine
    {
        private int id;
        private TypeMachine _TypeMachine;
        private Calendar calendar;
        private TimeSpan duration_cleaning;

        public Machine() { }

        public Machine(TypeMachine typeMachine, Calendar calendar, TimeSpan duration_cleaning, int id)
        {
            TypeMachine1 = typeMachine;
            this.calendar = calendar;
            this.Duration_cleaning = duration_cleaning;
            this.Id = id;
        }

        public TypeMachine TypeMachine
        {
            get => TypeMachine1;
            set => TypeMachine1 = value;
        }
        public Calendar Calendar { get => calendar; set => calendar = value; }

        public int Id { get => id; set => id = value; }
        public TypeMachine TypeMachine1 { get => _TypeMachine; set => _TypeMachine = value; }
        public TimeSpan Duration_cleaning { get => duration_cleaning; set => duration_cleaning = value; }
    }
}
