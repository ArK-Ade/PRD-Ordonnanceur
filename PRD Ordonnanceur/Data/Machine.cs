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
        public DateTime day;
        public DateTime beginning_hour;
        public DateTime finishing_hour;
    }

    public class Machine
    {
        private int id;
        private TypeMachine _TypeMachine;
        private Calendar calendar;
        private DateTime duration_cleaning;
        private Operator[] operator_available_cleaning;

        public Machine() { }

        public Machine(TypeMachine typeMachine, Calendar calendar, DateTime duration_cleaning, Operator[] operator_available_cleaning, int id)
        {
            TypeMachine1 = typeMachine;
            this.calendar = calendar;
            this.duration_cleaning = duration_cleaning;
            this.operator_available_cleaning = operator_available_cleaning;
            this.Id = id;
        }

        public TypeMachine TypeMachine
        {
            get => TypeMachine1;
            set => TypeMachine1 = value;
        }
        public Calendar Calendar { get => calendar; set => calendar = value; }
        public DateTime Duration_cleaning { get => duration_cleaning; set => duration_cleaning = value; }
        public Operator[] Operator_available_cleaning { get => operator_available_cleaning; set => operator_available_cleaning = value; }
        public int Id { get => id; set => id = value; }
        public TypeMachine TypeMachine1 { get => _TypeMachine; set => _TypeMachine = value; }
    }
}
