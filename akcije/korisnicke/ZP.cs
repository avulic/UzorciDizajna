using avulic.objects;
using avulic.objects.builder;
using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.raspored;
using avulic.objects.singleton;
using avulic_zadaca_3.objects;
using avulic_zadaca_3.objects.builder;
using avulic_zadaca_3.objects.logger;
using avulic_zadaca_3.objects.observer;
using avulic_zadaca_3.objects.raspored;
using avulic_zadaca_3.objects.singleton;


namespace avulic.akcije.korisnicke
{
    public class ZP : IAkcija
    {
        int brod_Id;
        int trajanje;
        string[] atributi;

        Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();

        Vez vez;
        Brod? brod;
        Zahtjev zahtjev;
        Kanal? kanal;

        DateTime zahtjev_vrijeme_od;
        DateTime zahtjev_vrijeme_do;

        public ZP(string[] args)
        {
            atributi = args;
        }

        public void IzvrsiAkciju()
        {
            if (!ParsirajArgumente())
                return;

            zahtjev_vrijeme_od = vrijeme.DohvatiTrenutnoDatumVrijeme();
            zahtjev_vrijeme_do = zahtjev_vrijeme_od.AddHours(trajanje);

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

            //if (kapetanija.BrodImaRezerviranVez(zahtjev_vrijeme_od.DayOfWeek, brod))
            //{
            //    luka.logger.Message($"Brod je već privezan. Brod: {brod}", LogLevel.Warning);
            //    return;
            //}

            //TO-DO - zahtjev se kreira napocetku a svaki fail se smatra odbijenim zahtjevom
            //zahtjev = kapetanija.KreirajZahtjevZaSlobodanVez(brod, vez, zahtjev_vrijeme_od, zahtjev_vrijeme_do);

            ////odobri/odbij kreirani zahtjev
            //if (ZahtjevOdobren(zahtjev))
            //{   
            //    DodajZauzetostVezaURaspored(zahtjev);
            //    //postaviti status broda u PRIVEZAN
            //    brod.privezan = true;

            //    kapetanija.OdobriZahtjev(zahtjev);

            //    return;
            //}
            //kapetanija.OdbijZahtjev(zahtjev);
            //luka.logger.Message($"Zahtev odbijen. Zahtjev: {zahtjev}", LogLevel.Info);
        }

        private bool ZahtjevOdobren(Zahtjev zahtjev)
        {
            Vez? vez;

            //TO-DO pronači najekonomičniji vez
            if (!kapetanija.DohvatiSlobodanVez(brod, out vez, zahtjev_vrijeme_od, zahtjev_vrijeme_do))
            {
                luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
                return false;
            }

            //TO-DO pronači najekonomičniji vez
            if (kapetanija.BrodOdgovaraVezu(vez, brod))
            {
                luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
                return false;
            }

            //zahtjev mora biti minimalno 1 h
            if (zahtjev.trajanje_priveza_u_h >= 1)
            {
                luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
                return false;
            }

            return true;
        }

        private void DodajZauzetostVezaURaspored(Zahtjev zahtjev)
        {
            RasporedSingleton raspored = RasporedSingleton.DohvatiRaspored();
            StavkaRasporeda stavka = new StavkaRasporeda();

            TimeOnly vrijeme_od = TimeOnly.FromDateTime(zahtjev.datum_vrijeme_od);
            TimeOnly vrijeme_do = TimeOnly.FromDateTime(zahtjev.datum_vrijeme_do);

            //stavka.PostaviPodatke(zahtjev.id_vez,
            //                      zahtjev.id_brod,
            //                      zahtjev.datum_vrijeme_od.DayOfWeek,
            //                      vrijeme_od,
            //                      vrijeme_do);

            //raspored.DodajStavku(zahtjev.datum_vrijeme_do.DayOfWeek, stavka);
        }

        private bool ParsirajArgumente()
        {

            if (int.TryParse(atributi[0], out brod_Id))
            {
                luka.logger.Message($"Krivi format broda_id, mora bit integer", LogLevel.Warning);
                return false;
            }


            if (int.TryParse(atributi[1], out trajanje))
            {
                luka.logger.Message($"Trajanje mora biti u satima.", LogLevel.Warning);
                return false;
            }

            return true;
        }

    }
}
