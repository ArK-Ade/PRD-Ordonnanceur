using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Solution
{
    /// <summary>
    /// Class that represents a planning for one day
    /// </summary>
    public class SolutionPlanning
    {
        private List<List<Object>> planningOF;
        private List<List<Object>> planningCons;
        private List<List<Object>> planningMachine;
        private List<List<Object>> planningTank;
        private List<List<Object>> planningOperator;
        private List<List<Object>> planningStep;

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
            PlanningStep = new();
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
        /// Confortable Constructor
        /// </summary>
        /// <param name="planningOF"></param>
        /// <param name="planningCons"></param>
        /// <param name="planningMachine"></param>
        /// <param name="planningTank"></param>
        /// <param name="planningOperator"></param>
        /// <param name="planningStep"></param>
        public SolutionPlanning(List<List<object>> planningOF, List<List<object>> planningCons, List<List<object>> planningMachine, List<List<object>> planningTank, List<List<object>> planningOperator, List<List<object>> planningStep)
        {
            this.planningOF = planningOF;
            this.planningCons = planningCons;
            this.planningMachine = planningMachine;
            this.planningTank = planningTank;
            this.planningOperator = planningOperator;
            this.PlanningStep = planningStep;
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
        
        /// <summary>
        /// 
        /// </summary>
        public List<List<object>> PlanningStep { get => planningStep; set => planningStep = value; }
    }
}