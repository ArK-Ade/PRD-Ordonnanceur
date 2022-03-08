using PRD_Ordonnanceur.Data;

namespace PRD_Ordonnanceur.Algorithms
{
    internal interface IHeuristic
    {
        public OF[] SortingAlgorithm(int choice, OF[] Ofs);
    }
}