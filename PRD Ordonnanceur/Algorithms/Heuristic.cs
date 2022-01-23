using PRD_Ordonnanceur.Data;
using System;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Heuristic : IHeuristic
    {
        public Heuristic()
        {

        }

        int Smallest_index_DTI(OF[] OFs)
        {

            int Index = -1;
            DateTime date = new DateTime(5000, 01, 01);
            int count = 0;

            foreach (OF oF in OFs)
            {
                if (date > oF.EarliestDate)
                {
                    date = oF.EarliestDate;
                    Index = count;
                }
                count++;
            }

            return Index;
        }

        OF[] SortCrescentDtiCrescentDli(OF[] OFs)
        {

            OF[] tableau = OFs;
            Array.Clear(tableau, 0, tableau.Length);

            OF earliest_OF = new();

            int index;

            for (int i = 0; i < OFs.Length; i++)
            {
                index = Smallest_index_DTI(OFs);
                tableau[i] = OFs[index];

                // TODO supprimer l'élément du tableau 
                OFs[index].EarliestDate = DateTime.MaxValue;
            }

            return tableau;
        }

        OF[] Sort_crescent_dti_decendant_dli(OF[] OFs)
        {
            return OFs;
        }

        OF[] Sort_random_5_crescent(OF[] OFs)
        {
            return OFs;
        }

        OF[] IHeuristic.SortingAlgorithm(int choice, OF[] Ofs)
        {
            switch (choice)
            {
                case 1:
                    SortCrescentDtiCrescentDli(Ofs);
                    break;

                case 2:
                    Sort_crescent_dti_decendant_dli(Ofs);
                    break;

                case 3:
                    Sort_random_5_crescent(Ofs);
                    break;

                default:
                    SortCrescentDtiCrescentDli(Ofs);
                    break;
            }
            return Ofs;
        }
    }
}
