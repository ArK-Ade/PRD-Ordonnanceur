using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    /// <summary>
    /// Class that represents the heuristic that will classify the OFs
    /// </summary>
    public class Heuristic : IHeuristic
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Heuristic()
        {
        }

        /// <summary>
        /// Methods that search the OF who can be schedule the earliest
        /// </summary>
        /// <param name="OFs"></param>
        /// <returns>index of the OF</returns>
        public int Smallest_index_DTI(List<OF> OFs)
        {
            int index = 0;
            DateTime date = DateTime.MaxValue;
            int count = -1;

            foreach (OF oF in OFs)
            {
                count++;

                if (date > oF.EarliestDate)
                {
                    date = oF.EarliestDate;
                    index = count;
                }
            }

            return index;
        }

        /// <summary>
        /// Methods that sort a list of OF
        /// </summary>
        /// <param name="OFs"></param>
        /// <returns></returns>
        public List<OF> SortCrescentDtiCrescentDli(List<OF> OFs)
        {
            List<OF> tmp = new(OFs);
            List<OF> tableau = new(OFs);

            int index;

            for (int i = 0; i < OFs.Count; i++)
            {
                index = Smallest_index_DTI(tmp);
                tableau[i] = new(tmp[index]);

                OF oF = new(tmp[index]);
                oF.EarliestDate = DateTime.MaxValue;
                tmp[index] = oF;
            }
            return tableau;
        }

        /// <summary>
        /// Methods that sort a list of OF
        /// </summary>
        /// <param name="OFs"></param>
        /// <returns></returns>
        public List<OF> Sort_crescent_dti_decendant_dli(List<OF> OFs)
        {
            return OFs;
        }

        /// <summary>
        /// Methods that sort a list of OF
        /// </summary>
        /// <param name="OFs"></param>
        /// <returns></returns>
        public List<OF> Sort_random_5_crescent(List<OF> OFs)
        {
            return OFs;
        }

        /// <summary>
        /// Methods that permit to change between the different heuristics
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="Ofs"></param>
        /// <returns></returns>
        List<OF> IHeuristic.SortingAlgorithm(int choice, List<OF> Ofs)
        {
            List<OF> newListOfs;

            switch (choice)
            {
                case 1:
                    newListOfs = new(SortCrescentDtiCrescentDli(Ofs));
                    break;

                case 2:
                    newListOfs = new(Sort_crescent_dti_decendant_dli(Ofs));
                    break;

                case 3:
                    newListOfs = new(Sort_random_5_crescent(Ofs));
                    break;

                default:
                    newListOfs = new(SortCrescentDtiCrescentDli(Ofs));
                    break;
            }
            return newListOfs;
        }
    }
}