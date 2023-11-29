using avulic.objects.builder;
using avulic.objects.Iterator;
using avulic.objects.logger;
using avulic.objects.memento;
using avulic.objects.observer;
using avulic.objects.raspored;


namespace avulic.objects.singleton
{
    public class Kapetanija : Observer
    {
        private static readonly Kapetanija instance = new Kapetanija();
        public List<Zahtjev> dnevnikRadaKapetanije = new List<Zahtjev>();

        RasporedSingleton raspored = RasporedSingleton.DohvatiRaspored();
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        private Kapetanija kapetanija = instance;

        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();

        private Kapetanija()
        {
            SpojiLukuNaKanale();
        }


        public static Kapetanija DohvatiKapetanijaSingleton()
        {
            return instance;
        }


        public bool DohvatiBrod(int brod_Id, out Brod? brod)
        {
            LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
            brod = luka.brodovi.FirstOrDefault(b => b.id == brod_Id);

            if (brod == null)
                return false;

            return true;
        }

        public bool DohvatiKanalSpojenogBroda(Brod brod, out Kanal? kanal)
        {
            foreach (Kanal k in luka.kanali)
            {
                if (k.BrodSpojen(brod))
                {
                    kanal = k;
                    return true;
                }
            }

            kanal = null;
            return false;
        }

        public bool DohvatiSlobodanVez(Brod brod, out Vez? vez, DateTime datum_od, DateTime datum_do)
        {
            LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
            List<Vez> vezovi = luka.vezovi;

            foreach (Vez v in vezovi)
            {
                if (VezSlobodanNaIntervalu(v, datum_od, datum_do))
                {
                    vez = v;
                    return true;
                }
            }

            vez = null;
            return false;
        }

        public bool BrodOdgovaraVezu(Vez v, Brod brod)
        {
            if (brod.vrsta == v.vrsta &&
                brod.duljina <= v.maksimalna_duljina &&
                brod.sirina <= v.maksimalna_sirina &&
                brod.gaz <= v.maksimalna_dubina)
                return true;

            return false;
        }

        //TO-DO provjera u odobreim zahtjevima
        public bool VezSlobodanNaIntervalu(Vez vez_kandidat, DateTime zahtjev_datum_od, DateTime zahtjev_datum_do)
        {
            RasporedSingleton raspored = RasporedSingleton.DohvatiRaspored();
            bool slobodanVez = false;

            IIterator daniIterator = raspored.DohvatiDane().DohvatiIterator();
            for (Dan dan = (Dan)daniIterator.First(); !daniIterator.IsCompleted; dan = (Dan)daniIterator.Next())
            {
                if (dan.DohvatiDan() > zahtjev_datum_do.DayOfWeek)
                    break;

                if (dan.DohvatiDan() < zahtjev_datum_od.DayOfWeek)
                    continue;

                IIterator stavkeIterator = dan.DohvatiStavke().DohvatiIterator();
                for (StavkaRasporeda stavka = (StavkaRasporeda)stavkeIterator.First(); !stavkeIterator.IsCompleted; stavka = (StavkaRasporeda)stavkeIterator.Next())
                {
                    //TO-DO provjeri logiku
                    if (stavka.id_vez == vez_kandidat.id)
                    {
                        if (!IntervaliSePreklapaju(zahtjev_datum_od, stavka.vrijeme_od, zahtjev_datum_do, stavka.vrijeme_od))
                            slobodanVez = true;

                    }
                }
            }

            return slobodanVez;
        }

        public bool DohvatiVez(int id_vez, out Vez vez)
        {
            LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
            vez = luka.vezovi.FirstOrDefault(v => v.id == id_vez);

            if (vez == null)
                return false;

            return true;
        }

        public bool IstiDanTjedna(DateTime zahtjev_datum_od, Dan dan_u_tjednu)
        {
            if (zahtjev_datum_od.DayOfWeek.CompareTo(dan_u_tjednu) != 0)
                return false;

            return true;
        }

        public bool IntervaliSePreklapaju(DateTime zahtjev_datum_od, TimeOnly vrijeme_od, DateTime zahtjev_datum_do, TimeOnly vrijeme_do)
        {
            if (Math.Max(zahtjev_datum_od.Hour, vrijeme_od.Hour) < Math.Min(zahtjev_datum_do.Hour, vrijeme_do.Hour))
                return true;

            return false;
        }

