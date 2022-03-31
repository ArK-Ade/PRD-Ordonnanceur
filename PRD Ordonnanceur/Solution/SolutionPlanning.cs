using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Solution
{
    /// <summary>
    /// 
    /// </summary>
    public class SolutionPlanning
    {
        private List<List<Object>> planningOF;
        private List<List<Object>> planningCons;
        private List<List<Object>> planningMachine;
        private List<List<Object>> planningTank;
        private List<List<Object>> planningOperator;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SolutionPlanning()
        {
            planningOF = new();
            planningCons = new();
            planningMachine = new();
            planningTank = new();
            planningOperator = new();
        }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="planningOF"></param>
        /// <param name="planningCons"></param>
        /// <param name="planningMachine"></param>
        /// <param name="planningTank"></param>
        /// <param name="planningOperator"></param>
        public SolutionPlanning(List<List<Object>> planningOF, List<List<Object>> planningCons, List<List<Object>> planningMachine, List<List<Object>> planningTank, List<List<Object>> planningOperator)
        {
            this.PlanningOF = planningOF;
            this.PlanningCons = planningCons;
            this.PlanningMachine = planningMachine;
            this.PlanningTank = planningTank;
            this.PlanningOperator = planningOperator;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<List<Object>> PlanningOF { get => planningOF; set => planningOF = value; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<List<Object>> PlanningCons { get => planningCons; set => planningCons = value; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<List<Object>> PlanningMachine { get => planningMachine; set => planningMachine = value; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<List<Object>> PlanningTank { get => planningTank; set => planningTank = value; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<List<Object>> PlanningOperator { get => planningOperator; set => planningOperator = value; }
    }
}