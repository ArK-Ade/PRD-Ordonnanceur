using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public class Matrix_cleaning
    {
        private List<List<object>> matrix;

        public Matrix_cleaning()
        {
        }

        public Matrix_cleaning(List<List<object>> matrix)
        {
            this.Matrix = matrix;
        }

        public List<List<object>> Matrix { get => matrix; set => matrix = value; }
    }
}