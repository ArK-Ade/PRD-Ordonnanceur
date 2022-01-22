using System;

namespace PRD_Ordonnanceur.Data
{
    class OF
    {
        private int Id { get; set; }

        public string[] StepSequence { get; set; }

        public DateTime EarliestDate { get; set; }

        public struct Status
        {
            public DateTime starting_hour { get; set; }
            public string next_step { get; set; }
        };

        public DateTime LatestDate { get; set; }

        public Tank[] set_tank { get; set; }

        public string number_product { get; set; }

        public string[][] consommable_quantity { get; set; }

        public OF() { }

        public OF(int id, string[] stepSequence, DateTime earliestDate, DateTime latestDate, Tank[] set_tank, string number_product, string[][] consommable_quantity)
        {
            Id = id;
            StepSequence = stepSequence ?? throw new ArgumentNullException(nameof(stepSequence));
            EarliestDate = earliestDate;
            LatestDate = latestDate;
            this.set_tank = set_tank ?? throw new ArgumentNullException(nameof(set_tank));
            this.number_product = number_product ?? throw new ArgumentNullException(nameof(number_product));
            this.consommable_quantity = consommable_quantity ?? throw new ArgumentNullException(nameof(consommable_quantity));
        }
    }
}
