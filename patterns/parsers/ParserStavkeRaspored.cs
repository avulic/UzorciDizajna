using avulic.objects;
using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.raspored;
using avulic.objects.singleton;
using avulic.parsers.factory;
using avulic.patterns.exceptions;
using avulic_zadaca_3.objects;
using avulic_zadaca_3.objects.observer;
using avulic_zadaca_3.objects.raspored;
using avulic_zadaca_3.objects.singleton;

namespace avulic.parsers
{
    internal class ParserStavkeRasporeda : IParser
    {
        StavkaRasporeda stavka = new StavkaRasporeda();
        List<StavkaRasporeda> stavkeRasporeda = new List<StavkaRasporeda>();

        RasporedSingleton raspored;

        public ParserStavkeRasporeda(string fileName)
        {
            _fileName = fileName;
            atributi = stavka.ToStringAtributi();
            raspored = RasporedSingleton.DohvatiRaspored();
        }
        public override List<StavkaRasporeda> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                KreirajStavku(lines.Skip(1).ToArray());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Datoteka " + _fileName + " nije pronađena!");
            }
            catch (NeispravnoZaglavljekDatotekeException e)
            {
                Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno zaglalje! " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Greška u ParserStavkeRaspored.Parsiraj(): " + e.Message);
            }

            return stavkeRasporeda;
        }

        private void KreirajStavku(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodat(data);

                    List<DayOfWeek> dani = DohvatiDane(data[2]);
                    StavkaRasporeda novaStavka = null;

                    foreach (DayOfWeek dan in dani)
                    {
                        novaStavka = (StavkaRasporeda)stavka.Kloniraj();

                        TimeOnly vrijeme_od = ParsirajVrijeme(data[3]);
                        TimeOnly vrijeme_do = ParsirajVrijeme(data[4]);

                        novaStavka.PostaviPodatke(
                                        int.Parse(data[0]),
                                        int.Parse(data[1]),
                                        dan,
                                        vrijeme_od,
                                        vrijeme_do);

                        if (Provjere(novaStavka))
                            stavkeRasporeda.Add(novaStavka);
                    }
                }
                catch (NeispravanRedakDatotekeException e)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravni podaci: " + e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno format: " + line);
                }
            }
        }

        private bool Provjere(StavkaRasporeda novaStavka)
        {
            LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
            Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();

            if (kapetanija.DohvatiBrod(novaStavka.id_brod, out Brod brod))
            {
                luka.logger.Message("Brod ne postoji", LogLevel.Warning);
                return false;
            }


            if (kapetanija.DohvatiVez(novaStavka.id_vez, out Vez vez))
            {
                luka.logger.Message("Vez ne postoji", LogLevel.Warning);
                return false;
            }

            if (kapetanija.BrodOdgovaraVezu(vez, brod))
            {
                luka.logger.Message("Brod ne odgovara vezu", LogLevel.Warning);
                return false;
            }

            if (stavkeRasporeda.Exists(s => s.dan_u_tjednu == novaStavka.dan_u_tjednu && s.id_brod == novaStavka.id_brod && s.id_vez == novaStavka.id_vez))
                if (IntervaliSePreklapaju(novaStavka))
                {
                    luka.logger.Message("Zapis u rasporedu dupliciran", LogLevel.Warning);
                    return false;
                }

            return true;
        }

        private bool IntervaliSePreklapaju(StavkaRasporeda novaStavka)
        {
            foreach (StavkaRasporeda stavka in stavkeRasporeda)
            {
                if (Math.Max(stavka.vrijeme_od.Hour, novaStavka.vrijeme_od.Hour) < Math.Min(stavka.vrijeme_do.Hour, novaStavka.vrijeme_do.Hour))
                    return false;
            }

            return true;
        }

        private List<DayOfWeek> DohvatiDane(string data)
        {
            string[] dani = data.Split(",");
            List<DayOfWeek> daniLlist = new List<DayOfWeek>();

            foreach (var dan in dani)
            {
                daniLlist.Add(raspored.DohvatiDan(int.Parse(dan)));
            }

            return daniLlist;
        }
    }
}
