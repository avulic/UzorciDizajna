using avulic.objects;
using avulic.parsers.factory;
using avulic.patterns.exceptions;
using avulic_zadaca_3.objects;

namespace avulic.parsers
{
    //product
    public class ParserMol : IParser
    {
        Mol mol = new Mol();
        List<Mol> molovi = new List<Mol>();

        public ParserMol(string fileName)
        {
            _fileName = fileName;
            atributi = mol.ToStringAtributi();
        }
        public override List<Mol> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                KreirajMolove(lines.Skip(1).ToArray());

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
                Console.WriteLine("Greška u ParserMol.Parsiraj(): " + e.Message);
            }
            return molovi;
        }

        private void KreirajMolove(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodatkeBroda(data);

                    Mol noviMol = (Mol)mol.Kloniraj();
                    noviMol.PostaviPodatke(int.Parse(data[0]), data[1]);

                    molovi.Add(noviMol);
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
    }
}
