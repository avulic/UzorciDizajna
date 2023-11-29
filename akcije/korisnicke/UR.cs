using avulic.objects;
using avulic.objects.builder;
using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.raspored;
using avulic.objects.singleton;
using avulic.objects.tablica;
using avulic.parsers;
using avulic.parsers.factory;

namespace avulic.akcije.korisnicke
{
    public class UR : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();
        Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();

        TablePrinter printer = TablePrinter.DohvatiTablePrinter();
        AbstractTable tablica = new TableBasic();


        Vez vez;
        Brod? brod;
        Zahtjev zahtjev;
        Kanal? kanal;

        string[] _attribute;

        public UR(string[] args)
        {
            _attribute = args;
        }

        public void IzvrsiAkciju()
        {
            if (!_attribute[0].Equals("UR"))
                return;

            IParser parser = KreatorParsera.DohvatiParser("UR", _attribute[1]);
            List<Zahtjev> zahtjevi = (List<Zahtjev>)parser.Parsiraj();

            foreach (Zahtjev zahtjev in zahtjevi)
            {
                if (ZahtjevOdobren(zahtjev))
                {
                    //dodati odobrene zahtjeve u raspored
                    //DodajZauzetostVezaURaspored(zahtjev);
                    //postaviti status broda u PRIVEZAN
                    brod.privezan = true;

                    //kapetanija.OdobriZahtjev(zahtjev);
                    luka.logger.Message($"Zahtev odobren. Zahtjev: {zahtjev}", LogLevel.Info);

                    return;
                }
                //kapetanija.OdbijZahtjev(zahtjev);
                luka.logger.Message($"Zahtev odbijen. Zahtjev: {zahtjev}", LogLevel.Info);
            }
        }

        private bool ZahtjevOdobren(Zahtjev zahtjev)
        {
            Vez? vez;

            //zahtjev mora biti minimalno 1 h
            if (zahtjev.trajanje_priveza_u_h >= 1)
            {
                luka.logger.Message($"Minimalno trajanje zahtjeva je 1h.", LogLevel.Info);
                return false;
            }


            //TO-DO pronači najekonomičniji vez
            if (!kapetanija.DohvatiSlobodanVez(brod, out vez, zahtjev.datum_vrijeme_od, zahtjev.datum_vrijeme_do))
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

            raspored.DodajStavku(zahtjev.datum_vrijeme_do.DayOfWeek, stavka);
        }

    }
}
