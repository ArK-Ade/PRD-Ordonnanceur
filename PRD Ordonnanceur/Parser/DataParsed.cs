using PRD_Ordonnanceur.Data;

namespace PRD_Ordonnanceur.Parser
{
    public class DataParsed
    {
        private OF[] oFs;
        private Consumable[] consummables;
        private Machine[] machine;
        private Tank[] tanks;
        private Operator[] operators;

        public DataParsed() 
        { 
            // Initialisation des données parsées


        }

        public DataParsed(OF[] oFs, Consumable[] consummables, Machine[] machine, Tank[] tanks, Operator[] operators)
        {
            this.OFs = oFs;
            this.Consummables = consummables;
            this.Machine = machine;
            this.Tanks = tanks;
            this.Operators = operators;
        }

        public OF[] OFs { get => oFs; set => oFs = value; }
        public Consumable[] Consummables { get => consummables; set => consummables = value; }
        public Tank[] Tanks { get => tanks; set => tanks = value; }
        public Operator[] Operators { get => operators; set => operators = value; }
        internal Machine[] Machine { get => machine; set => machine = value; }
    }
}
