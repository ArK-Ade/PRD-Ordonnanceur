using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// This class represents the machines used by the operators
    /// </summary>
    public class Machine
    {
        /// <summary>
        /// Constructor by default
        /// </summary>
        public Machine()
        { }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="typeMachine"></param>
        /// <param name="calendar"></param>
        /// <param name="cleaningDuration"></param>
        /// <param name="id"></param>
        public Machine(TypeMachine typeMachine, List<Calendar> calendar, TimeSpan cleaningDuration, int id)
        {
            this.TypeMachine = typeMachine;
            this.Calendar = calendar;
            this.CleaningDuration = cleaningDuration;
            this.Id = id;
        }

        /// <summary>
        /// Timetable of unavailability
        /// </summary>
        public List<Calendar> Calendar { get; set; }

        /// <summary>
        /// Unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Type of Machine
        /// </summary>
        public TypeMachine TypeMachine { get; set; }

        /// <summary>
        /// Time needed to clean the machine
        /// </summary>
        public TimeSpan CleaningDuration { get; set; }
    }
}