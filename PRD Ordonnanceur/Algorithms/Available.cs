using PRD_Ordonnanceur.Data;
using PRD_Ordonnanceur.Solution;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Available
    {
        // TODO terminer cette fonction
        public List<Operator> findOperatorForTank(List<Object> planningOperator, List<Operator> listOperator)
        {
            if(planningOperator.Count == 0)
                return listOperator;
            else if (listOperator.Count == 0)
                return null;
                        
            List<Operator> operatorss = new();
            return operatorss;


        }

        public List<Operator> findOperatorForStep(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime timeNow, TypeMachine Competence)
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

        public Machine[] findMachineForStep(object[,] machines)
        {
            Machine[] operatorss = new Machine[2];

            return operatorss;
        }

        public Tank[] findTankForStep(object[,] tanks)
        {
            Tank[] operatorss = new Tank[2];

            return operatorss;
        }

        public Consumable[] findConsoForStep(object[,] consomables)
        {
            Consumable[] operatorss = new Consumable[2];

            return operatorss;
        }
    }
}