using PRD_Ordonnanceur.Data;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    /// <summary>
    /// Interface that is the template for the sortingAlgorithme
    /// </summary>
    internal interface IHeuristic
    {
        /// <summary>
        /// Template method
        /// </summary>
        /// <param name="choice"></param>
        /// <param name="Ofs"></param>
        /// <returns></returns>
        public List<OF> SortingAlgorithm(int choice, List<OF> Ofs);
    }
}