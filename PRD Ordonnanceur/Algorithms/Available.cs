using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Available
    {
        public List<Operator> FindOperatorForTank(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime timeNow)
        {
            if(planningOperator.Count == 0)
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
                return null;

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

            foreach(List<Object> list in planningOperator)
            {
                count += list.Count;
            }

            if (count == 0)
                return listOperator;
            else if (listOperator.Count == 0) // TODO Retourne une erreur try catch a faire
                return null;

            List<Operator> listOperatorAvailable = listOperator;

            // TODO Enlever les opérateurs indisponibles par leur heure de travail

            foreach (List<Object> list in planningOperator)
            {
                // On retire tous les opérateurs indisponibles sur le temps (planning)
                if(beginningTimeOfOperation >= (DateTime)list[1] && endTimeOfOperation <= (DateTime)list[2])
                {
                    // On supprime l'operateur de la liste available
                    uint id = (uint)list[5];
                    int count2 = 0;
                    int index = -1;
                    foreach (Operator op in listOperatorAvailable)
                    {
                        if(op.Id == id)
                        {
                            index = count2;
                        }
                        count2++;
                    }
                    listOperatorAvailable.RemoveAt(index);
                }
                          
            }

            // On retire tous les opérateurs indisponibles par leur compétences
            foreach(Operator operat in listOperatorAvailable)
            {
                bool haveSkill = false;
                foreach(TypeMachine skill in operat.MachineSkill)
                {
                    if (skill == Competence)
                        haveSkill = true;
                }
                
                if(haveSkill == false)
                {
                    listOperatorAvailable.Remove(operat);
                }
            }

            return listOperatorAvailable;
        }
        
        public List<Machine> FindMachineForStep(List<List<Object>> planningMachine, List<Machine> listMachine, DateTime timeNow, TypeMachine typeMachine)
        {
            int count = 0;

            foreach (List<Object> list in planningMachine)
            {
                count += list.Count;
            }

            if (count == 0)
                return listMachine;
            else if (listMachine.Count == 0)
                return null;

            List<Machine> listMachineAvailable = listMachine;

            foreach (List<Object> list in planningMachine)
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
            }

            // On retire toutes les machines indisponibles par leur compétences
            foreach (Machine operat in listMachineAvailable)
            {
                count = 0;
                if (operat.TypeMachine == typeMachine)
                    listMachineAvailable.Remove(operat);
                count++;
            }

            return listMachineAvailable;
        }

        public List<Tank> FindTankForStep(List<List<Object>> planningTank, List<Tank> listTank, DateTime timeNow)
        {
            int count = 0;

            foreach (List<Object> list in planningTank)
            {
                count += list.Count;
            }

            if (count == 0)
                return listTank;
            else if (planningTank.Count == 0)
                return null;

            List<Tank> listTankAvailable = listTank;

            foreach (List<Object> list in planningTank)
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
                        foreach (Tank op in listTankAvailable)
                        {
                            if (op.IdTank == id)
                            {
                                index = count2;
                            }
                            count2++;
                        }
                        listTankAvailable.RemoveAt(index);
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
            if(listConsumableNeeded.Count != quantity.Count)
            {
                return false; //  TODO  doit retourner une erreur 
            }

            List<int> quantityRemaining = new(listConsumableNeeded.Count);
            int count = 0;

            // Remplissable de quantityRemaining
            foreach (Consumable consumable in listConsumableNeeded)
            {
                quantityRemaining[count] = consumable.QuantityAvailable;
                count++;
            }

            count = 0;
            
            // Pour chaque jour du planning on enleve les consommables utilisés
            foreach (List<Object> list in planningConso)
            {
                DateTime day = (DateTime) list[0];
                int quantityUsed = (int) list[1];
                int idConsumable = (int) list[2];
                
                foreach (Consumable consumable in listConsumableNeeded)
                {
                    if(consumable.Id == idConsumable)
                    {
                        quantityRemaining[count] -= quantityUsed;
                    }
                    count++;
                }
                count = 0;
            }

            // On verifie que les contraintes de quantités sont respectés
            foreach(int quantityUsed in quantityRemaining)
            {
                if(quantityUsed < 0)
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