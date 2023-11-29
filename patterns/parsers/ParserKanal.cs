using avulic.objects.observer;
using avulic.parsers.factory;
using avulic.patterns.exceptions;
using avulic_zadaca_3.objects;
using avulic_zadaca_3.objects.observer;

namespace avulic.parsers
{
    //product
    public class ParserKanal : IParser
    {
        Kanal kanal = new Kanal();
        List<Kanal> kanali = new List<Kanal>();

        public ParserKanal(string fileName)
        {
            _fileName = fileName;
            atributi = kanal.ToStringAtributi();
        }

        public override List<Kanal> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                KreirajKanale(lines.Skip(1).ToArray());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Datoteka " + _fileName + " nije pronađena!");
            }
            catch (NeispravnoZaglavljekDatotekeException e)
            {
                Console.WriteLine($"{++Program.brojGreskiSustava} " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Greška u ParserKanal.Parsiraj(): " + e.Message);
            }
            return kanali;
        }

        private void KreirajKanale(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodatkeBroda(data);

                    Kanal noviKanal = (Kanal)kanal.Kloniraj();
                    noviKanal.PostaviPodatke(int.Parse(data[0]), int.Parse(data[1]), int.Parse(data[2]));


                    kanali.Add(noviKanal);
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

        private void ProvjeriPodatkeBroda(string[] data)
        {
            ProvjeriPodat(data);
        }

        public List<Kanal> DohvatiBrodove()
        {
            return kanali;
        }

    }
}
