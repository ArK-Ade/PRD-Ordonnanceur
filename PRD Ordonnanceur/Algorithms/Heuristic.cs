using PRD_Ordonnanceur.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRD_Ordonnanceur.Algorithms
{
    public class Heuristic : IHeuristic
    {
        public Heuristic()
        {

        }

        public bool application()
        {
            return true;
        }

        int Smallest_index_DTI(OF[] OFs)
        {

            int Index = -1;
            DateTime date = new DateTime(5000,01,01);
            int count = 0;

            foreach (OF oF in OFs)
            {
                if (date > oF.earliest_date)
                {
                    date = oF.earliest_date;
                    Index = count;
                }
                count++;
            }

            return Index;
        }

        OF[] SortCrescentDtiCrescentDli(OF[] OFs)
        {

            OF[] tableau = OFs;
            Array.Clear(tableau,0,tableau.Length);

            OF earliest_OF = new();

            int index;

            for (int i = 0; i < OFs.Length; i++)
            {
                index = Smallest_index_DTI(OFs);
                tableau[i] = OFs[index];

                // TODO supprimer l'élément du tableau 
                OFs[index].earliest_date = DateTime.MaxValue;
            }

            return tableau;
        }

        void Sort_crescent_dti_decendant_dli(OF[] OFs)
        {
            foreach (OF i in OFs)
            {

            }
        }

        void Sort_random_5_crescent(OF[] OFs)
        {
            foreach (OF i in OFs)
            {

            }
        }

    }
}
