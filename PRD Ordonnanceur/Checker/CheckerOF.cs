using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Checker
{
    /// <summary>
    /// Static class that verifiy that the algorithm respect each constraint
    /// </summary>
    public static class CheckerOF
    {
        /// <summary>
        /// Method that verify the constraint link to the operator
        /// </summary>
        /// <param name="planning"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        public static bool CheckConstraintOperator(SolutionPlanning planning, List<Operator> operators)
        {
            DateTime jobBeginning1;
            DateTime jobEnd1;
            DateTime jobBeginning2;
            DateTime jobEnd2;

            List<Object> list;
            
            for (int i = 0; i < planning.PlanningOperator.Count; i++)
            {
                list = planning.PlanningOperator[i];

                Operator currentOperator = new();
                jobBeginning1 = (DateTime)list[1];
                jobEnd1 = (DateTime)list[2];
                uint idOp1 = (uint)list[5];

                foreach (Operator operat in operators)
                {
                    if (idOp1 == operat.Uid)
                    {
                        currentOperator = operat;
                        break;
                    }
                }

                // Checking if the job is within the work schedule
                if (jobBeginning1.Hour < currentOperator.StartWorkSchedule.Hour || (jobBeginning1.Hour == currentOperator.StartWorkSchedule.Hour && jobBeginning1.Minute < currentOperator.StartWorkSchedule.Minute))
                    return false;
                    
                if(jobEnd1.Hour > currentOperator.End.Hour || (jobEnd1.Hour == currentOperator.End.Hour && jobEnd1.Minute > currentOperator.End.Minute))
                    return false;

                int count = 0;

                // Checking if two job are scheduled at the same time for an operator
                foreach (List<Object> planningOperator in planning.PlanningOperator)
                {
                    jobBeginning2 = (DateTime)planningOperator[1];
                    jobEnd2 = (DateTime)planningOperator[2];

                    bool check = false;

                    // if it's the same operator and the jobs are different
                    if (idOp1 == (uint)planningOperator[5] && i != count)
                    {
                        if(jobBeginning1 < jobBeginning2 && jobEnd1 <= jobBeginning2)
                            check = true;

                        if(jobBeginning2 < jobBeginning1 && jobEnd2 <= jobEnd1)
                            check = true;

                        // if the jobs are sheduled at the same time
                        if (!check)
                            return false;
                    }
                    count++;
                }
            }

            return true;
        }

        /// <summary>
        /// Method that verify the constraint link to the machine
        /// </summary>
        /// <param name="planning"></param>
        /// <returns></returns>
        public static bool CheckConstraintMachine(SolutionPlanning planning)
        {
            DateTime jobBeginning1;
            DateTime jobEnd1;
            DateTime jobBeginning2;
            DateTime jobEnd2;

            List<Object> list;

            for (int i = 0; i < planning.PlanningMachine.Count; i++)
            {
                list = planning.PlanningMachine[i];

                jobBeginning1 = (DateTime)list[1];
                jobEnd1 = (DateTime)list[2];
                int idmachine = (int)list[6];

                int count = 0;

                // Checking if two job are scheduled at the same time for a machine
                foreach (List<Object> planningMachine in planning.PlanningMachine)
                {
                    jobBeginning2 = (DateTime)planningMachine[1];
                    jobEnd2 = (DateTime)planningMachine[2];

                    bool check = false;

                    // if it's the same machine and the jobs are different
                    if (idmachine == (int)planningMachine[6] && i != count)
                    {
                        if (jobBeginning1 < jobBeginning2 && jobEnd1 <= jobBeginning2)
                            check = true;

                        if (jobBeginning2 < jobBeginning1 && jobEnd2 <= jobEnd1)
                            check = true;

                        // if the jobs are sheduled at the same time
                        if (!check)
                            return false;
                    }
                    count++;
                }
            }

            return true;
        }

        /// <summary>
        /// Method that verify the constraint link to the tank
        /// </summary>
        /// <param name="planning"></param>
        /// <returns></returns>
        public static bool CheckConstraintTank(SolutionPlanning planning)
        {
            DateTime jobBeginning1;
            DateTime jobEnd1;
            DateTime jobBeginning2;
            DateTime jobEnd2;

            List<Object> list;

            for (int i = 0; i < planning.PlanningTank.Count; i++)
            {
                list = planning.PlanningTank[i];

                jobBeginning1 = (DateTime)list[1];
                jobEnd1 = (DateTime)list[2];
                int idTank = (int)list[6];

                int count = 0;

                // Checking if two job are scheduled at the same time for a tank
                foreach (List<Object> planningTank in planning.PlanningMachine)
                {
                    jobBeginning2 = (DateTime)planningTank[1];
                    jobEnd2 = (DateTime)planningTank[2];

                    bool check = false;

                    // if it's the same tank and the jobs are different
                    if (idTank == (int)planningTank[6] && i != count)
                    {
                        if (jobBeginning1 < jobBeginning2 && jobEnd1 <= jobBeginning2)
                            check = true;

                        if (jobBeginning2 < jobBeginning1 && jobEnd2 <= jobEnd1)
                            check = true;

                        // if the jobs are sheduled at the same time
                        if (!check)
                            return false;
                    }
                    count++;
                }
            }

            return true;
        }

        /// <summary>
        /// Method that verify the constraint link to the consumable
        /// </summary>
        /// <param name="planning"></param>
        /// <param name="consumables"></param>
        /// <returns></returns>
        public static bool CheckConstraintConsumable(SolutionPlanning planning, List<Consumable> consumables)
        {
            int countConsomable;
            double quantityConso;
            Consumable currentConso;

            // Checking if the sum used for one consumable 
            for (countConsomable = 0; countConsomable < consumables.Count ; countConsomable++)
            {
                currentConso = consumables[countConsomable];
                quantityConso = currentConso.QuantityAvailable;

                foreach (List<Object> listConsomable in planning.PlanningCons)
                {
                    if((int) listConsomable[2] == currentConso.Id)
                    {
                        quantityConso -= (double) listConsomable[1];
                    }
                }

                if (quantityConso < 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planning"></param>
        /// <returns></returns>
        public static bool CheckConstraintOF(SolutionPlanning planning)
        {
            return true;
        }
    }
}