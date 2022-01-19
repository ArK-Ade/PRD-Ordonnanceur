using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Data
{
    class OF
    {

        public string[] step_sequence;

        public DateTime earliest_date;

        public struct Status
        {
            public DateTime starting_hour;
            public string next_step;
        };

        public DateTime latest_date;

        public string number_product;

        public string[][] consommable_quantity;

        public OF() { }

    }
}
