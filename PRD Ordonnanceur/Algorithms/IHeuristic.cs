using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Algorithms
{
    interface IHeuristic
    {
        public OF[] SortingAlgorithm(int choice, OF[] Ofs);

    }
}
