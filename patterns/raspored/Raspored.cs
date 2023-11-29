using avulic.objects.Iterator;
using avulic.objects.observer;


namespace avulic.objects.raspored
{
    public sealed class RasporedSingleton
    {
        private static readonly RasporedSingleton instance = new RasporedSingleton();

        private DanCollection sadrzajRasporeda;
        //private StavkaRasporeda stavka;

        private RasporedSingleton()
        {
            sadrzajRasporeda = new DanCollection();

        }

        public static RasporedSingleton DohvatiRaspored()
        {
            return instance;
        }


        public string[] ToStringAtributi()
        {
            string[] atributiZahtjev = { "id_vez", "id_brod", "dani_u_tjednu", "datum_vrijeme_od", "trajanje_priveza_u_h" };
            return atributiZahtjev;
        }

        public List<string> ToStringData()
        {
            List<string> data = new List<string>();

            //foreach (var item in raspored)
            //{
            //    data{ this.id_vez.ToString(), this.id_brod.ToString(), this.dani_u_tjednu.ToString(), this.vrijeme_od.ToString(), this.vrijeme_do.ToString() };

            //}

            return data;
        }

        public DanCollection DohvatiDane()
        {
            return sadrzajRasporeda;
        }

        public DayOfWeek DohvatiDan(int dan)
        {
            var dana = dan switch
            {
                0 => DayOfWeek.Sunday,
                1 => DayOfWeek.Monday,
                2 => DayOfWeek.Tuesday,
                3 => DayOfWeek.Wednesday,
                4 => DayOfWeek.Thursday,
                5 => DayOfWeek.Friday,
                6 => DayOfWeek.Saturday,
                _ => throw new NeispravanRedakDatotekeException("Pokušaj dohvaćanja nepostoječeg dana."),
            };
            return dana;
        }

        public void DodajStavke(List<StavkaRasporeda> stavke)
        {
            foreach (StavkaRasporeda stavka in stavke)
            {
                sadrzajRasporeda.AddStavkaZaDan(stavka.dan_u_tjednu, stavka);
            }
        }
        public void DodajStavke(DayOfWeek dani_u_tjednu, StavkaCollection stavke)
        {
            sadrzajRasporeda.AddStavkeZaDan(dani_u_tjednu, stavke);
        }

        public void DodajStavku(DayOfWeek dan, StavkaRasporeda stavka)
        {

            sadrzajRasporeda.AddStavkaZaDan(dan, stavka);

        }

        public StavkaCollection DohvatiStavkeDana(DayOfWeek dan)
        {
            StavkaCollection result;
            instance.sadrzajRasporeda.GetStavkeZaDan(dan, out result);

            return result;
        }

        internal bool BrodImaRezervaciju(DayOfWeek dan, Brod brod)
        {
            IIterator stavkeIterator = DohvatiStavkeDana(dan).DohvatiIterator();
            for (StavkaRasporeda stavka = (StavkaRasporeda)stavkeIterator.First(); !stavkeIterator.IsCompleted; stavka = (StavkaRasporeda)stavkeIterator.Next())
            {
                if (stavka.id_brod.Equals(brod.id))
                    return true;
            }

            return false;
        }
    }
}
