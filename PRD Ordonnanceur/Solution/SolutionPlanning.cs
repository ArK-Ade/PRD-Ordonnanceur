using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using System;

namespace PRD_Ordonnanceur.Solution
{
    public class SolutionPlanning
    {
        private DataParsed dataParsed;
        private Object[,] planningOF;
        private Object[,] planningCons;
        private Object[,] planningMachine;
        private Object[,] planningTank;
        private Object[,] planningOperator;

        public SolutionPlanning()
        {
            foreach(Tank tank in DataParsed.Tanks)
            {

            }

        }

        public SolutionPlanning(object[,] planningOF, object[,] planningCons, object[,] planningMachine, object[,] planningTank, object[,] planningOperator, DataParsed dataParsed)
        {
            this.PlanningOF = planningOF;
            this.PlanningCons = planningCons;
            this.PlanningMachine = planningMachine;
            this.PlanningTank = planningTank;
            this.PlanningOperator = planningOperator;
            this.DataParsed = dataParsed;
        }

        public object[,] PlanningOF { get => planningOF; set => planningOF = value; }
        public object[,] PlanningCons { get => planningCons; set => planningCons = value; }
        public object[,] PlanningMachine { get => planningMachine; set => planningMachine = value; }
        public object[,] PlanningTank { get => planningTank; set => planningTank = value; }
        public object[,] PlanningOperator { get => planningOperator; set => planningOperator = value; }
        public DataParsed DataParsed { get => dataParsed; set => dataParsed = value; }

        public void InitPlanning()
        {
            // TODO Initialisation des plannings
        }
    }
}
