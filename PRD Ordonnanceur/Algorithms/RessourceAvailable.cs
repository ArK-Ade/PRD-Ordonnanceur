using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    /// <summary>
    /// This class regroup all the method you need to use to verify if a ressource is available
    /// </summary>
    public class RessourceAvailable
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="listOperator"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <returns></returns>
        public static List<Operator> FindOperatorForTank(List<SolutionPlanning> plannings, List<Operator> listOperator, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation)
        {
            List<Operator> listOperatorAvailable = listOperator;
            return RessourceHasTime<Operator>(plannings, listOperatorAvailable, beginningTimeOfOperation, endTimeOfOperation);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="listOperator"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <param name="Competence"></param>
        /// <returns></returns>
        public static List<Operator> FindOperator(List<SolutionPlanning> plannings, List<Operator> listOperator, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation, TypeMachine Competence)
        {
            // Checking if it's a weekend
            if (beginningTimeOfOperation.DayOfWeek == DayOfWeek.Saturday || beginningTimeOfOperation.DayOfWeek == DayOfWeek.Sunday)
                return new();
            if (endTimeOfOperation.DayOfWeek == DayOfWeek.Saturday || endTimeOfOperation.DayOfWeek == DayOfWeek.Sunday)
                return new();

            List<Operator> listOperatorAvailable = new(listOperator), tempList = new(listOperator);

            // On retire tous les opérateurs indisponibles par leur compétences / emploi du temps
            foreach (Operator currentOperator in listOperatorAvailable)
            {
                bool hasSkill = false;
                bool hasTime = false;

                if ((currentOperator.StartOfShiftSchedule.Minute <= beginningTimeOfOperation.Minute && currentOperator.StartOfShiftSchedule.Hour <= beginningTimeOfOperation.Hour) || (currentOperator.EndOfShiftSchedule.Minute <= endTimeOfOperation.Minute && currentOperator.EndOfShiftSchedule.Hour <= endTimeOfOperation.Hour))
                    hasTime = true;

                foreach (TypeMachine skill in currentOperator.SkillSet)
                {
                    if (skill.CompareTo(Competence) == 0)
                        hasSkill = true;
                }

                if (!hasSkill || !hasTime)
                {
                    tempList.Remove(currentOperator);
                }
            }

            listOperatorAvailable = new(tempList);

            return RessourceHasTime<Operator>(plannings, listOperatorAvailable, beginningTimeOfOperation, endTimeOfOperation);
        }

        /// <summary>
        /// It searchs weither the ressource machine is available
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="listMachine"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <param name="typeMachine"></param>
        /// <returns>List that contain the available ressources</returns>
        public static List<Machine> FindMachineForStep(List<SolutionPlanning> plannings, List<Machine> listMachine, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation, TypeMachine typeMachine)
        {
            List<Machine> listMachineAvailable = new(listMachine);

            List<Machine> tempList = new(listMachine);

            // On retire toutes les machines indisponibles par leur compétences
            foreach (Machine operat in listMachineAvailable)
            {
                if (operat.TypeMachine.CompareTo(typeMachine) != 0)
                {
                    tempList.Remove(operat);
                }
            }

            listMachineAvailable = new(tempList);

            return RessourceHasTime<Machine>(plannings, listMachineAvailable, beginningTimeOfOperation, endTimeOfOperation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plannings"></param>
        /// <param name="listTank"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <returns></returns>
        public static List<Tank> FindTankForStep(List<SolutionPlanning> plannings, List<Tank> listTank, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation)
        {
            List<Tank> listTankAvailable = new(listTank);

            listTankAvailable = RessourceHasTime<Tank>(plannings, listTankAvailable, beginningTimeOfOperation, endTimeOfOperation);

            return listTankAvailable;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="planningConso"></param>
        /// <param name="listComsumable"></param>
        /// <param name="timeNow"></param>
        /// <param name="listConsumableNeeded"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static bool FindConsoForStep(List<List<Object>> planningConso, List<Consumable> listComsumable, DateTime timeNow, List<Consumable> listConsumableNeeded, List<double> quantity)
        {
            // On recherche dans le planning la quantité restante de consommable restant
            bool consumubleIsAvailable = true;

            // Cas ou les list ne sont pas egaux
            if (listConsumableNeeded.Count != quantity.Count)
            {
                string msg = "Il y a une erreur dans le parseur";
                Exception exception = new(msg);
                throw exception;
            }

            List<double> quantityRemaining = new(listConsumableNeeded.Count);
            int count = 0;

            // Remplissable de quantityRemaining
            foreach (Consumable consumable in listConsumableNeeded)
            {
                quantityRemaining.Add(consumable.QuantityAvailable);
                count++;
            }

            count = 0;

            // Pour chaque jour du planning on enleve les consommables utilisés
            foreach (List<Object> list in planningConso)
            {
                int idConsumable = (int)list[1];
                double quantityUsed = (double)list[2];

                foreach (Consumable consumable in listConsumableNeeded)
                {
                    if (consumable.Id == idConsumable)
                    {
                        quantityRemaining[count] -= quantityUsed;
                    }
                    count++;
                }
                count = 0;
            }

            // On verifie que les contraintes de quantités sont respectés
            foreach (double quantityUsed in quantityRemaining)
            {
                if (quantityUsed < 0)
                    consumubleIsAvailable = false;
            }

            return consumubleIsAvailable;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="oFBefore"></param>
        /// <param name="oFAfter"></param>
        /// <param name="tank"></param>
        /// <returns></returns>
        public static TimeSpan FindTimeCleaningTank(OF oFBefore, OF oFAfter, Tank tank)
        {
            return new(0, 10, 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="plannings"></param>
        /// <param name="listRessourcesAvailable"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <returns></returns>
        public static List<T> RessourceHasTime<T>(List<SolutionPlanning> plannings, List<T> listRessourcesAvailable, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation)
        {
            List<T> tempList = new(listRessourcesAvailable);
            List<List<Object>> solutionPlanning = new();

            foreach (SolutionPlanning planning in plannings)
            {
                foreach (T currentOperator in listRessourcesAvailable)
                {
                    if (typeof(T) == typeof(Operator))
                    {
                        solutionPlanning = planning.PlanningOperator;
                    }
                    else if (typeof(T) == typeof(Machine))
                    {
                        solutionPlanning = planning.PlanningMachine;
                    }
                    else if (typeof(T) == typeof(Tank))
                    {
                        solutionPlanning = planning.PlanningTank;
                    }

                    foreach (List<Object> list in solutionPlanning)
                    {
                        int uidObject = -1;
                        // Trouvé l'id de l'operateur en fonction du planning operateur
                        // si différent on passe
                        if (typeof(T) == typeof(Operator))
                        {
                            Operator operatorgrrg = (Operator)(object)currentOperator;
                            uidObject = (int)operatorgrrg.Uid;
                            if ((int)list[5] != uidObject)
                                continue;
                        }
                        else if (typeof(T) == typeof(Machine))
                        {
                            Machine machine = (Machine)(object)currentOperator;
                            uidObject = machine.Id;
                            if ((int)list[6] != uidObject)
                                continue;
                        }
                        else if (typeof(T) == typeof(Tank))
                        {
                            Tank tank = (Tank)(object)currentOperator;
                            uidObject = tank.IdTank;
                            if ((int)list[5] != uidObject)
                                continue;
                        }

                        bool hasTime = false;

                        if (beginningTimeOfOperation < (DateTime)list[1] && endTimeOfOperation <= (DateTime)list[1])
                            hasTime = true;

                        if (beginningTimeOfOperation >= (DateTime)list[2] && endTimeOfOperation > (DateTime)list[2])
                            hasTime = true;

                        // On retire tous les opérateurs indisponibles sur le temps (planning)
                        if (!hasTime)
                        {
                            // On supprime l'operateur de la liste available
                            tempList.Remove(currentOperator);
                        }
                    }
                }
            }
            return new(tempList);
        }
    }
}