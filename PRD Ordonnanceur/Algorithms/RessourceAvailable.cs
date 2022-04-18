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
        /// Methods that search if an operator is available for a tank
        /// </summary>
        /// <param name="planningOperator"></param>
        /// <param name="listOperator"></param>
        /// <param name="timeNow"></param>
        /// <returns>return a list of operator available</returns>
        public static List<Operator> FindOperatorForTank(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime timeNow)
        {
            if (planningOperator.Count == 0)
                return listOperator;
            else if (listOperator.Count == 0)
                return new();

            int count = 0;

            foreach (List<Object> list in planningOperator)
            {
                count += list.Count;
            }

            if (count == 0)
                return listOperator;
            else if (listOperator.Count == 0)
                return new();

            List<Operator> listOperatorAvailable = listOperator;

            foreach (List<Object> list in planningOperator)
            {
                if (list[2] is DateTime && timeNow <= (DateTime)list[2])
                {
                    int id = (int)list[0];
                    int count2 = 0;
                    int index = -1;
                    foreach (Operator op in listOperatorAvailable)
                    {
                        if (op.Uid == id)
                        {
                            index = count2;
                        }
                        count2++;
                    }
                    listOperatorAvailable.RemoveAt(index);
                }
            }

            return listOperatorAvailable;
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

            List<Operator> listOperatorAvailable = new(listOperator);

            List<Operator> tempList = new(listOperatorAvailable);

            // On retire tous les opérateurs indisponibles par leur compétences / emploi du temps 
            foreach (Operator operat in listOperatorAvailable)
            {
                bool hasSkill = false;
                bool hasTime = false;

                if ((operat.StartWorkSchedule.Minute <= beginningTimeOfOperation.Minute && operat.StartWorkSchedule.Hour <= beginningTimeOfOperation.Hour) || (operat.End.Minute <= endTimeOfOperation.Minute && operat.End.Hour <= endTimeOfOperation.Hour))
                    hasTime = true;
                
                foreach (TypeMachine skill in operat.SkillSet)
                {
                    if (skill.CompareTo(Competence) == 0)
                        hasSkill = true;
                }

                if (!hasSkill || !hasTime)
                {
                    tempList.Remove(operat);
                }
            }

            listOperatorAvailable = new(tempList);

            int count = 0;

            // verification si plannings null
            foreach (SolutionPlanning planning in plannings)
            {
                foreach (List<Object> list in planning.PlanningOperator)
                {
                    count += list.Count;
                }
            }

            if (listOperatorAvailable.Count == 0)
                return new();

            if (count == 0)
                return listOperatorAvailable;

            if (listOperator.Count == 0)
                throw new("Liste Operateur Vide");

            tempList = new(listOperatorAvailable);
            
            foreach(SolutionPlanning planning in plannings)
            {
                foreach (Operator currentOperator in listOperatorAvailable)
                {
                    // Enlever les opérateurs indisponibles par leur heure de travail
                    foreach (List<Object> list in planning.PlanningOperator)
                    {
                        // Trouvé l'id de l'operateur en fonction du planning operateur
                        // si différent on passe 
                        if ((uint)list[5] != currentOperator.Uid)
                            continue;

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

            listOperatorAvailable = new(tempList);

            return listOperatorAvailable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planningMachine"></param>
        /// <param name="listMachine"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <param name="typeMachine"></param>
        /// <returns></returns>
        public static List<Machine> FindMachineForStep(List<List<Object>> planningMachine, List<Machine> listMachine, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation, TypeMachine typeMachine)
        {
            int count = 0;

            foreach (List<Object> list in planningMachine)
            {
                count += list.Count;
            }

            List<Machine> listMachineAvailable = new(listMachine);

        reset:
            // On retire toutes les machines indisponibles par leur compétences
            foreach (Machine operat in listMachineAvailable)
            {
                if (operat.TypeMachine.CompareTo(typeMachine) != 0)
                {
                    listMachineAvailable.Remove(operat);
                    goto reset;
                }
            }

            if (count == 0)
                return listMachine;
            else if (listMachine.Count == 0)
                return new();

            foreach (List<Object> list in planningMachine)
            {
                bool hasTime = false;

                if (beginningTimeOfOperation < (DateTime)list[1] && endTimeOfOperation <= (DateTime)list[1])
                    hasTime = true;

                if (beginningTimeOfOperation >= (DateTime)list[2] && endTimeOfOperation > (DateTime)list[2])
                    hasTime = true;

                // On retire toutes les machines indisponibles sur le temps
                if (!hasTime)
                {
                    // On supprime la machine de la liste available
                    int id = (int)list[5];
                    int count2 = 0;
                    int index = -1;
                    foreach (Machine op in listMachineAvailable)
                    {
                        if (op.Id == id)
                        {
                            index = count2;
                        }
                        count2++;
                    }
                    listMachineAvailable.RemoveAt(index);
                }
            }
            return listMachineAvailable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="planningTank"></param>
        /// <param name="listTank"></param>
        /// <param name="beginningTimeOfOperation"></param>
        /// <param name="endTimeOfOperation"></param>
        /// <returns></returns>
        public static List<Tank> FindTankForStep(List<List<Object>> planningTank, List<Tank> listTank, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation)
        {
            int count = 0;

            foreach (List<Object> list in planningTank)
            {
                count += list.Count;
            }

            if (count == 0)
                return listTank;
            else if (planningTank.Count == 0)
                return new();

            List<Tank> listTankAvailable = new(listTank);

        reset:

            foreach (List<Object> list in planningTank)
            {
                bool hasTime = false;

                if (beginningTimeOfOperation < (DateTime)list[1] && endTimeOfOperation <= (DateTime)list[1])
                    hasTime = true;

                if (beginningTimeOfOperation >= (DateTime)list[2] && endTimeOfOperation > (DateTime)list[2])
                    hasTime = true;

                if (!hasTime)
                {
                    // On supprime l'operateur de la liste available
                    foreach (Tank op in listTankAvailable)
                    {
                        listTankAvailable.Remove(op);
                        goto reset;
                    }
                }
            }

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
                double quantityUsed = (double)list[1];
                int idConsumable = (int)list[2];

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
    }
}