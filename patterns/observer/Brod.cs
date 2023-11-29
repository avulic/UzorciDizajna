using avulic.objects.composite.models;

namespace avulic.objects.observer
{
    public class Brod : Observer, IPrototype
    {
        public int id { get; set; }
        public string oznaka_broda { get; set; }
        public string naziv { get; set; }
        public string vrsta { get; set; }
        public double duljina { get; set; }
        public double sirina { get; set; }
        public double gaz { get; set; }
        public double maksimalna_brzina { get; set; }
        public int kapacitet_putnika { get; set; }
        public int kapacitet_osobnih_vozila { get; set; }
        public double kapacitet_tereta { get; set; }


        public bool privezan = false;

        public Brod()
        {

        }

        public Brod(int id, string oznaka_broda, string naziv, string vrsta, double duljina, double sirina, double gaz,
                    double maksimalna_brzina, int kapacitet_putnika, int kapacitet_osobnih_vozila, double kapacitet_tereta)
        {
            PostaviPodatke(id, oznaka_broda, naziv, vrsta, duljina, sirina, gaz,
                        maksimalna_brzina, kapacitet_putnika, kapacitet_osobnih_vozila, kapacitet_tereta);
        }

        public IPrototype Kloniraj()
        {
            return new Brod();
        }

        public void PostaviPodatke(int id, string oznaka_broda, string naziv, string vrsta, double duljina, double sirina, double gaz,
                                    double maksimalna_brzina, int kapacitet_putnika, int kapacitet_osobnih_vozila, double kapacitet_tereta)
        {
            this.id = id;
            this.oznaka_broda = oznaka_broda;
            this.naziv = naziv;
            this.vrsta = vrsta;
            this.duljina = duljina;
            this.sirina = sirina;
            this.gaz = gaz;
            this.maksimalna_brzina = maksimalna_brzina;
            this.kapacitet_putnika = kapacitet_putnika;
            this.kapacitet_osobnih_vozila = kapacitet_osobnih_vozila;
            this.kapacitet_tereta = kapacitet_tereta;
        }

        public string[] ToStringAtributi()
        {
            string[] atributiBrod = { "id", "oznaka_broda", "naziv", "vrsta", "duljina", "sirina", "gaz",
                                    "maksimalna_brzina", "kapacitet_putnika", "kapacitet_osobnih_vozila", "kapacitet_tereta" };
            return atributiBrod;
        }
        public string[] ToStringData()
        {
            string[] data = { id.ToString(), oznaka_broda, naziv, vrsta, duljina.ToString(), sirina.ToString(), gaz.ToString(),
                            maksimalna_brzina.ToString(), kapacitet_putnika.ToString(), kapacitet_osobnih_vozila.ToString(), kapacitet_tereta.ToString() };
            return data;
        }



        string message;
        private Kanal channel;

        public override void Update(string message)
        {
            this.message = message;
            Console.WriteLine("Ship {0} received message: {1}", id, message);
        }

        public override void PosaljiPoruku(string message)
        {
            channel.Poruka = message;
        }

        public override int GetId()
        {
            return id;
        }
    }
}
