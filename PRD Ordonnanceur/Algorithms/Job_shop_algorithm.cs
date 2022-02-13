using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Job_shop_algorithm
    {
        private DataParsed data;
        private Available availableAlgorithm;
        private List<SolutionPlanning> plannings = new();
        private SolutionPlanning solutionPlanning = new();

        public Job_shop_algorithm()
        {
        }

        public Job_shop_algorithm(Available available, SolutionPlanning solutionPlanning)
        {
            this.AvailableAlgorithm = available;
            this.SolutionPlanning = solutionPlanning;
        }

        public DataParsed DataParsed { get => data; set => data = value; }
        public List<SolutionPlanning> Plannings { get => plannings; set => plannings = value; }
        public SolutionPlanning SolutionPlanning { get => solutionPlanning; set => solutionPlanning = value; }
        public Available AvailableAlgorithm { get => availableAlgorithm; set => availableAlgorithm = value; }

        // TODO Initialisation des plannings


        /// <summary>
        /// It searchs the ressources necessery for the algorythm
        /// </summary>
        /// <param name="time"></param>
        /// <param name="step"></param>
        /// <param name="firstStep"></param>
        /// <returns></returns>
        public List<Object> Search_Ressources(DateTime time, Step step, bool firstStep)
        {
            List<Tank> tankAvailable = null;

            // Recherche des ressouces disponibles
            List<Operator> operatorAvailableStep = AvailableAlgorithm.FindOperatorForStep(SolutionPlanning.PlanningOperator, DataParsed.Operators, time, step.TypeMachineNeeded);

            List<Machine> machineAvailable = AvailableAlgorithm.FindMachineForStep(SolutionPlanning.PlanningMachine,DataParsed.Machine,time,step.TypeMachineNeeded);

            if (firstStep)
            {
                tankAvailable = AvailableAlgorithm.FindTankForStep(SolutionPlanning.PlanningTank,DataParsed.Tanks,time);
            }

            List<Operator> operatorAvailableTank = AvailableAlgorithm.FindOperatorForTank(SolutionPlanning.PlanningOperator,DataParsed.Operators,time);

            List<Consumable> consomableAvailable = AvailableAlgorithm.FindConsoForStep(SolutionPlanning.PlanningCons,DataParsed.Consummables,time,step.ConsumableUsed,step.QuantityConsumable);


            // Si une des ressources est indisponible on passe 5 minutes plus tard
            if (operatorAvailableStep == null ||
                machineAvailable == null ||
                operatorAvailableTank == null ||
                consomableAvailable == null)
            {
                return Search_Ressources(time.AddMinutes(5.0), step, firstStep);
            }

            List<Object> listRessourcesAvailable = new();

            // On choisit la premiere ressource de chaque liste
            listRessourcesAvailable.Add(operatorAvailableStep[0]);
            listRessourcesAvailable.Add(operatorAvailableTank[0]);
            listRessourcesAvailable.Add(machineAvailable[0]);
            listRessourcesAvailable.Add(consomableAvailable[0]);

            if (firstStep)
            {
                listRessourcesAvailable.Add(tankAvailable[0]);
            }
            
            // On renvoie une liste d'objet contenant toutes les ressources
            return listRessourcesAvailable;
        }

        public void ScheduleStep(List<Object> ressourceList)
        {
            // Planification Operateur

            // Planification OperateurTank

            // Planification Machine

            // Planification Consommable

            // Planification Tank
        }

        /// <summary>
        /// Algorithm who planify the ressources
        /// </summary>
        /// <param name="oFs"></param>
        /// <param name="BeginningDate"></param>
        /// <param name="operators"></param>
        public void Step_algorithm(DateTime BeginningDate, DateTime timeNow)
        {
            int nbCteMaxViole = 0;
            bool firstStep = false;
            
            SolutionPlanning planningAujourd = new();

            foreach (OF oF in DataParsed.OFs)
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
                        firstStep = true;
                    }

                    List<Object> resultat = Search_Ressources(BeginningDate, step, firstStep);

                    if ((DateTime)resultat[0] > BeginningDate)
                        nbCteMaxViole++;

                    // Panifier l'étape courante a t'
                    ScheduleStep(resultat);
                }

                // Nettoyage de la cuve
                List<Operator> operatorAvailable = AvailableAlgorithm.FindOperatorForTank(SolutionPlanning.PlanningOperator,DataParsed.Operators,timeNow);
            }
        }
    }
}
