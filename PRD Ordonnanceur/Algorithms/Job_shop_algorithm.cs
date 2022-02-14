using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using PRD_Ordonnanceur.Solution;
using PRD_Ordonnanceur.Checker;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Job_shop_algorithm
    {
        private DataParsed data;
        private Available availableAlgorithm;
        private List<SolutionPlanning> plannings;
        private SolutionPlanning solutionPlanning;
        private CheckerOF checker;

        public Job_shop_algorithm()
        {
            this.data = new();
            this.availableAlgorithm = new();
            this.plannings = new();
            this.solutionPlanning = new();
            this.checker = new();
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


        /// <summary>
        /// It searchs the ressources necessery for the algorythm
        /// </summary>
        /// <param name="time"></param>
        /// <param name="step"></param>
        /// <param name="firstStep"></param>
        /// <returns></returns>
        public List<Object> Search_Ressources(DateTime time, Step step, bool firstStep)
        {
            // Recherche des ressouces disponibles

            // S'il s'agit de la premiere étape, on recherche une cuve pour l'OF
            List<Tank> tankAvailable = null;

            if (firstStep)
                tankAvailable = AvailableAlgorithm.FindTankForStep(SolutionPlanning.PlanningTank, DataParsed.Tanks, time);

            // TODO Rechercher les opérateurs pour durée avant et après étape + nettoyage machine
            List<Operator> operatorAvailableStep = AvailableAlgorithm.FindOperatorForStep(SolutionPlanning.PlanningOperator, DataParsed.Operators, time, step.TypeMachineNeeded);
            List<Machine> machineAvailable = AvailableAlgorithm.FindMachineForStep(SolutionPlanning.PlanningMachine,DataParsed.Machine,time,step.TypeMachineNeeded);
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

        public SolutionPlanning ScheduleStep(List<Object> ressourceList, SolutionPlanning solutionPlanning, DateTime time, OF oF, Step step, Boolean lastStep, OF oFBefore)
        {
            DateTime dayTime = time.Date;
            String code = "";

            Operator operatorBefore = (Operator) ressourceList[0];
            Operator operatorAfter = (Operator) ressourceList[0];
            Operator operatorNetMachine = (Operator)ressourceList[0];
            Consumable consumable = (Consumable)ressourceList[0];
            Machine machine = (Machine)ressourceList[0];

            Operator operatorTank = null;
            
            // Planification OperateurAvant
            List<Object> listOpBefore = new();
            listOpBefore.Add(dayTime);
            listOpBefore.Add(time);
            listOpBefore.Add(time.Add(step.Duration.DurationBeforeOp));
            code = "OPBefore";
            listOpBefore.Add(code);
            listOpBefore.Add(oF.IdOF);
            listOpBefore.Add(operatorBefore.Id);

            // Ajout dans la planification du jour
            solutionPlanning.PlanningOperator.Add(listOpBefore);

            // Planification OperateurApres
            List<Object> listOpAfter = new();
            listOpAfter.Add(dayTime);
            listOpAfter.Add(time.Subtract(step.Duration.DurationAfterOp));
            listOpAfter.Add(time.Add(step.Duration.DurationOp));
            code = "OPAfter";
            listOpAfter.Add(code);
            listOpAfter.Add(oF.IdOF);
            listOpAfter.Add(operatorAfter.Id);

            // Ajout dans la planification du jour
            solutionPlanning.PlanningOperator.Add(listOpAfter);

            // Planification OperateurNettoyage Machine
            List<Object> listOpCleaning = new();
            listOpCleaning.Add(dayTime);
            listOpCleaning.Add(time.Add(step.Duration.DurationOp));
            listOpCleaning.Add(time.Add(step.Duration.DurationOp).Add(machine.Duration_cleaning));
            code = "OPNetMachine";
            listOpCleaning.Add(code);
            listOpCleaning.Add(machine.Id);
            listOpCleaning.Add(operatorNetMachine.Id);

            solutionPlanning.PlanningOperator.Add(listOpCleaning);

            // Planification OperateurTank
            List<Object> listOpTank = null;
            List<Object> listTank = null;

            if (lastStep)
            {
                operatorTank = (Operator)ressourceList[0];
                Tank tank = (Tank)ressourceList[0];

                TimeSpan timeSpan = availableAlgorithm.FindTimeCleaningTank(oFBefore, oF, tank);

                listOpTank = new();
                listOpTank.Add(dayTime);
                listOpTank.Add(time.Add(step.Duration.DurationOp));
                listOpTank.Add(time.Add(step.Duration.DurationOp).Add(timeSpan));
                code = "OPNetTank";
                listOpTank.Add(code);
                listOpTank.Add(tank.IdTank);
                listOpTank.Add(operatorTank.Id);

                // Planification Tank
                listTank = new();
                listTank.Add(dayTime);
                listTank.Add(time.Add(step.Duration.DurationOp));
                listTank.Add(time.Add(step.Duration.DurationOp).Add(timeSpan));
                listTank.Add(timeSpan);
                listTank.Add(oF.IdOF);
                listTank.Add(operatorTank.Id);
            }
           
            solutionPlanning.PlanningOperator.Add(listOpTank);
            solutionPlanning.PlanningTank.Add(listTank);

            // Planification Machine
            List<Object> listMachine = new();
            listMachine.Add(dayTime);
            listMachine.Add(oF.IdOF);
            listMachine.Add(operatorBefore.Id);
            listMachine.Add(operatorAfter.Id);

            solutionPlanning.PlanningMachine.Add(listMachine);

            // Planification Consommable
            List<Object> listConsumable = new();
            listConsumable.Add(dayTime);
            listMachine.Add(step.ConsumableUsed);

            solutionPlanning.PlanningCons.Add(listConsumable);

            return solutionPlanning;
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

            int countOF = -1;
            OF ofBefore = null;
            
            SolutionPlanning planningAujourd = new();

            foreach (OF oF in DataParsed.OFs)
            {
                if(countOF <= 0)
                    ofBefore = DataParsed.OFs[countOF];
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
                    planningAujourd = ScheduleStep(resultat,planningAujourd,timeNow,oF,step,false,ofBefore);
                }

                // Nettoyage de la cuve
                List<Operator> operatorAvailable = AvailableAlgorithm.FindOperatorForTank(SolutionPlanning.PlanningOperator,DataParsed.Operators,timeNow);

                countOF++;
            }
        }
    }
}
