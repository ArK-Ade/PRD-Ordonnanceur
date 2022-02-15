using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Available
    {
        // TODO Vérifier qu'il ne faut pas de compétences pour nettoyer une cuve
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

        // TODO Faire en sorte de prendre en compte la durée de travail a faire TimeSpan
        public List<Operator> FindOperatorForStep(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime timeNow, TypeMachine Competence)
        {
            int count = 0;

            foreach(List<Object> list in planningOperator)
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
                    if(timeNow <= (DateTime)list[2])
                    {

                        // On supprime l'operateur de la liste available
                        int id = (int)list[0];
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

        // TODO Terminer la fonction
        public List<Consumable> FindConsoForStep(List<List<Object>> planningConso, List<Consumable> listComsumable, DateTime timeNow, Consumable consumable,int quantity)
        {
            List<Consumable> operatorss = new();

            return operatorss;
        }

        public TimeSpan FindTimeCleaningTank(OF oFBefore, OF oFAfter, Tank tank)
        {

            return TimeSpan.MinValue;
        }

    }
}