        public Dictionary<Vez, List<StavkaRasporeda>> DohvatiSveVezoveZaInterval(string vrstaVeza, StatusVeza statusVeza, DateTime datum_od, DateTime datum_do)
        {
            Dictionary<Vez, List<StavkaRasporeda>> vezovi = new Dictionary<Vez, List<StavkaRasporeda>>();
            List<StavkaRasporeda> stavkeVeza = new List<StavkaRasporeda>();
            Vez vez = null;

            IIterator daniIterator = raspored.DohvatiDane().DohvatiIterator();
            for (Dan dan = (Dan)daniIterator.First(); !daniIterator.IsCompleted; dan = (Dan)daniIterator.Next())
            {
                if (dan.DohvatiDan() > datum_od.DayOfWeek)
                    break;

                if (dan.DohvatiDan() < datum_do.DayOfWeek)
                    continue;

                IIterator stavkeIterator = dan.DohvatiStavke().DohvatiIterator();
                for (StavkaRasporeda stavka = (StavkaRasporeda)stavkeIterator.First(); !stavkeIterator.IsCompleted; stavka = (StavkaRasporeda)stavkeIterator.Next())
                {
                    TimeOnly hours_od = TimeOnly.FromDateTime(datum_od);
                    TimeOnly hours_do = TimeOnly.FromDateTime(datum_do);

                    if (!kapetanija.DohvatiVez(stavka.id_vez, out vez))
                    {
                        luka.logger.Message("Krivi vez_id", LogLevel.Warning);
                        break;
                    }

                    if (!vez.vrsta.Equals(vrstaVeza))
                        break;

                    if (statusVeza.Equals(StatusVeza.Slobodan) && !IntervaliSePreklapaju(datum_od, stavka.vrijeme_od, datum_do, stavka.vrijeme_do))
                        stavkeVeza.Add(stavka);

                    //To-DO - možda nije dobra logika
                    if (statusVeza.Equals(StatusVeza.Zauzet) && IntervaliSePreklapaju(datum_od, stavka.vrijeme_od, datum_do, stavka.vrijeme_do))
                        stavkeVeza.Add(stavka);
                }

                vezovi.Add(vez, stavkeVeza);
            }

            return vezovi;
        }

        public bool BrodPrivezan(Brod brod)
        {
            if (brod.privezan)
                return true;

            return false;
        }

        internal bool BrodImaRezerviranVez(DayOfWeek dan, Brod brod)
        {
            RasporedSingleton raspored = RasporedSingleton.DohvatiRaspored();

            raspored.BrodImaRezervaciju(dan, brod);

            return true;
        }

        internal bool DohvatiKanal(int kanalId, out Kanal? kanal)
        {
            kanal = luka.kanali.FirstOrDefault(k => k.id.Equals(kanalId));

            if (kanal != null)
            {
                return true;
            }

            return false;
        }

        internal bool BrodSpojenNaKanal(Brod brod)
        {
            bool spojen = luka.kanali.Any(k => k.BrodSpojen(brod));

            return spojen;
        }


        private void SpojiLukuNaKanale()
        {
            foreach (Kanal kanal in luka.kanali)
            {
                if (!kanal.Spoji(this))
                    luka.logger.Message($"Prekoračen broj na kanalu: {kanal.id}", LogLevel.Warning);
            }
        }

        string message;
        private Kanal channel;

        public override void Update(string message)
        {
            this.message = message;
            Console.WriteLine("Kapetanija received message: {1}", message);
        }

        public override void PosaljiPoruku(string message)
        {
            channel.Poruka = message;
        }

        internal Zahtjev KreirajZahtjevZaRezerviraniVez(Brod brod, DateTime zahtjev_vrijeme)
        {
            throw new NotImplementedException();
        }

        public void PohraniZahtjev(Zahtjev zahtjev)
        {
            dnevnikRadaKapetanije.Add(zahtjev);
        }

        internal bool RezervacijaZaDatumVrijemePostoji(Zahtjev zahtjevNovi)
        {
            foreach (Zahtjev zahtjev in dnevnikRadaKapetanije)
            {
                if (zahtjev.Status.Equals(StatusZahtjeva.Odobren))
                {
                    TimeOnly od_time = TimeOnly.FromDateTime(zahtjevNovi.datum_vrijeme_od);
                    TimeOnly do_time = TimeOnly.FromDateTime(zahtjevNovi.datum_vrijeme_do);

                    if (IntervaliSePreklapaju(zahtjev.datum_vrijeme_od, od_time, zahtjev.datum_vrijeme_do, do_time))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override int GetId()
        {
            throw new NotImplementedException();
        }



        //Originator
        public Memento SpremiStanjeVezova(string oznaka)
        {
            return new Memento(luka.vezovi, oznaka, vrijeme.DohvatiTrenutnoDatumVrijeme());
        }

        public void VratiStanjeVezova(Memento memento)
        {
            luka.vezovi = memento.vezovi;
            vrijeme.PostaviNovoVrijeme(memento.trenutnoVrijeme);
        }
    }
}
