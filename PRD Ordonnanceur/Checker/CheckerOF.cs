using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Checker
{
    // Il doit rregarde sur chaque jour si toutes les contraintes sont respectées.
    public static class CheckerOF
    {
        public static bool CheckConstrainOperator(SolutionPlanning planning, List<Operator> operators)
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
                    if (idOp1 == operat.Id)
                    {
                        currentOperator = operat;
                        break;
                    };
                }

                // Vérification heure de travail
                if (jobBeginning1.Hour < currentOperator.Beginning.Hour || (jobBeginning1.Hour == currentOperator.Beginning.Hour && jobBeginning1.Minute < currentOperator.Beginning.Minute))
                    return false;
                    
                if(jobEnd1.Hour > currentOperator.End.Hour || (jobEnd1.Hour == currentOperator.End.Hour && jobEnd1.Minute > currentOperator.End.Minute))
                    return false;

                int count = 0;

                // Vérification planning
                foreach (List<Object> planningOperator in planning.PlanningOperator)
                {
                    jobBeginning2 = (DateTime)planningOperator[1];
                    jobEnd2 = (DateTime)planningOperator[2];

                    bool check = false;

                    if (idOp1 == (uint)planningOperator[5] && i != count)
                    {
                        if(jobBeginning1 < jobBeginning2 && jobEnd1 <= jobBeginning2)
                            check = true;

                        if(jobBeginning2 < jobBeginning1 && jobEnd2 <= jobEnd1)
                            check = true;

                        if (!check)
                            return false;
                    }
                    count++;
                }
            }

            return true;
        }

        public static bool CheckConstrainMachine(SolutionPlanning planning)
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

                // Vérification planning
                foreach (List<Object> planningMachine in planning.PlanningMachine)
                {
                    jobBeginning2 = (DateTime)planningMachine[1];
                    jobEnd2 = (DateTime)planningMachine[2];

                    bool check = false;

                    if (idmachine == (int)planningMachine[6] && i != count)
                    {
                        if (jobBeginning1 < jobBeginning2 && jobEnd1 <= jobBeginning2)
                            check = true;

                        if (jobBeginning2 < jobBeginning1 && jobEnd2 <= jobEnd1)
                            check = true;

                        if (!check)
                            return false;
                    }
                    count++;
                }
            }

            return true;
        }

        public static bool CheckConstrainTank(SolutionPlanning planning)
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

                // Vérification planning
                foreach (List<Object> planningTank in planning.PlanningMachine)
                {
                    jobBeginning2 = (DateTime)planningTank[1];
                    jobEnd2 = (DateTime)planningTank[2];

                    bool check = false;

                    if (idTank == (int)planningTank[6] && i != count)
                    {
                        if (jobBeginning1 < jobBeginning2 && jobEnd1 <= jobBeginning2)
                            check = true;

                        if (jobBeginning2 < jobBeginning1 && jobEnd2 <= jobEnd1)
                            check = true;

                        if (!check)
                            return false;
                    }
                    count++;
                }
            }

            return true;
        }

        public static bool CheckConstrainConsommable(SolutionPlanning planning, List<Consumable> consumables)
        {
            // Verification consommable negatif
            int countConsomable;
            int numberConso;
            Consumable currentConso;

            for (countConsomable = 0; countConsomable < consumables.Count ; countConsomable++)
            {
                currentConso = consumables[countConsomable];
                numberConso = currentConso.QuantityAvailable;

                foreach (List<Object> listConsomable in planning.PlanningCons)
                {
                    if((int) listConsomable[2] == currentConso.Id)
                    {
                        numberConso -= (int) listConsomable[1];
                    }
                }

                if (numberConso < 0)
                    return false;
            }
            return true;
        }

        public static bool CheckConstrainOF(SolutionPlanning planning)
        {
            return true;
        }

    }
}