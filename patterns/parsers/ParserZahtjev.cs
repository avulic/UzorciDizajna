using avulic.objects;
using avulic.objects.builder;
using avulic.parsers.factory;
using avulic.patterns.exceptions;


namespace avulic.parsers
{
    internal class ParserZahtjevRezrevacije : IParser
    {
        List<Zahtjev> zahtjevi = new List<Zahtjev>();

        public ParserZahtjevRezrevacije(string fileName)
        {
            _fileName = fileName;
            // atributi = zahtjev.ToStringAtributi();
        }
        public override List<Zahtjev> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                KreirajZahtjev(lines.Skip(1).ToArray());
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
                Console.WriteLine("Greška u ParserLuka.Parsiraj(): " + e.Message);
            }

            return zahtjevi;
        }

        private void KreirajZahtjev(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodatkeZahtjeva(data);

                    DateTime datum_vrijeme_od = ParsirajDatumVrijeme(data[1]);

                    Zahtjev noviZahtjev = KreirajNoviZahtjev(int.Parse(data[0]), datum_vrijeme_od, int.Parse(data[2]));


                    zahtjevi.Add(noviZahtjev);
                }
                catch (NeispravanRedakDatotekeException e)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Nepotpuni podaci: " + e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno format vremena! " + line);
                }
            }
        }

        private Zahtjev KreirajNoviZahtjev(int id, DateTime datum_vrijeme_od, int trajanje_priveza_u_h)
        {
            ZahtjevDirector direktor = new ZahtjevDirector(new ZahtjevRezervacijaBuilder());
            return direktor.KreriajZahtjev(id, datum_vrijeme_od, trajanje_priveza_u_h);
        }

        private void ProvjeriPodatkeZahtjeva(string[] data)
        {
            ProvjeriPodat(data);
        }

        public List<Zahtjev> DohvatiZahtjeve()
        {
            return zahtjevi;
        }
    }
}
