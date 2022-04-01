using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    /// This class represents the consumables used by the OF steps
    /// </summary>
    public class Consumable
    {
        /// <summary>
        /// Constructor by default
        /// </summary>
        public Consumable()
        {
            Calendar = new();
        }

        /// <summary>
        /// Confortable Constructor 
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_quantityAvailable"></param>
        /// <param name="_calendar"></param>
        /// <param name="_delaySupply"></param>
        public Consumable(int _id, double _quantityAvailable, List<Object> _calendar, DateTime _delaySupply)
        {
            Id = _id;
            QuantityAvailable = _quantityAvailable;
            Calendar = _calendar;
            DelaySupply = _delaySupply;
        }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="_quantityAvailable"></param>
        /// <param name="_name"></param>
        /// <param name="_calendar"></param>
        /// <param name="_delaySupply"></param>
        public Consumable(int _id, double _quantityAvailable, string _name, List<Object> _calendar, DateTime _delaySupply)
        {
            Id = _id;
            Name = _name;
            QuantityAvailable = _quantityAvailable;
            Calendar = _calendar;
            DelaySupply = _delaySupply;
        }

        /// <summary>
        /// Unique Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Getter and Setters for QuantityAvailable
        /// </summary>
        public double QuantityAvailable { get; set; }

        /// <summary>
        /// Timetable of unavailability
        /// </summary>
        public List<Object> Calendar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DelaySupply { get; set; }

        /// <summary>
        /// Name of the consumable
        /// </summary>
        public string Name { get; set; }
    }
}