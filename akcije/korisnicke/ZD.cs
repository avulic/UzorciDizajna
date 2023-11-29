using avulic.objects;
using avulic.objects.builder;
using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.singleton;
using avulic_zadaca_3.objects;
using avulic_zadaca_3.objects.builder;
using avulic_zadaca_3.objects.logger;
using avulic_zadaca_3.objects.observer;
using avulic_zadaca_3.objects.singleton;

namespace avulic.akcije.korisnicke
{
    public class ZD : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();
        Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();

        string[] atributi;
        int brod_Id;
        DateTime zahtjev_vrijeme;

        Vez vez;
        Brod? brod;
        Zahtjev zahtjev;
        Kanal? kanal;

        public ZD(string[] args)
        {
            atributi = args;
        }

        public void IzvrsiAkciju()
        {
            if (!ParsirajArgumente())
                return;

            zahtjev_vrijeme = vrijeme.DohvatiTrenutnoDatumVrijeme();
            //zahtjev_vrijeme_do = zahtjev_vrijeme_od.AddHours(trajanje);

            if (!kapetanija.DohvatiBrod(brod_Id, out brod))
            {
                luka.logger.Message($"Brod nije pronađen.", LogLevel.Warning);
                return;
            }

            if (!kapetanija.DohvatiKanalSpojenogBroda(brod, out kanal))
            {
                luka.logger.Message($"Brod nije spojen na kanala. Brod: {brod}, Kanal: {kanal}", LogLevel.Warning);
                return;
            }

            if (kapetanija.BrodPrivezan(brod))
            {
                luka.logger.Message($"Brod je već privezan. Brod: {brod}", LogLevel.Warning);
                return;
            }

            if (!kapetanija.BrodImaRezerviranVez(zahtjev_vrijeme.DayOfWeek, brod))
            {
                luka.logger.Message($"Brod nema rzervaciju. Brod: {brod}", LogLevel.Warning);
                return;
            }

            //TO-DO - zahtjev se kreira napocetku a svaki fail se smatra odbijenim zahtjevom
            zahtjev = kapetanija.KreirajZahtjevZaRezerviraniVez(brod, zahtjev_vrijeme);

            //odobri/odbij kreirani zahtjev
            //if (UvjetiZadovoljeni(zahtjev))
            //{
            //    //postaviti status broda u PRIVEZAN
            //    brod.privezan = true;

            //    //kapetanija.OdobriZahtjev(zahtjev);
            //    luka.logger.Message($"Zahtev odobren. Zahtjev: {zahtjev}", LogLevel.Info);

            //    return;
            //}

            //kapetanija.OdbijZahtjev(zahtjev);
            luka.logger.Message($"Zahtev odbijen. Zahtjev: {zahtjev}", LogLevel.Info);
        }

        //private bool UvjetiZadovoljeni(ZahtjevPrivezNaPrazno zahtjev)
        //{
        //    if ( !(zahtjev.datum_vrijem <= stavka.pocetak || zahtjev.datum_vrijeme >= stavka.kraj) )
        //    {
        //        luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
        //        return false;
        //    }

        //    return true;
        //}

        private bool ParsirajArgumente()
        {
            if (int.TryParse(atributi[0], out brod_Id))
            {
                luka.logger.Message($"Krivi format broda_id, mora bit integer", LogLevel.Warning);
                return false;
            }

            return true;
        }
    }
}