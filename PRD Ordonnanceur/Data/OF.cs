using System;

namespace PRD_Ordonnanceur.Data
{
    // TODO Changer les accesseurs get et set de toutes les classes
    class OF
    {
        private int Id { get; set; }

        public string[] StepSequence { get; set; }

        private DateTime _earliestDate;

        public struct Status
        {
            public DateTime starting_hour { get; set; }
            public string next_step { get; set; }
        };

        public DateTime LatestDate { get; set; }

        public Tank[] set_tank { get; set; }

        public string NumberProduct { get; set; }

        public string[][] consommable_quantity { get; set; }

        public OF() { }

        public OF(int id, string[] stepSequence, DateTime earliestDate, DateTime latestDate, Tank[] set_tank, string number_product, string[][] consommable_quantity)
        {
            Id = id;
            StepSequence = stepSequence ?? throw new ArgumentNullException(nameof(stepSequence));
            _earliestDate = earliestDate;
            LatestDate = latestDate;
            this.set_tank = set_tank ?? throw new ArgumentNullException(nameof(set_tank));
            this.NumberProduct = number_product ?? throw new ArgumentNullException(nameof(number_product));
            this.consommable_quantity = consommable_quantity ?? throw new ArgumentNullException(nameof(consommable_quantity));
        }

        public DateTime EarliestDate
        
        {   
            get => _earliestDate; 
            set => _earliestDate = value; 
        }
    }
}
