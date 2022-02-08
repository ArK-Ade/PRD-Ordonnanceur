namespace PRD_Ordonnanceur.Data
{
    public class Tank
    {
        private int idTank;
        private int typeTank;

        public Tank()
        {
        }

        public Tank(int idTank, int typeTank)
        {
            this.idTank = idTank;
            this.typeTank = typeTank;
        }

        public int IdTank { get => idTank; set => idTank = value; }
        public int TypeTank { get => typeTank; set => typeTank = value; }
    }
}
