namespace PRD_Ordonnanceur.Data
{
    /// <summary>
    ///
    /// </summary>
    public class Tank
    {
        private int idTank;
        private int typeTank;

        /// <summary>
        ///
        /// </summary>
        public Tank()
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idTank"></param>
        /// <param name="typeTank"></param>
        public Tank(int idTank, int typeTank)
        {
            this.idTank = idTank;
            this.typeTank = typeTank;
        }

        /// <summary>
        ///
        /// </summary>
        public int IdTank { get => idTank; set => idTank = value; }

        /// <summary>
        ///
        /// </summary>
        public int TypeTank { get => typeTank; set => typeTank = value; }
    }
}