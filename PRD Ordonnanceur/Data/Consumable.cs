using System;

namespace PRD_Ordonnanceur.Data
{
    public class Consumable
    {
        private int id;

        private int quantityAvailable;

        private Object[] calendar;

        private DateTime delaySupply;

        public Consumable()
        {
        }

        public Consumable(int _id, int _quantityAvailable, object[] _calendar, DateTime _delaySupply)
        {
            id = _id;
            QuantityAvailable = _quantityAvailable;
            Calendar = _calendar ?? throw new ArgumentNullException(nameof(_calendar));
            DelaySupply = _delaySupply;
        }

        public int Id { get => id; set => id = value; }
        public int QuantityAvailable { get => quantityAvailable; set => quantityAvailable = value; }
        public object[] Calendar { get => calendar; set => calendar = value; }
        public DateTime DelaySupply { get => delaySupply; set => delaySupply = value; }
    }
}
