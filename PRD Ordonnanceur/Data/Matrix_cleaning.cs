using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    public class Matrix_cleaning
    {

        // TODO Creer la matrice de nettoyage des OFs et le prendre en compte dans l'algorithme

        private List<List<object>> matrix;

        public Matrix_cleaning(List<List<object>> matrix)
        {
            this.Matrix = matrix;
        }

        public List<List<object>> Matrix { get => matrix; set => matrix = value; }
    }
}
