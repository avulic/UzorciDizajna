using avulic.objects.observer;
using avulic.parsers.factory;
using avulic.patterns.exceptions;

namespace avulic.parsers
{
    //product
    public class ParserBrod : IParser
    {
        Brod brod = new Brod();
        List<Brod> brodovi = new List<Brod>();

        public ParserBrod(string fileName)
        {
            _fileName = fileName;
            atributi = brod.ToStringAtributi();
        }
        public override List<Brod> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                KreirajBrodove(lines.Skip(1).ToArray());
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
            return brodovi;
        }

        private void KreirajBrodove(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodat(data);

                    Brod noviBrod = (Brod)brod.Kloniraj();
                    noviBrod.PostaviPodatke(int.Parse(data[0]), data[1], data[2], data[3],
                                            ParsDecimal(data[4]), ParsDecimal(data[5]), ParsDecimal(data[6]), ParsDecimal(data[7]),
                                            int.Parse(data[8]), int.Parse(data[9]), int.Parse(data[10]));

                    if (ProvjeriPodatkeBroda(noviBrod)) ;
                    brodovi.Add(noviBrod);
                }
                catch (NeispravanRedakDatotekeException e)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Nepotpuni podaci: " + e.Message);
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno format: " + e.Message);
                }
            }
        }

        private bool ProvjeriPodatkeBroda(Brod data)
        {
            if (data.kapacitet_tereta.Equals(0) && data.kapacitet_putnika.Equals(0) && data.kapacitet_osobnih_vozila.Equals(0))
            {
                Console.WriteLine($"{++Program.brojGreskiSustava} Kapacitet ne može biti 0: tereta " +
                                $"{data.kapacitet_tereta}, putnci {data.kapacitet_putnika}, vozila {data.kapacitet_osobnih_vozila}");
                return false;
            }

            return true;
        }

        public List<Brod> DohvatiBrodove()
        {
            return brodovi;
        }
    }
}
