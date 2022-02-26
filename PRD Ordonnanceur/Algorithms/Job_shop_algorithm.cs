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
        /// <param name="lastStep"></param>
        /// <returns></returns>
        public List<Object> Search_Ressources(DateTime time, Step step, bool lastStep)
        {
            // S'il s'agit de la dernière étape, on recherche une cuve pour l'OF
            List<Tank> tankAvailable = null;

            if (lastStep)
                tankAvailable = AvailableAlgorithm.FindTankForStep(SolutionPlanning.PlanningTank, DataParsed.Tanks, time);

            List<Operator> operatorAvailableBeforeOp = AvailableAlgorithm.FindOperator(SolutionPlanning.PlanningOperator, DataParsed.Operators, time + step.Duration.DurationBeforeOp, step.TypeMachineNeeded);
            List<Operator> operatorAvailableAfterOp = AvailableAlgorithm.FindOperator(SolutionPlanning.PlanningOperator, DataParsed.Operators, time + step.Duration.DurationOp + step.Duration.DurationAfterOp, step.TypeMachineNeeded);

            List<Machine> machineAvailable = AvailableAlgorithm.FindMachineForStep(SolutionPlanning.PlanningMachine,DataParsed.Machine,time,step.TypeMachineNeeded);
            List<Operator> operatorAvailableTank = AvailableAlgorithm.FindOperatorForTank(SolutionPlanning.PlanningOperator,DataParsed.Operators,time);
            bool consomableAvailable = AvailableAlgorithm.FindConsoForStep(SolutionPlanning.PlanningCons,DataParsed.Consummables,time,step.ConsumableUsed,step.QuantityConsumable);

            
            // Si une des ressources est indisponible on passe 5 minutes plus tard
            if (operatorAvailableBeforeOp == null ||
                operatorAvailableAfterOp == null ||
                machineAvailable == null ||
                operatorAvailableTank == null ||
                consomableAvailable == false)
            {
                return Search_Ressources(time.AddMinutes(5.0), step, lastStep);
            }

            // S'il manque des consommables on passe au jour d'après
            if (consomableAvailable == false){

                while(time.Hour != data.Operators[0].Beginning.Hour)
                {
                    time.AddMinutes(5.0);
                }
                return Search_Ressources(time, step, lastStep);
            }

            // On cherche un operateur pour nettoyer la machine choisie
            // TODO Simplifier les fonctions et attributs
            List<Operator> operatorAvailableCleaning = AvailableAlgorithm.FindOperator(SolutionPlanning.PlanningOperator, DataParsed.Operators, time + step.Duration.DurationOp + step.Duration.DurationAfterOp + machineAvailable[0].Duration_cleaning, TypeMachine.cleaning);

            List<Object> listRessourcesAvailable = new();

            // On choisit la premiere ressource de chaque liste
            listRessourcesAvailable.Add(operatorAvailableBeforeOp[0]);
            listRessourcesAvailable.Add(operatorAvailableAfterOp[0]);
            listRessourcesAvailable.Add(operatorAvailableCleaning[0]);
            listRessourcesAvailable.Add(operatorAvailableTank[0]);
            listRessourcesAvailable.Add(machineAvailable[0]);
            listRessourcesAvailable.Add(consomableAvailable);

            if (lastStep)
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

            solutionPlanning.PlanningCons.Add(listConsumable); // TODO Attention contrainte quantité negatif

            return solutionPlanning;
        }

        /// <summary>
        /// Algorithm who planify the ressources
        /// $
        /// 
        /// 
        /// 
        /// </summary>
        /// <param name="oFs"></param>
        /// <param name="BeginningDate"></param>
        /// <param name="operators"></param>
        public void Step_algorithm(DateTime time)
        {
            int nbCteMaxViole = 0; // Il s'agit d'une constante qui va nous permettre de savoir le nombre de contraintes violés et permettra de comparer des heuristics
            bool lastStep = false;

            // TODO faire en sorte de faire solution1 et solutionbis
            // il faut effacer l'un des deux lorsqu'il faut tout replanifier

            // Prendre en compte les OFs en Cours

            // S'il on trouve dans un weekend on passe un jour
            while (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
                time.AddDays(1);

            int countOF = -1;
            int countStep = 0;
            OF ofBefore = null;
            DateTime currentTime = time;
            List<Object> resultRessources;

            SolutionPlanning planningAujourd = new();

            foreach (OF oF in DataParsed.OFs)
            {
                // S'il on trouve dans un weekend on passe un jour
                while (currentTime.DayOfWeek == DayOfWeek.Saturday || currentTime.DayOfWeek == DayOfWeek.Sunday)
                    currentTime.AddDays(1);

                if (countOF <= 0)
                    ofBefore = DataParsed.OFs[countOF];

                DateTime dti = DateTime.MaxValue;

                if (oF.Starting_hour == DateTime.MinValue)
                {
                    dti = oF.EarliestDate;
                }
                else
                {
                    dti = oF.Starting_hour;
                }

                // Prend en compte les jours au plus tot
                while(dti.Day > currentTime.Day)
                {
                    currentTime.AddDays(1);
                }

                foreach (Step step in oF.StepSequence)
                {

                    if (oF.StepSequence[countStep+1] == null)
                    {
                        lastStep = true;
                    }

                    // TODO gerer le cas ou on ne peux pas reserver les ressources aka Step non reportable
                    resultRessources = Search_Ressources(currentTime, step, lastStep);

                    // Si on ne peux pas reserver des ressources, changer de jour et annuler la reservation de l'OF
                    if(resultRessources == null)
                    {
                        planningAujourd = null;
                        break;
                    }

                    if ((DateTime)resultRessources[0] > oF.LatestDate)
                        nbCteMaxViole++;

                    // Panifier l'étape courante a t'
                    planningAujourd = ScheduleStep(resultRessources,planningAujourd,currentTime,oF,step,false,ofBefore);

                    currentTime.Add(step.Duration.DurationOp + step.Duration.DurationBeforeOp + step.Duration.DurationAfterOp);
                    countStep++;
                }
                countStep = 0;
                countOF++;
                plannings.Add(planningAujourd);
            }
        }
    }
}
