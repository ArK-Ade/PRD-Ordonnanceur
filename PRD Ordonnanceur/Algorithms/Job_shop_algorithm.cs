using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Job_shop_algorithm
    {

        private Available available;
        private SolutionPlanning solutionPlanning;

        public Job_shop_algorithm()
        {
            solutionPlanning.InitPlanning();
        }

        public Job_shop_algorithm(Available available, SolutionPlanning solutionPlanning)
        {
            this.available = available;
            this.solutionPlanning = solutionPlanning;
        }

        // TODO Initialisation des plannings


        // TODO A finir
        public Object[,] Search_Ressources(DateTime time, Step step, bool firstime)
        {
            Tank[] tankAvailable = null;

            // Recherche des opérateurs disponibles
            Operator[] operatorAvailableStep = available.findOperatorForTank(solutionPlanning.PlanningOperator);

            Machine[] machineAvailable = available.findMachineForStep(solutionPlanning.PlanningMachine);

            if (firstime)
            {
                tankAvailable = available.findTankForStep(solutionPlanning.PlanningTank);
            }

            Operator[] operatorAvailableTank = available.findOperatorForTank(solutionPlanning.PlanningOperator);

            Consumable[] consomableAvailable = available.findConsoForStep(solutionPlanning.PlanningCons);

            // Determiner comment noter que des operateurs sont en train de travailler
            // Il faut regarder dans le planning si les opérateurs sont disponibles

            foreach (Operator op in operatorAvailableStep)
            {

            }

            foreach (Operator op in operatorAvailableTank)
            {

            }

            foreach (Machine machine in machineAvailable)
            {

            }

            foreach (Consumable conso in consomableAvailable)
            {

            }

            
            foreach (Tank tank in tankAvailable)
            {

            }



            return ojet;
        }

        public void Step_algorithm(OF[] oFs, DateTime time, Operator[] operators)
        {
            int nbCteMaxViole = 0;
            bool firstime = false;

            foreach (OF oF in oFs)
            {
                DateTime dti;

                if (oF.Starting_hour == DateTime.MinValue)
                {
                    dti = oF.EarliestDate;
                }
                else
                {
                    dti = oF.Starting_hour;
                }


                foreach (Step step in oF.StepSequence)
                {

                    if (step == oF.StepSequence[0])
                    {
                        firstime = true;
                    }

                    Object[,] resultat = Search_Ressources(time, step, firstime);

                    if ((DateTime)resultat[1, 1] > time)
                        nbCteMaxViole++;

                    // Panifier l'étape courante a t'

                }

                // Changer l'heure

                // Nettoyage de la cuve
                Operator[] operatorAvailable = available.findOperatorForTank(solutionPlanning.PlanningOperator);
            }
        }
    }
}
