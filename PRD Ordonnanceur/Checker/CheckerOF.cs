using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Checker
{
    // TODO Faire le checker qui va vérifier si la solution est correcte
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
                jobBeginning1 = (DateTime)list[2];
                jobEnd1 = (DateTime)list[3];

                foreach (Operator operat in operators)
                {
                    if ((uint)list[5] == operat.Id)
                    {
                        currentOperator = operat;
                        break;
                    };
                }

                // Vérification heure de travail
                if (jobBeginning1 < currentOperator.Beginning || jobEnd1 > currentOperator.End)
                {
                    return false;
                }

                // Vérification planning
                foreach (List<Object> planningOperator in planning.PlanningOperator)
                {
                    jobBeginning2 = (DateTime)planningOperator[2];
                    jobEnd2 = (DateTime)planningOperator[3];

                    if (jobBeginning1 >= jobBeginning2 && jobBeginning1 <= jobEnd2)
                    {
                        return false;
                    }
                    else if (jobBeginning1 <= jobBeginning2 && jobEnd1 >= jobEnd2)
                    {
                        return false;
                    }
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

            // Verification planning
            for (int i = 0; i < planning.PlanningMachine.Count; i++)
            {
                foreach (List<Object> planningMachine in planning.PlanningMachine)
                {
                    list = planning.PlanningOperator[i];

                    jobBeginning1 = (DateTime)list[2];
                    jobEnd1 = (DateTime)list[3];
                    jobBeginning2 = (DateTime)planningMachine[2];
                    jobEnd2 = (DateTime)planningMachine[3];

                    if (jobBeginning1 >= jobBeginning2 && jobBeginning1 <= jobEnd2)
                    {
                        return false;
                    }
                    else if (jobBeginning1 <= jobBeginning2 && jobEnd1 >= jobEnd2)
                    {
                        return false;
                    }
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

            // Vérification planning
            for (int i = 0; i < planning.PlanningTank.Count; i++)
            {
                foreach (List<Object> planningTank in planning.PlanningTank)
                {
                    list = planning.PlanningTank[i];

                    jobBeginning1 = (DateTime)list[2];
                    jobEnd1 = (DateTime)list[3];
                    jobBeginning2 = (DateTime)planningTank[2];
                    jobEnd2 = (DateTime)planningTank[3];

                    if (jobBeginning1 >= jobBeginning2 && jobBeginning1 <= jobEnd2)
                    {
                        return false;
                    }
                    else if (jobBeginning1 <= jobBeginning2 && jobEnd1 >= jobEnd2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        // TODO Terminer cette fonction
        public static bool CheckConstrainConsommable(SolutionPlanning planning)
        {
            return true;
        }

        public static bool CheckConstrainOF()
        {
            return true;
        }

    }
}