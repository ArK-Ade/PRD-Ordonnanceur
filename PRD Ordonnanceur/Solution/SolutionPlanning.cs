using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Solution
{
    public class SolutionPlanning
    {
        private List<List<Object>> planningOF;
        private List<List<Object>> planningCons;
        private List<List<Object>> planningMachine;
        private List<List<Object>> planningTank;
        private List<List<Object>> planningOperator;

        public SolutionPlanning()
        {

        }

        public SolutionPlanning(List<List<Object>> planningOF, List<List<Object>> planningCons, List<List<Object>> planningMachine, List<List<Object>> planningTank, List<List<Object>> planningOperator)
        {
            this.PlanningOF = planningOF;
            this.PlanningCons = planningCons;
            this.PlanningMachine = planningMachine;
            this.PlanningTank = planningTank;
            this.PlanningOperator = planningOperator;
        }

        public List<List<Object>> PlanningOF { get => planningOF; set => planningOF = value; }
        public List<List<Object>> PlanningCons { get => planningCons; set => planningCons = value; }
        public List<List<Object>> PlanningMachine { get => planningMachine; set => planningMachine = value; }
        public List<List<Object>> PlanningTank { get => planningTank; set => planningTank = value; }
        public List<List<Object>> PlanningOperator { get => planningOperator; set => planningOperator = value; }
    }
}
