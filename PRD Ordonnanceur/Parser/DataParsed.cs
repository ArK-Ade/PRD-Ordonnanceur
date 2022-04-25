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
        private MatrixCleaning matrix_Cleaning;

        /// <summary>
        ///
        /// </summary>
        public DataParsed()
        {
            // Initialisation des données parsées
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oFs"></param>
        /// <param name="consummables"></param>
        /// <param name="machine"></param>
        /// <param name="tanks"></param>
        /// <param name="operators"></param>
        /// <param name="matrix_Cleaning"></param>
        public DataParsed(List<OF> oFs, List<Consumable> consummables, List<Machine> machine, List<Tank> tanks, List<Operator> operators, MatrixCleaning matrix_Cleaning)
        {
            this.OFs = oFs;
            this.Consummables = consummables;
            this.Machine = machine;
            this.Tanks = tanks;
            this.Operators = operators;
            this.Matrix_Cleaning = matrix_Cleaning;
        }

        /// <summary>
        ///
        /// </summary>
        public List<OF> OFs { get => oFs; set => oFs = value; }

        /// <summary>
        ///
        /// </summary>
        public List<Consumable> Consummables { get => consummables; set => consummables = value; }

        /// <summary>
        ///
        /// </summary>
        public List<Machine> Machine { get => machine; set => machine = value; }

        /// <summary>
        ///
        /// </summary>
        public List<Tank> Tanks { get => tanks; set => tanks = value; }

        /// <summary>
        ///
        /// </summary>
        public List<Operator> Operators { get => operators; set => operators = value; }

        /// <summary>
        ///
        /// </summary>
        public MatrixCleaning Matrix_Cleaning { get => matrix_Cleaning; set => matrix_Cleaning = value; }
    }
}