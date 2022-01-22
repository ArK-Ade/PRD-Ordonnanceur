namespace PRD_Ordonnanceur.Data
{
    class Tank
    {
        public int Id { get; set; }

        public int TypeTank { get; set; }

        public Tank()
        {

        }

        public Tank(int id, int typeTank)
        {
            Id = id;
            TypeTank = typeTank;
        }
    }
}
