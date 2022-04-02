using PRD_Ordonnanceur.Data;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IHeuristic
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="Ofs"></param>
        /// <returns></returns>
        public List<OF> SortingAlgorithm(int choice, List<OF> Ofs);
    }
}