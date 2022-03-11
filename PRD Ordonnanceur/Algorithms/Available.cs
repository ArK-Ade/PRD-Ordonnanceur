using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Available
    {
        public List<Operator> FindOperatorForTank(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime timeNow)
        {
            if (planningOperator.Count == 0)
                return listOperator;
            else if (listOperator.Count == 0)
                return null;

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
                // On retire tous les opérateurs indisponibles sur le temps
                if (list[2] is DateTime)
                {
                    if (timeNow <= (DateTime)list[2])
                    {
                        // On supprime l'operateur de la liste available
                        int id = (int)list[0];
                        int count2 = 0;
                        int index = -1;
                        foreach (Operator op in listOperatorAvailable)
                        {
                            if (op.Id == id)
                            {
                                index = count2;
                            }
                            count2++;
                        }
                        listOperatorAvailable.RemoveAt(index);
                    }
                }
            }

            return listOperatorAvailable;
        }

        public List<Operator> FindOperator(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation, TypeMachine Competence)
        {
            int count = 0;

            foreach (List<Object> list in planningOperator)
            {
                count += list.Count;
            }

            List<Operator> listOperatorAvailable = new(listOperator);

            reset:
            // On retire tous les opérateurs indisponibles par leur compétences
            foreach (Operator operat in listOperatorAvailable)
            {
                bool haveSkill = false;
                foreach (TypeMachine skill in operat.MachineSkill)
                {
                    if (skill.CompareTo(Competence) == 0)
                        haveSkill = true;
                }

                if (haveSkill == false)
                {
                    listOperatorAvailable.Remove(operat); // TODO Changer la gestion de suppression des tableaux list
                    goto reset;
                }
            }

            if (count == 0)
                return listOperator;
            else if (listOperator.Count == 0)
                return new();

            // Enlever les opérateurs indisponibles par leur heure de travail
            foreach (List<Object> list in planningOperator)
            {
                // On retire tous les opérateurs indisponibles sur le temps (planning)
                if (beginningTimeOfOperation >= (DateTime)list[1] && endTimeOfOperation <= (DateTime)list[2])
                {
                    // On supprime l'operateur de la liste available
                    uint id = (uint)list[5];
                    int count2 = 0;
                    int index = -1;
                    foreach (Operator op in listOperatorAvailable)
                    {
                        if (op.Id == id)
                            index = count2;
                        count2++;
                    }
                    listOperatorAvailable.RemoveAt(index);
                }
            }

            return listOperatorAvailable;
        }

        public List<Machine> FindMachineForStep(List<List<Object>> planningMachine, List<Machine> listMachine, DateTime beginningTimeOfOperation, DateTime endTimeOfOperation, TypeMachine typeMachine)
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
                // On retire toutes les machines indisponibles sur le temps
                if (beginningTimeOfOperation >= (DateTime)list[1] && endTimeOfOperation <= (DateTime)list[2])
                {
                    // On supprime la machine de la liste available
                    int id = (int)list[5]; //TODO Changer le format du planning des machines
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

        public List<Tank> FindTankForStep(List<List<Object>> planningTank, List<Tank> listTank, DateTime beginningTimeOfOperation)
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
                if (beginningTimeOfOperation >= (DateTime)list[2]) // TODO Changer cette condition
                {
                    // On supprime l'operateur de la liste available
                    int id = (int)list[0];
                    foreach (Tank op in listTankAvailable)
                    {
                        listTankAvailable.Remove(op);
                        goto reset;
                    }
                }
            }

            return listTankAvailable;
        }

        public bool FindConsoForStep(List<List<Object>> planningConso, List<Consumable> listComsumable, DateTime timeNow, List<Consumable> listConsumableNeeded, List<int> quantity)
        {
            // On recherche dans le planning la quantité restante de consommable restant
            bool consumubleIsAvailable = true;

            // Cas ou les list ne sont pas egaux
            if (listConsumableNeeded.Count != quantity.Count)
            {
                return false; //  TODO doit retourner une erreur
            }

            List<int> quantityRemaining = new(listConsumableNeeded.Count);
            int count = 0;

            // Remplissable de quantityRemaining
            foreach (Consumable consumable in listConsumableNeeded)
            {
                quantityRemaining.Add(consumable.QuantityAvailable); // TODO A corriger
                count++;
            }

            count = 0;

            // Pour chaque jour du planning on enleve les consommables utilisés
            foreach (List<Object> list in planningConso)
            {
                DateTime day = (DateTime)list[0];
                int quantityUsed = (int)list[1];
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
            foreach (int quantityUsed in quantityRemaining)
            {
                if (quantityUsed < 0)
                    consumubleIsAvailable = false;
            }

            return consumubleIsAvailable;
        }

        // TODO Terminer la fonction si utiliser
        public TimeSpan FindTimeCleaningTank(OF oFBefore, OF oFAfter, Tank tank)
        {
            return TimeSpan.MinValue;
        }
    }
}