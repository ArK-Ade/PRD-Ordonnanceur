using System;

namespace PRD_Ordonnanceur.Data
{
    class Consumable
    {
        private int Id { get; set; }

        private int QuantityAvailable { get; set; }

        private Object[] Calendar { get; set; }

        private DateTime DelaySupply { get; set; }

        public Consumable()
        {
        }

        public Consumable(int id, int quantityAvailable, object[] calendar, DateTime delaySupply)
        {
            Id = id;
            QuantityAvailable = quantityAvailable;
            Calendar = calendar ?? throw new ArgumentNullException(nameof(calendar));
            DelaySupply = delaySupply;
        }
    }
}
