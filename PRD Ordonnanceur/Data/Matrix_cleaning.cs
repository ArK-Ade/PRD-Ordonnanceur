using System.Collections.Generic;

namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    ///
    /// </summary>
    public class MatrixCleaning
    {
        private List<List<object>> matrix;

        /// <summary>
        ///
        /// </summary>
        public MatrixCleaning()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="matrix"></param>
        public MatrixCleaning(List<List<object>> matrix)
        {
            this.Matrix = matrix;
        }

        /// <summary>
        ///
        /// </summary>
        public List<List<object>> Matrix { get => matrix; set => matrix = value; }
    }
}