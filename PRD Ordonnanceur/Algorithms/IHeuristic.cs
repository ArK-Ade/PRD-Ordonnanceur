using PRD_Ordonnanceur.Data;
using System.Collections.Generic;

namespace PRD_Ordonnanceur.Algorithms
{
    internal interface IHeuristic
    {
        public List<OF> SortingAlgorithm(int choice, List<OF> Ofs);
    }
}