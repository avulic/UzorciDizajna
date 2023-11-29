using avulic.objects.logger;
using avulic.objects.memento;
using avulic.objects.singleton;


namespace avulic.akcije.korisnicke
{
    public class VPS : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();

        string[] atributi;
        string opcija;

        public VPS(string[] args)
        {
            atributi = args;
        }

        public void IzvrsiAkciju()
        {
            if (!ParsirajArgumente())
                return;


            // Restore saved state
            Caretaker c = new Caretaker();

            Memento memento = c.Get(opcija);
            if (memento == null)
            {
                luka.logger.Message("Greška, stanje se ne može vrat", LogLevel.Warning);

                return;
            }


            kapetanija.VratiStanjeVezova(memento);
            luka.logger.Message("Stanje vezova spremljeno, oznaka:" + opcija, LogLevel.Info);
        }


        private bool ParsirajArgumente()
        {
            opcija = atributi[1];

            if (!string.IsNullOrEmpty(opcija))
            {
                luka.logger.Message("Neispravna naredba", LogLevel.Warning);
                return false;
            }
            return true;
        }
    }
}
