using avulic.objects.builder;
using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.raspored;
using avulic.objects.tablica;


namespace avulic.objects.singleton
{
    public class LukaSingleton
    {
        private static readonly LukaSingleton instance = new LukaSingleton();

        public string naziv { get; set; }
        public double GPS_sirina { get; set; }
        public double GPS_visina { get; set; }
        public double dubina_luke { get; set; }
        public int ukupni_broj_putnickih_vezova { get; set; }
        public int ukupni_broj_poslovnih_vezova { get; set; }
        public int ukupni_broj_ostalih_vezova { get; set; }


        public List<Brod> brodovi = null;
        public List<Zahtjev> zahtjeviRezrevacija = null;
        public List<Vez> vezovi = null;

        public RasporedSingleton raspored = null;
        public List<Mol> molovi = null;
        public List<Kanal> kanali = null;

        public VirtualnoVrijemeSingleton virtualno_vrijeme = null;
        public AbstractLogger logger;
        public AbstractTable printer;

        private bool _podaciPostavljeni = false;



        private LukaSingleton()
        {
            virtualno_vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();
        }

        public static LukaSingleton DohvatiLukaSingleton()
        {
            return instance;
        }

        public void PostaviPodatke(string naziv, double GPS_sirina, double GPS_visina, double dubina_luke,
            int ukupni_broj_putnickih_vezova, int ukupni_broj_poslovnih_vezova, int ukupni_broj_ostalih_vezova, VirtualnoVrijemeSingleton virtualno_vrijeme)
        {
            this.naziv = naziv;
            this.GPS_sirina = GPS_sirina;
            this.GPS_visina = GPS_visina;
            this.dubina_luke = dubina_luke;
            this.ukupni_broj_putnickih_vezova = ukupni_broj_putnickih_vezova;
            this.ukupni_broj_poslovnih_vezova = ukupni_broj_poslovnih_vezova;
            this.ukupni_broj_ostalih_vezova = ukupni_broj_ostalih_vezova;
            this.virtualno_vrijeme = virtualno_vrijeme;

            _podaciPostavljeni = true;
        }

        public string[] ToStringAtributi()
        {
            string[] atributiLuka = { "naziv", "GPS_sirina", "GPS_visina", "dubina_luke", "ukupni_broj_putnickih_vezova",
                                        "ukupni_broj_poslovnih_vezova", "ukupni_broj_ostalih_vezova", "virtualno_vrijeme" };
            return atributiLuka;
        }
        public string[] ToStringData()
        {
            string[] data = { naziv, GPS_sirina.ToString(), GPS_visina.ToString(), dubina_luke.ToString(),
                            ukupni_broj_putnickih_vezova.ToString(), ukupni_broj_poslovnih_vezova.ToString(),
                            ukupni_broj_ostalih_vezova.ToString(), virtualno_vrijeme.ToString() };
            return data;
        }

        public bool PodaciPostavljeni()
        {
            return _podaciPostavljeni;
        }

        internal int DohvatiTrenutniBrojVezova(string vrstaVeza)
        {
            return vezovi.Count(vez => vez.vrsta.Equals(vrstaVeza));
        }

        public int DohvatiMaxDopustenBrojVezovaVrste(string vrstaVeza)
        {
            int maximalniKapacitet = 0;

            if (vrstaVeza.Equals(VrstaVeza.Putnicki))
            {
                maximalniKapacitet = ukupni_broj_putnickih_vezova;
            }
            else if (vrstaVeza.Equals(VrstaVeza.Poslovni))
            {
                maximalniKapacitet = ukupni_broj_poslovnih_vezova;
            }
            else if (vrstaVeza.Equals(VrstaVeza.Ostali))
            {
                maximalniKapacitet = ukupni_broj_ostalih_vezova;
            }

            return maximalniKapacitet;
        }

    }
}
