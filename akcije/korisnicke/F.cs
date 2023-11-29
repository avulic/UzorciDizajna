using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.singleton;


namespace avulic.akcije.korisnicke
{
    public class F : IAkcija
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

        public F(string[] args)
        {
            atributi = args;
        }

        public void IzvrsiAkciju()
        {
            if (!ParsirajArgumente())
                return;

            if (!DohvatiObjekte())
                return;

            brodSpojenNaKanal = true;

            if (opcija.Equals("Q"))
            {
                if (!brodSpojenNaKanal)
                {
                    luka.logger.Message($"Brod: {brod.id} nije spojenan na kanal", LogLevel.Warning);
                    return;
                }

                if (kanal.id != kanalId)
                {
                    luka.logger.Message($"Brod: {brod.id} nije spojenan na kanal: {kanalId}", LogLevel.Warning);
                    return;
                }

                if (OdjaviBrodSKanala(brod, kanal))
                {
                    luka.logger.Message($"Brod: {brod.id} odjavljen s kanala", LogLevel.Warning);
                    return;
                }

                luka.logger.Message($"Error, brod nije odjavljen. Brod {brod.id}, Kanal {kanal.id}", LogLevel.Warning);
                return;
            }

            if (brodSpojenNaKanal)
            {
                luka.logger.Message($"Brod: {brod.id} je već spojen na kanal", LogLevel.Warning);
                return;
            }

            SpojiBrodNaKanal(brod, kanal);
        }

        private bool DohvatiObjekte()
        {
            if (kapetanija.DohvatiBrod(brodId, out brod))
            {
                luka.logger.Message($"Brod nije pronađen", LogLevel.Warning);
                return false;
            }

            if (kapetanija.DohvatiKanal(kanalId, out kanal))
            {
                luka.logger.Message($"Kanal nije pronađen", LogLevel.Warning);
                return false;
            }

            if (!kapetanija.DohvatiKanalSpojenogBroda(brod, out kanal))
            {
                luka.logger.Message($"Kanal {kanalId} ne postoji.", LogLevel.Warning);
                return false;
            }

            return true;
        }

        private bool BrodSpojen(Brod brod, Kanal kanal)
        {
            return kanal.BrodSpojen(brod);
        }

        private bool OdjaviBrodSKanala(Brod brod, Kanal kanal)
        {
            return kanal.Odjavi(brod);
        }

        private void SpojiBrodNaKanal(Brod brod, Kanal kanal)
        {
            if (kanal.maksimalanBroj < kanal.BrojSpojenihBrodova())
            {
                kanal.Spoji(brod);
                luka.logger.Message($"Brod: {brod.id} spojen na kanal", LogLevel.Info);
            }
        }

        private bool ParsirajArgumente()
        {
            if (int.TryParse(atributi[0], out kanalId))
            {
                luka.logger.Message($"Krivi format kanalId, mora bit integer", LogLevel.Warning);
                return false;
            }

            if (int.TryParse(atributi[1], out brodId))
            {
                luka.logger.Message($"Krivi format broda_id, mora bit integer", LogLevel.Warning);
                return false;
            }

            opcija = atributi[2];

            if (!string.IsNullOrEmpty(opcija) && !opcija.Equals("Q"))
            {
                luka.logger.Message("Samo opcija Q prihvatljiva", LogLevel.Warning);
                return false;
            }
            return true;
        }
    }
}
