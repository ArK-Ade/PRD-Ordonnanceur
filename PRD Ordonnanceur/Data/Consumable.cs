using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Data
{
    class Consumable
    {
        public string name;

        public int quantity;

        public DateTime[] calendar;

        public DateTime delay_supply;

        public Consumable()
        {
        }
    }
}
