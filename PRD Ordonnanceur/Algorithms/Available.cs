﻿using PRD_Ordonnanceur.Data;
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

        public List<Operator> FindOperator(List<List<Object>> planningOperator, List<Operator> listOperator, DateTime beginningTimeOfOperation, TypeMachine Competence)
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

            foreach (List<Object> list in planningOperator)
            {
                // On retire tous les opérateurs indisponibles sur le temps
                if (list[2] is DateTime)
                {
                    if(beginningTimeOfOperation <= (DateTime)list[2])
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

        // TODO changer les consommables pour plusieurs consommables
        public bool FindConsoForStep(List<List<Object>> planningConso, List<Consumable> listComsumable, DateTime timeNow, Consumable consumable,int quantity)
        {
            // On recherche dans le planning la quantité restante de consommable restant
            bool enoughRessources = false;

            // On retourne 
            List<Consumable> consumables = new();

            foreach(List<Object> list in planningConso)
            {
                if((int) list[0] == consumable.Id && (int) list[1] >= quantity)
                {
                    enoughRessources = true;
                }
            }

            return enoughRessources;
        }

        public TimeSpan FindTimeCleaningTank(OF oFBefore, OF oFAfter, Tank tank)
        {

            return TimeSpan.MinValue;
        }

    }
}