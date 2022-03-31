using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// This class represents the person who will work on the machines 
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// Constructor by default
        /// </summary>
        public Operator()
        { }

        /// <summary>
        /// Confortable Constructor 
        /// </summary>
        /// <param name="beginning"></param>
        /// <param name="end"></param>
        /// <param name="calendar"></param>
        /// <param name="id"></param>
        /// <param name="machineSkill"></param>
        public Operator(DateTime beginning, DateTime end, List<Calendar> calendar, uint id, List<TypeMachine> machineSkill)
        {
            this.StartWorkSchedule = beginning;
            this.End = end;
            this.Calendar = calendar;
            this.Uid = id;
            this.SkillSet = machineSkill;
        }

        /// <summary>
        /// Indicate the beginning of his daily schedule
        /// </summary>
        public DateTime StartWorkSchedule { get; set; }

        /// <summary>
        /// Indicate the end of his daily schedule
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Calendar> Calendar { get; set; }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public uint Uid { get; set; }

        /// <summary>
        /// Represent his skill set
        /// </summary>
        public List<TypeMachine> SkillSet { get; set; } = new();

        /// <summary>
        /// Method that reset his skill set
        /// </summary>
        public void CleanSkill()
        {
            SkillSet.Clear();
        }

        /// <summary>
        /// Method that add a new skill in his skill set
        /// </summary>
        /// <param name="typeMachine">New skill to add</param>
        public void AddSkill(TypeMachine typeMachine)
        {
            SkillSet.Add(typeMachine);
        }
    }
}