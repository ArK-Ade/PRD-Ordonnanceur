using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class jobShopAlgorithm
    {
        private DataParsed data;
        private Available availableAlgorithm;
        private List<SolutionPlanning> plannings;
        private SolutionPlanning solutionPlanning;

        public jobShopAlgorithm()
        {
            this.data = new();
            this.availableAlgorithm = new();
            this.plannings = new();
            this.solutionPlanning = new();
        }

        public jobShopAlgorithm(DataParsed data, Available availableAlgorithm, List<SolutionPlanning> plannings, SolutionPlanning solutionPlanning)
        {
            this.data = data;
            this.availableAlgorithm = availableAlgorithm;
            this.plannings = plannings;
            this.solutionPlanning = solutionPlanning;
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
        public List<Object> SearchRessources(DateTime time, Step step, bool lastStep)
        {
            DateTime beginningOpBeforeTime = time;
            DateTime endOpBeforeTime = time.Add(step.Duration.DurationBeforeOp);
            DateTime beginningOpAfterTime = endOpBeforeTime.Add(step.Duration.DurationOp);
            DateTime endOpAfterTime = beginningOpAfterTime.Add(step.Duration.DurationAfterOp);

            List<Tank> tankAvailable = null;
            List<Operator> operatorAvailableTank = null;

            // S'il s'agit de la dernière étape, on recherche une cuve pour l'OF
            if (lastStep)
            {
                tankAvailable = new(Available.FindTankForStep(SolutionPlanning.PlanningTank, DataParsed.Tanks, endOpAfterTime, endOpAfterTime.Add(new(0, 10, 0))));
                operatorAvailableTank = new(Available.FindOperatorForTank(SolutionPlanning.PlanningOperator, DataParsed.Operators, endOpAfterTime));
            }

            List<Operator> operatorAvailableBeforeOp = new(Available.FindOperator(SolutionPlanning.PlanningOperator, DataParsed.Operators, beginningOpBeforeTime, endOpBeforeTime, step.TypeMachineNeeded));
            List<Operator> operatorAvailableAfterOp = new(Available.FindOperator(SolutionPlanning.PlanningOperator, DataParsed.Operators, beginningOpAfterTime, endOpAfterTime, step.TypeMachineNeeded));
            List<Machine> machineAvailable = new(Available.FindMachineForStep(SolutionPlanning.PlanningMachine, DataParsed.Machine, beginningOpBeforeTime, endOpAfterTime, step.TypeMachineNeeded));
            
            bool consomableAvailable = Available.FindConsoForStep(SolutionPlanning.PlanningCons, DataParsed.Consummables, time, step.ConsumableUsed, step.QuantityConsumable);

            // Si une des ressources est indisponible on passe 5 minutes plus tard
            if(lastStep && (operatorAvailableTank.Count == 0 || tankAvailable.Count == 0))
                return SearchRessources(time.AddMinutes(5.0), step, lastStep);

            if (operatorAvailableBeforeOp.Count == 0 ||
                operatorAvailableAfterOp.Count == 0 ||
                machineAvailable.Count == 0)
            {
                return SearchRessources(time.AddMinutes(5.0), step, lastStep);
            }

            // S'il manque des consommables on passe au jour d'après
            if (!consomableAvailable)
            {
                while (time.Hour != data.Operators[0].Beginning.Hour)
                {
                    time = time.AddMinutes(5.0);
                }
                return SearchRessources(time, step, lastStep);
            }

            DateTime endOperation = endOpAfterTime.Add(machineAvailable[0].Duration_cleaning);

            // On cherche un operateur pour nettoyer la machine choisie 
            List <Operator> operatorAvailableCleaning = new(Available.FindOperator(SolutionPlanning.PlanningOperator, DataParsed.Operators, endOpAfterTime, endOperation, TypeMachine.cleaning));

            if(operatorAvailableCleaning.Count == 0)
                return SearchRessources(time.AddMinutes(5.0), step, lastStep);

            List<Object> listRessourcesAvailable = new();

            // On choisit la premiere ressource de chaque liste

            if(lastStep)
                listRessourcesAvailable.Add(endOperation.Add(new(0, 10, 0)));
            else
                listRessourcesAvailable.Add(endOperation);

            listRessourcesAvailable.Add(operatorAvailableBeforeOp[0]);
            listRessourcesAvailable.Add(operatorAvailableAfterOp[0]);
            listRessourcesAvailable.Add(operatorAvailableCleaning[0]);
            listRessourcesAvailable.Add(machineAvailable[0]);
            listRessourcesAvailable.Add(consomableAvailable);

            if (lastStep)
            {
                listRessourcesAvailable.Add(operatorAvailableTank[0]);
                listRessourcesAvailable.Add(tankAvailable[0]);
            }

            // On renvoie une liste d'objet contenant toutes les ressources
            return listRessourcesAvailable;
        }

        public SolutionPlanning ScheduleStep(List<Object> ressourceList, SolutionPlanning solutionPlanning, DateTime time, OF oF, Step step, Boolean lastStep, OF oFBefore)
        {
            DateTime dayTime = time.Date;
            string code;

            DateTime beginningOpBeforeTime = time;
            DateTime endOpBeforeTime = time.Add(step.Duration.DurationBeforeOp);
            DateTime beginningOpAfterTime = endOpBeforeTime.Add(step.Duration.DurationOp);
            DateTime endOpAfterTime = beginningOpAfterTime.Add(step.Duration.DurationAfterOp);
            
            Operator operatorBefore = (Operator)ressourceList[1];
            Operator operatorAfter = (Operator)ressourceList[2];
            Operator operatorNetMachine = (Operator)ressourceList[3];
            Machine machine = (Machine)ressourceList[4];
            bool consumable = (bool)ressourceList[5];

            Operator operatorTank = null;
            
            // Planification OperateurAvant
            List<Object> listOpBefore = new();
            listOpBefore.Add(dayTime);
            listOpBefore.Add(beginningOpBeforeTime);
            listOpBefore.Add(endOpBeforeTime);
            code = "OPBefore";
            listOpBefore.Add(code);
            listOpBefore.Add(oF.IdOF);
            listOpBefore.Add(operatorBefore.Id);

            // Ajout dans la planification du jour
            solutionPlanning.PlanningOperator.Add(listOpBefore);

            // Planification OperateurApres
            List<Object> listOpAfter = new();
            listOpAfter.Add(dayTime);
            listOpAfter.Add(beginningOpAfterTime);
            listOpAfter.Add(endOpAfterTime);
            code = "OPAfter";
            listOpAfter.Add(code);
            listOpAfter.Add(oF.IdOF);
            listOpAfter.Add(operatorAfter.Id);

            // Ajout dans la planification du jour
            solutionPlanning.PlanningOperator.Add(listOpAfter);

            // Planification OperateurNettoyage Machine
            List<Object> listOpCleaning = new();
            listOpCleaning.Add(dayTime);
            listOpCleaning.Add(endOpAfterTime);
            listOpCleaning.Add(endOpAfterTime.Add(machine.Duration_cleaning));
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
                operatorTank = (Operator)ressourceList[6];
                Tank tank = (Tank)ressourceList[7];

                TimeSpan timeSpan = Available.FindTimeCleaningTank(oFBefore, oF, tank);

                listOpTank = new();
                listOpTank.Add(dayTime);
                listOpTank.Add(endOpAfterTime.Add(machine.Duration_cleaning));
                listOpTank.Add(endOpAfterTime.Add(machine.Duration_cleaning).Add(timeSpan));
                code = "OPNetTank";
                listOpTank.Add(code);
                listOpTank.Add(tank.IdTank);
                listOpTank.Add(operatorTank.Id);

                // Planification Tank
                listTank = new();
                listTank.Add(dayTime);
                listTank.Add(endOpAfterTime.Add(machine.Duration_cleaning));
                listTank.Add(endOpAfterTime.Add(machine.Duration_cleaning).Add(timeSpan));
                listTank.Add(timeSpan);
                listTank.Add(oF.IdOF);
                listTank.Add(operatorTank.Id);
                listTank.Add(tank.IdTank);

                // Planification OF
                List<Object> listOF = new();
                listOF.Add(dayTime);
                listOF.Add(tank.IdTank);
                listOF.Add(step);
                listOF.Add(machine.Id);
                listOF.Add(operatorBefore);
                listOF.Add(operatorAfter);
                listOF.Add(oF.Next_step);

                if (oF.Next_step > 0)
                {
                    listOF.Add(step.IdStep);
                }

                solutionPlanning.PlanningOF.Add(listOF);
                solutionPlanning.PlanningOperator.Add(listOpTank);
                solutionPlanning.PlanningTank.Add(listTank);
            }

            // Planification Machine
            List<Object> listMachine = new();
            listMachine.Add(dayTime);
            listMachine.Add(endOpBeforeTime);
            listMachine.Add(beginningOpAfterTime);
            listMachine.Add(oF.IdOF);
            listMachine.Add(operatorBefore.Id);
            listMachine.Add(operatorAfter.Id);
            listMachine.Add(machine.Id);

            solutionPlanning.PlanningMachine.Add(listMachine);

            // Planification Consommable
            List<Object> listConsumable = new();

            int count = 0;
            foreach (Consumable consumables in step.ConsumableUsed)
            {
                listConsumable.Add(dayTime);
                listConsumable.Add(step.QuantityConsumable[count]);
                listConsumable.Add(step.ConsumableUsed[count].Id);
                solutionPlanning.PlanningCons.Add(new(listConsumable));
                listConsumable.Clear();
                count++;
            }

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
        public int StepAlgorithm(DateTime time) //todo NetMachine and opbefore
        {
            int nbCteMaxViole = 0; // Il s'agit d'une constante qui va nous permettre de savoir le nombre de contraintes violés et permettra de comparer des heuristics
            bool lastStep = false;

            // S'il on trouve dans un weekend on passe un jour
            while (time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday)
                time = time.AddDays(1);

            int countOF = -1;
            int countStep = 0;
            OF ofBefore = null;
            DateTime currentTime = time;
            List<Object> resultRessources;
            bool OFinProgress = false;
            DateTime dti;
            SolutionPlanning planningAujourd = new();

            foreach (OF oF in DataParsed.OFs)
            {
                // S'il on trouve dans un weekend on passe un jour
                while (currentTime.DayOfWeek == DayOfWeek.Saturday || currentTime.DayOfWeek == DayOfWeek.Sunday)
                    currentTime.AddDays(1);

                if (countOF >= 0)
                    ofBefore = DataParsed.OFs[countOF];

                // On regarde si l'OF est en cours
                if (oF.Starting_hour == DateTime.MinValue)
                {
                    dti = oF.EarliestDate;
                }
                else
                {
                    dti = oF.Starting_hour;
                    OFinProgress = true;
                }

                // Prend en compte les jours au plus tot
                while (dti.Day >= currentTime.Day && dti.Month >= currentTime.Month && dti.Year >= currentTime.Year)
                {
                    currentTime.AddMinutes(5);
                }

            // Permet le reset de la loop
            restart:
                foreach (Step step in oF.StepSequence)
                {
                    // Si l'OF est en cours

                    if(oF.Next_step >= 1)
                        if (oF.StepSequence[countStep] == oF.StepSequence[oF.Next_step])        
                            OFinProgress = false;


                    // Etape déja faite, on passe à l'étape suivante
                    if (OFinProgress)
                        continue;

                    // On regarde s'il s'agit de la derniere etape
                    if (countStep + 1 == oF.StepSequence.Count)
                        lastStep = true;

                    // On recherche si les ressources sont disponibles
                    resultRessources = new(SearchRessources(currentTime, step, lastStep));

                    DateTime timeNeeded = (DateTime)resultRessources[0];

                    // On arrive a la fin de la journée et l'étape est reportable
                    if ((timeNeeded.Hour > data.Operators[0].End.Hour || (timeNeeded.Minute > data.Operators[0].End.Minute && timeNeeded.Hour == data.Operators[0].End.Hour)) && step.NextStepReportable)
                    {
                        while (currentTime.Hour != data.Operators[0].Beginning.Hour)
                            currentTime = currentTime.AddMinutes(5);

                        resultRessources = SearchRessources(currentTime, step, lastStep);
                    }

                    // on arrive a la fin de la journée et l'étape n'est pas reportable, on change de jour et on annule la reservation de l'OF
                    if ((timeNeeded.Hour > data.Operators[0].End.Hour || (timeNeeded.Minute > data.Operators[0].End.Minute && timeNeeded.Hour == data.Operators[0].End.Hour)) && !step.NextStepReportable)
                    {
                        planningAujourd = new();

                        // Sinon on passe au jour suivant a l'heure de debut d'un employé
                        while (currentTime.Hour != data.Operators[0].Beginning.Hour)
                            currentTime = currentTime.AddMinutes(5);

                        // On va au reset
                        goto restart;
                    }

                    // Si on respecte pas la contrainte de jour au plus tard, on incrémente nbCteMaxViole
                    if (timeNeeded.Day > oF.LatestDate.Day || timeNeeded.Month > oF.LatestDate.Month)
                        nbCteMaxViole++;

                    // Panifier l'étape courante a t'
                    planningAujourd = ScheduleStep(resultRessources, planningAujourd, currentTime, oF, step, lastStep, ofBefore);

                    Machine machine = (Machine)resultRessources[4];
                    

                    // On ajoute le temps passé
                    if (!lastStep)
                        currentTime = currentTime.Add(step.Duration.DurationOp + step.Duration.DurationBeforeOp + step.Duration.DurationAfterOp + machine.Duration_cleaning);
                    else
                    {
                        Tank tank = (Tank)resultRessources[7];
                        TimeSpan timeSpan = Available.FindTimeCleaningTank(ofBefore, oF, tank);
                        currentTime = currentTime.Add(step.Duration.DurationOp + step.Duration.DurationBeforeOp + step.Duration.DurationAfterOp + machine.Duration_cleaning + timeSpan);
                    }

                    countStep++;
                }
                // Mise a jour des compteurs
                countStep = 0;
                countOF++;

                // Ajout de l'OF dans le planning
                plannings.Add(planningAujourd);
                planningAujourd = new();
            }

            return nbCteMaxViole;
        }
    }
}