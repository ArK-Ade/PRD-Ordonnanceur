using PRD_Ordonnanceur.Data;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class DataParsed
    {
        private List<OF> oFs;
        private List<Consumable> consummables;
        private List<Machine> machine;
        private List<Tank> tanks;
        private List<Operator> operators;
        private Matrix_cleaning matrix_Cleaning;

        public DataParsed()
        {
            // Initialisation des données parsées
        }

        public DataParsed(List<OF> oFs, List<Consumable> consummables, List<Machine> machine, List<Tank> tanks, List<Operator> operators, Matrix_cleaning matrix_Cleaning)
        {
            this.OFs = oFs;
            this.Consummables = consummables;
            this.Machine = machine;
            this.Tanks = tanks;
            this.Operators = operators;
            this.Matrix_Cleaning = matrix_Cleaning;
        }

        public List<OF> OFs { get => oFs; set => oFs = value; }
        public List<Consumable> Consummables { get => consummables; set => consummables = value; }
        public List<Machine> Machine { get => machine; set => machine = value; }
        public List<Tank> Tanks { get => tanks; set => tanks = value; }
        public List<Operator> Operators { get => operators; set => operators = value; }
        public Matrix_cleaning Matrix_Cleaning { get => matrix_Cleaning; set => matrix_Cleaning = value; }
    }
}