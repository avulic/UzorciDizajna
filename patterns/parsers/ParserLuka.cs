using avulic.objects.singleton;
using avulic.parsers.factory;
using avulic.patterns.exceptions;

namespace avulic.parsers
{
    internal class ParserLuka : IParser
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();

        public ParserLuka(string fileName)
        {
            _fileName = fileName;

            atributi = luka.ToStringAtributi();
        }
        public override LukaSingleton Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                KreirajLuku(lines.Skip(1).ToArray());
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

            return luka;
        }

        private void KreirajLuku(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodatkeLuke(data);

                    string datum = data[7].Trim();
                    DateTime vrijeme = ParsirajDatumVrijeme(datum);
                    VirtualnoVrijemeSingleton virtualnoVrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();
                    virtualnoVrijeme.PostaviNovoVrijeme(vrijeme);

                    luka.PostaviPodatke(data[0], ParsDecimal(data[1]), ParsDecimal(data[2]), ParsDecimal(data[3]),
                                            int.Parse(data[4]), int.Parse(data[5]), int.Parse(data[6]), virtualnoVrijeme);
                }
                catch (NeispravanRedakDatotekeException e)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravni podaci: " + e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno format datuma " + line);
                }
            }
        }

        private void ProvjeriPodatkeLuke(string[] data)
        {
            ProvjeriPodat(data);
        }
    }
}
