namespace avulic.objects.raspored
{
    public class Dan
    {
        private DayOfWeek DayOfWeek { get; set; }
        private StavkaCollection stavke { get; set; }


        public Dan()
        {

        }

        public void AddStavka(StavkaRasporeda stavka)
        {
            stavke.AddStavka(stavka);
        }
        public void AddStavke(StavkaCollection stavka)
        {
            stavke = stavka;
        }

        public StavkaCollection DohvatiStavke()
        {
            return stavke;
        }

        public DayOfWeek DohvatiDan()
        {
            return DayOfWeek;
        }
    }
}
