using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public class Operator
    {
        private uint id;
        List<TypeMachine> machineSkill;
        private DateTime beginning;
        private DateTime end;
        private List<Object> calendar;

        public Operator() { }

        public Operator(DateTime beginning, DateTime end, List<Object> calendar, uint id, List<TypeMachine> machineSkill)
        {
            this.beginning = beginning;
            this.end = end;
            this.calendar = calendar;
            this.Id = id;
            this.MachineSkill = machineSkill;
        }

        public DateTime Beginning { get => beginning; set => beginning = value; }
        public DateTime End { get => end; set => end = value; }
        public List<Object> Calendar { get => calendar; set => calendar = value; }
        public uint Id { get => id; set => id = value; }
        public List<TypeMachine> MachineSkill { get => machineSkill; set => machineSkill = value; }
    }
}