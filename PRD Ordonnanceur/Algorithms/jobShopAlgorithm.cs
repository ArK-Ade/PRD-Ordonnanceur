using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Parser;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    /// <summary>
    /// Class that represent the sheduling algorithm
    /// </summary>
    public class JobShopAlgorithm
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public JobShopAlgorithm()
        {
            this.DataParsed = new();
            this.AvailableAlgorithm = new();
            this.Plannings = new();
            this.SolutionPlanning = new();
        }

        /// <summary>
        /// Confortable Constructor
        /// </summary>
        /// <param name="data"></param>
        /// <param name="availableAlgorithm"></param>
        /// <param name="plannings"></param>
        /// <param name="solutionPlanning"></param>
        public JobShopAlgorithm(DataParsed data, RessourceAvailable availableAlgorithm, List<SolutionPlanning> plannings, SolutionPlanning solutionPlanning)
        {
            this.DataParsed = data;
            this.AvailableAlgorithm = availableAlgorithm;
            this.Plannings = plannings;
            this.SolutionPlanning = solutionPlanning;
        }

        /// <summary>
        /// data that has been retrieved by the parser
        /// </summary>
        public DataParsed DataParsed { get; set; }

        /// <summary>
        /// List of planning
        /// </summary>
        public List<SolutionPlanning> Plannings { get; set; }

        /// <summary>
        /// Current planning for the OF
        /// </summary>
        public SolutionPlanning SolutionPlanning { get; set; }

        /// <summary>
        /// Tool to verify if the ressource are available
        /// </summary>
        public RessourceAvailable AvailableAlgorithm { get; set; }

        /// <summary>
        /// It searchs the ressources necessery for the algorythm
        /// </summary>
        /// <param name="time"></param>
        /// <param name="step"></param>
        /// <param name="lastStep"></param>
        /// <returns>Return a list of all the ressource selected</returns>
        public List<Object> SearchRessources(DateTime time, Step step, bool lastStep)
        {
            DateTime beginningOpBeforeTime = time;
            DateTime endOpBeforeTime = time.Add(step.Duration.DurationBeforeOp);
            DateTime beginningOpAfterTime = endOpBeforeTime.Add(step.Duration.DurationOp);
            DateTime endOpAfterTime = beginningOpAfterTime.Add(step.Duration.DurationAfterOp);

            List<Tank> tankAvailable = null;
            List<Operator> operatorAvailableTank = null;

            // If it's the last step, we look for a tank
            if (lastStep)
            {
                tankAvailable = new(RessourceAvailable.FindTankForStep(SolutionPlanning.PlanningTank, DataParsed.Tanks, endOpAfterTime, endOpAfterTime.Add(new(0, 10, 0))));
                operatorAvailableTank = new(RessourceAvailable.FindOperatorForTank(SolutionPlanning.PlanningOperator, DataParsed.Operators, endOpAfterTime));
            }

            List<Operator> operatorAvailableBeforeOp = new(RessourceAvailable.FindOperator(Plannings, DataParsed.Operators, beginningOpBeforeTime, endOpBeforeTime, step.TypeMachineNeeded));
            List<Operator> operatorAvailableAfterOp = new(RessourceAvailable.FindOperator(Plannings, DataParsed.Operators, beginningOpAfterTime, endOpAfterTime, step.TypeMachineNeeded));
            List<Machine> machineAvailable = new(RessourceAvailable.FindMachineForStep(SolutionPlanning.PlanningMachine, DataParsed.Machine, beginningOpBeforeTime, endOpAfterTime, step.TypeMachineNeeded));
            
            bool consomableAvailable = RessourceAvailable.FindConsoForStep(SolutionPlanning.PlanningCons, DataParsed.Consummables, time, step.ConsumableUsed, step.QuantityConsumable);

            // If one of the ressources are unavailable
            if(lastStep && (operatorAvailableTank.Count == 0 || tankAvailable.Count == 0))
                return new();

            if (operatorAvailableBeforeOp.Count == 0 ||
                operatorAvailableAfterOp.Count == 0 ||
                machineAvailable.Count == 0)
            {
                return new();
            }

            // If they are no consumable
            if (!consomableAvailable)
            {
                return new();
            }

            DateTime endOperation = endOpAfterTime.Add(machineAvailable[0].CleaningDuration);
 
            List <Operator> operatorAvailableCleaning = new(RessourceAvailable.FindOperator(Plannings, DataParsed.Operators, endOpAfterTime, endOperation, TypeMachine.cleaning));

            if(operatorAvailableCleaning.Count == 0)
                return new();

            List<Object> listRessourcesAvailable = new();

            // We randomly choose one ressource from each list
            if (lastStep)
                listRessourcesAvailable.Add(endOperation.Add(new(0, 10, 0)));
            else
                listRessourcesAvailable.Add(endOperation);

            Random rnd = new();
            int randomIndex = rnd.Next(0, operatorAvailableBeforeOp.Count);
            listRessourcesAvailable.Add(operatorAvailableBeforeOp[randomIndex]);

            randomIndex = rnd.Next(0, operatorAvailableAfterOp.Count);
            listRessourcesAvailable.Add(operatorAvailableAfterOp[randomIndex]);

            randomIndex = rnd.Next(0, operatorAvailableCleaning.Count);
            listRessourcesAvailable.Add(operatorAvailableCleaning[randomIndex]);

            randomIndex = rnd.Next(0, machineAvailable.Count);
            listRessourcesAvailable.Add(machineAvailable[randomIndex]);
            listRessourcesAvailable.Add(consomableAvailable);

            if (lastStep)
            {
                randomIndex = rnd.Next(0, operatorAvailableTank.Count);
                listRessourcesAvailable.Add(operatorAvailableTank[randomIndex]);

                randomIndex = rnd.Next(0, tankAvailable.Count);
                listRessourcesAvailable.Add(tankAvailable[randomIndex]);
            }

            return listRessourcesAvailable;
        }

        /// <summary>
        /// Method that schedule the step
        /// </summary>
        /// <param name="ressourceList"></param>
        /// <param name="solutionPlanning"></param>
        /// <param name="time"></param>
        /// <param name="oF"></param>
        /// <param name="step"></param>
        /// <param name="lastStep"></param>
        /// <param name="oFBefore"></param>
        /// <returns>The planning of the step</returns>
        public SolutionPlanning ScheduleStep(List<Object> ressourceList, SolutionPlanning solutionPlanning, DateTime time, OF oF, Step step, Boolean lastStep, OF oFBefore)
        {
            DateTime dayTime = time.Date;
            string code;

            Operator operatorBefore = (Operator)ressourceList[1];
            Operator operatorAfter = (Operator)ressourceList[2];
            Operator operatorNetMachine = (Operator)ressourceList[3];
            Machine machine = (Machine)ressourceList[4];
            bool consumable = (bool)ressourceList[5];

            DateTime beginningOpBeforeTime = time;
            DateTime endOpBeforeTime = time.Add(step.Duration.DurationBeforeOp);
            DateTime beginningOpAfterTime = endOpBeforeTime.Add(step.Duration.DurationOp);
            DateTime endOpAfterTime = beginningOpAfterTime.Add(step.Duration.DurationAfterOp);
            DateTime endCleaning = endOpAfterTime.Add(machine.CleaningDuration);


            Operator operatorTank = null;
            
            // Scheduling Operator before operation
            List<Object> listOpBefore = new();
            listOpBefore.Add(dayTime);
            listOpBefore.Add(beginningOpBeforeTime);
            listOpBefore.Add(endOpBeforeTime);
            code = "OPBefore";
            listOpBefore.Add(code);
            listOpBefore.Add(oF.Uid);
            listOpBefore.Add(operatorBefore.Uid);
            solutionPlanning.PlanningOperator.Add(listOpBefore);

            // Scheduling Operator after operation
            List<Object> listOpAfter = new();
            listOpAfter.Add(dayTime);
            listOpAfter.Add(beginningOpAfterTime);
            listOpAfter.Add(endOpAfterTime);
            code = "OPAfter";
            listOpAfter.Add(code);
            listOpAfter.Add(oF.Uid);
            listOpAfter.Add(operatorAfter.Uid);
            solutionPlanning.PlanningOperator.Add(listOpAfter);

            // Scheduling cleaning
            List<Object> listOpCleaning = new();
            listOpCleaning.Add(dayTime);
            listOpCleaning.Add(endOpAfterTime);
            listOpCleaning.Add(endCleaning);
            code = "OPNetMachine";
            listOpCleaning.Add(code);
            listOpCleaning.Add(machine.Id);
            listOpCleaning.Add(operatorNetMachine.Uid);
            solutionPlanning.PlanningOperator.Add(listOpCleaning);

            List<Object> listOpTank = null;
            List<Object> listTank = null;

            if (lastStep)
            {
                operatorTank = (Operator)ressourceList[6];
                Tank tank = (Tank)ressourceList[7];

                TimeSpan timeSpan = RessourceAvailable.FindTimeCleaningTank(oFBefore, oF, tank);

                // Scheduling operator for the tank
                listOpTank = new();
                listOpTank.Add(dayTime);
                listOpTank.Add(endCleaning);
                listOpTank.Add(endCleaning.Add(timeSpan));
                code = "OPNetTank";
                listOpTank.Add(code);
                listOpTank.Add(tank.IdTank);
                listOpTank.Add(operatorTank.Uid);

                // Scheduling the tank
                listTank = new();
                listTank.Add(dayTime);
                listTank.Add(endCleaning);
                listTank.Add(endCleaning.Add(timeSpan));
                listTank.Add(timeSpan);
                listTank.Add(oF.Uid);
                listTank.Add(operatorTank.Uid);
                listTank.Add(tank.IdTank);

                // Scheduling the OF
                List<Object> listOF = new();
                listOF.Add(dayTime);
                listOF.Add(tank.IdTank);
                listOF.Add(step);
                listOF.Add(machine.Id);
                listOF.Add(operatorBefore);
                listOF.Add(operatorAfter);
                listOF.Add(oF.NextStep);

                if (oF.NextStep > 0)
                {
                    listOF.Add(step.Uid);
                }

                solutionPlanning.PlanningOF.Add(listOF);
                solutionPlanning.PlanningOperator.Add(listOpTank);
                solutionPlanning.PlanningTank.Add(listTank);
            }

            // Scheduling the machine
            List<Object> listMachine = new();
            listMachine.Add(dayTime);
            listMachine.Add(beginningOpBeforeTime);
            listMachine.Add(endOpAfterTime);
            listMachine.Add(oF.Uid);
            listMachine.Add(operatorBefore.Uid);
            listMachine.Add(operatorAfter.Uid);
            listMachine.Add(machine.Id);

            solutionPlanning.PlanningMachine.Add(listMachine);

            // Scheduling consumable
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

            // Scheduling step
            List<Object> listStep = new();
            listStep.Add(step.Uid);
            listStep.Add(beginningOpBeforeTime);
            if(!lastStep)
                listStep.Add(endOpAfterTime);
            else
            {
                Tank tank = (Tank)ressourceList[7];
                TimeSpan timeSpan = RessourceAvailable.FindTimeCleaningTank(oFBefore, oF, tank);
                listStep.Add(endCleaning.Add(timeSpan));
            }
            solutionPlanning.PlanningStep.Add(new(listStep));

            return solutionPlanning;
        }

        /// <summary>
        /// Algorithm who planify the ressources
        /// </summary>
        /// <param name="time"></param>
        /// <returns>The number of constraint not met</returns>
        public int StepAlgorithm(DateTime time)
        {
            int nbConstrainNotRespected = 0;
            bool lastStep = false;

            int countOF = -1;
            int countStep = 0;
            OF ofBefore = null;
            DateTime currentTime = time;
            List<Object> resultRessources = new();
            bool OFinProgress = false;
            DateTime dti;
            SolutionPlanning currentPlanning = new();

            foreach (OF oF in DataParsed.OFs)
            {
                if (countOF >= 0)
                    ofBefore = DataParsed.OFs[countOF];

                // if the OF has already begun
                if (oF.StartingHour == DateTime.MinValue)
                {
                    dti = oF.EarliestDate;
                }
                else
                {
                    dti = oF.StartingHour;
                    OFinProgress = true;
                }

                // Looking if we can start the OF
                while (dti.Day >= currentTime.Day && dti.Month >= currentTime.Month && dti.Year >= currentTime.Year)
                {
                    currentTime = currentTime.AddMinutes(5);
                }

            restart:
                foreach (Step step in oF.StepSequence)
                {
                    // if OF already begun
                    if(oF.NextStep >= 1 && oF.StepSequence[countStep] == oF.StepSequence[oF.NextStep])      
                        OFinProgress = false;

                    // Etape déja faite, on passe à l'étape suivante
                    if (OFinProgress)
                        continue;

                    // On regarde s'il s'agit de la derniere etape
                    if (countStep + 1 == oF.StepSequence.Count)
                        lastStep = true;

                    // Looking if the ressources are available
                    while(resultRessources.Count == 0)
                    {
                        resultRessources = new(SearchRessources(currentTime, step, lastStep));

                        if (resultRessources.Count == 0)
                            currentTime = currentTime.AddMinutes(5);
                    }

                    DateTime timeNeeded = (DateTime)resultRessources[0];

                    // We arrive at the end of the day and the stage is postable
                    if ((timeNeeded.Hour > DataParsed.Operators[0].End.Hour || (timeNeeded.Minute > DataParsed.Operators[0].End.Minute && timeNeeded.Hour == DataParsed.Operators[0].End.Hour)) && step.NextStepReportable)
                    {
                        while (currentTime.Hour != DataParsed.Operators[0].StartWorkSchedule.Hour)
                            currentTime = currentTime.AddMinutes(5);

                        resultRessources = SearchRessources(currentTime, step, lastStep);
                    }

                    // We arrive at the end of the day and the stage is not postponable, we change day and we cancel the reservation of the OF
                    if ((timeNeeded.Hour > DataParsed.Operators[0].End.Hour || (timeNeeded.Minute > DataParsed.Operators[0].End.Minute && timeNeeded.Hour == DataParsed.Operators[0].End.Hour)) && !step.NextStepReportable)
                    {
                        currentPlanning = new();

                        // We move to the next day at the start time of an employee
                        while (currentTime.Hour != DataParsed.Operators[0].StartWorkSchedule.Hour)
                            currentTime = currentTime.AddMinutes(5);

                        resultRessources.Clear();
                        nbConstrainNotRespected -= countStep;

                        goto restart;
                    }

                    // Si on respecte pas la contrainte de jour au plus tard, on incrémente nbCteMaxViole
                    if (timeNeeded.Day > oF.LatestDate.Day || timeNeeded.Month > oF.LatestDate.Month)
                        nbConstrainNotRespected++;

                    // Panifier l'étape courante a t'
                    currentPlanning = ScheduleStep(resultRessources, currentPlanning, currentTime, oF, step, lastStep, ofBefore);

                    Machine machine = (Machine)resultRessources[4];

                    // We add the time spent
                    if (!lastStep)
                        currentTime = timeNeeded;
                    else
                    {
                        Tank tank = (Tank)resultRessources[7];
                        TimeSpan timeSpan = RessourceAvailable.FindTimeCleaningTank(ofBefore, oF, tank);
                        currentTime = time;
                        lastStep = false;
                    }

                    resultRessources.Clear();
                    countStep++;
                }
                countStep = 0;
                countOF++;

                // Addition of the OF in the planning
                Plannings.Add(currentPlanning);
                currentPlanning = new();
            }

            return nbConstrainNotRespected;
        }
    }
}