using avulic.objects;
using avulic.parsers.factory;
using avulic.patterns.exceptions;
using avulic_zadaca_3.objects;

namespace avulic.parsers
{
    internal class ParserVez : IParser
    {
        Vez vez = new Vez();
        List<Vez> vezovi = new List<Vez>();

        public ParserVez(string fileName)
        {
            _fileName = fileName;
            atributi = vez.ToStringAtributi();
        }
        public override List<Vez> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                // ProvjeriZaglavlje(lines);

                KreirajVez(lines.Skip(1).ToArray());
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

            return vezovi;
        }

        private void KreirajVez(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodatkeVeza(data);

                    Vez noviVez = (Vez)vez.Kloniraj();

                    noviVez.PostaviPodatke(int.Parse(data[0]), data[1], ParsirajVrstaVeza(data[2]),
                                            int.Parse(data[3]), int.Parse(data[4]),
                                            int.Parse(data[5]), int.Parse(data[6]));

                    vezovi.Add(noviVez);
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

        private string ParsirajVrstaVeza(string vrsta)
        {
            string vrstaOznaka;

            if (vrsta.ToUpper().Equals(VrstaVeza.Putnicki))
                vrstaOznaka = VrstaVeza.Putnicki;
            else if (vrsta.ToUpper().Equals(VrstaVeza.Poslovni))
                vrstaOznaka = VrstaVeza.Poslovni;
            else if (vrsta.ToUpper().Equals(VrstaVeza.Ostali))
                vrstaOznaka = VrstaVeza.Ostali;
            else
                throw new NeispravanRedakDatotekeException("Nepostojeci vrsta veza");

            return vrstaOznaka;
        }

        private void ProvjeriPodatkeVeza(string[] data)
        {
            ProvjeriPodat(data);
        }

        public List<Vez> getList()
        {
            return vezovi;
        }
    }
}
