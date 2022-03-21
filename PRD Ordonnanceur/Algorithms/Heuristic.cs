using PRD_Ordonnanceur.Data;
using System;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Heuristic : IHeuristic
    {
        public Heuristic()
        {
        }

        public int Smallest_index_DTI(OF[] OFs)
        {
            int index = 0;
            DateTime date = DateTime.MaxValue;
            int count = -1;

            foreach (OF oF in OFs)
            {
                count++;

                if (date > oF.EarliestDate)
                {
                    if (date == oF.EarliestDate)
                    {
                    }
                    date = oF.EarliestDate;
                    index = count;
                }
            }

            return index;
        }

        public OF[] SortCrescentDtiCrescentDli(OF[] OFs)
        {
            OF[] tmp = OFs;
            OF[] tableau = new OF[OFs.Length];

            int index;

            for (int i = 0; i < OFs.Length; i++)
            {
                index = Smallest_index_DTI(tmp);
                tableau[i] = tmp[index];

                tmp[index].EarliestDate = DateTime.MaxValue;
            }

            return tableau;
        }

        public OF[] Sort_crescent_dti_decendant_dli(OF[] OFs)
        {
            return OFs;
        }

        public OF[] Sort_random_5_crescent(OF[] OFs)
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