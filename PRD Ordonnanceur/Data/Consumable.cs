using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public class Consumable
    {
        private int id;
        private int quantityAvailable;
        private List<Object> calendar;
        private DateTime delaySupply;

        public Consumable()
        {
        }

        public Consumable(int _id, int _quantityAvailable, List<Object> _calendar, DateTime _delaySupply)
        {
            id = _id;
            QuantityAvailable = _quantityAvailable;
            Calendar = _calendar;
            DelaySupply = _delaySupply;
        }

        public int Id { get => id; set => id = value; }
        public int QuantityAvailable { get => quantityAvailable; set => quantityAvailable = value; }
        public List<Object> Calendar { get => calendar; set => calendar = value; }
        public DateTime DelaySupply { get => delaySupply; set => delaySupply = value; }
    }
}