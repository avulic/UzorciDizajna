using avulic.objects.logger;
using avulic.objects.memento;
using avulic.objects.observer;
using avulic.objects.singleton;


namespace avulic.akcije.korisnicke
{
    public class SPS : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();

        string[] atributi;
        int brodId;
        int kanalId;
        string opcija;
        bool brodSpojenNaKanal = false;

        Brod? brod;
        Kanal kanal;

        public SPS(string[] args)
        {
            atributi = args;
        }

        public void IzvrsiAkciju()
        {
            if (!ParsirajArgumente())
                return;


            // Store internal state
            Caretaker c = new Caretaker();
            c.Add(kapetanija.SpremiStanjeVezova(opcija));
            luka.logger.Message("Stanje vezova spremljeno, oznaka:" + opcija, LogLevel.Info);


        }

        private bool ParsirajArgumente()
        {
            opcija = atributi[1];

            if (string.IsNullOrEmpty(opcija))
            {
                luka.logger.Message("Neispravna naredba", LogLevel.Warning);
                return false;
            }

            return true;
        }
    }
}
