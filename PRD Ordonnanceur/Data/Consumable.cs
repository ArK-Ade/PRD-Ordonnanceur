using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public class Consumable
    {
        private int id;
        private string name;
        private double quantityAvailable;
        private List<Object> calendar;
        private DateTime delaySupply;

        public Consumable()
        {
        }

        public Consumable(int _id, double _quantityAvailable, List<Object> _calendar, DateTime _delaySupply)
        {
            id = _id;
            QuantityAvailable = _quantityAvailable;
            Calendar = _calendar;
            DelaySupply = _delaySupply;
        }

        public Consumable(int _id, double _quantityAvailable, string _name, List<Object> _calendar, DateTime _delaySupply)
        {
            id = _id;
            Name = _name;
            QuantityAvailable = _quantityAvailable;
            Calendar = _calendar;
            DelaySupply = _delaySupply;
        }

        public int Id { get => id; set => id = value; }
        public double QuantityAvailable { get => quantityAvailable; set => quantityAvailable = value; }
        public List<Object> Calendar { get => calendar; set => calendar = value; }
        public DateTime DelaySupply { get => delaySupply; set => delaySupply = value; }
        public string Name { get => name; set => name = value; }
    }
}