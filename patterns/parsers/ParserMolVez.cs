using avulic.objects;
using avulic.objects.singleton;
using avulic.parsers.factory;
using avulic.patterns.exceptions;
using avulic_zadaca_3.objects;
using avulic_zadaca_3.objects.singleton;

namespace avulic.parsers
{
    //product
    public class ParserMolVez : IParser
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();

        List<Vez> vezovi;
        List<Mol> molovi;

        public ParserMolVez(string fileName)
        {
            _fileName = fileName;
            atributi = new[] { "id_mol", "id_vez" };

            molovi = luka.molovi;
            vezovi = luka.vezovi;
        }
        public override List<Mol> Parsiraj()
        {
            try
            {
                string[] lines = DohvatiPodatke();

                //ProvjeriZaglavlje(lines);

                ParsirajPodatke(lines.Skip(1).ToArray());
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Datoteka " + _fileName + " nije pronađena!");
            }
            catch (NeispravnoZaglavljekDatotekeException e)
            {
                Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno zaglalje! " + e.Message + "\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Greška u ParserMolVez.Parsiraj(): " + e.Message);
            }

            return molovi;
        }

        private void ParsirajPodatke(string[] lines)
        {
            foreach (string line in lines)
            {
                try
                {
                    string[] data = line.Split(";");

                    ProvjeriPodatke(data);

                    string mol = data[0];
                    string[] vezovi = data[1].Split(",");

                    foreach (var vezId in vezovi)
                    {
                        DodajVezMolu(mol, vezId);
                    }
                }
                catch (NeispravanRedakDatotekeException e)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Nepotpuni podaci: " + e.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"{++Program.brojGreskiSustava} Neispravno format vremena! " + line);
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine("Null reference kod vezove_mol, ParserVezMol: {0}", e.Message);
                }
            }
        }

        private void DodajVezoveMolu(string molId, string[] vezoviId)
        {
            Vez? vez;

            Mol mol = molovi.First(item => item.id == int.Parse(molId));

            foreach (var vezId in vezoviId)
            {
                vez = vezovi.FirstOrDefault(item => item.id == int.Parse(vezId));
                if (vez == null)
                    Console.WriteLine($"{++Program.brojGreskiSustava}" + $"Vez ne postoji, vezId: {vezId}, molId: {mol.id}");

                mol.GetVezovi().AddVez(vez);
            }
        }

        private void DodajVezMolu(string molId, string vezId)
        {
            Vez? vez = vezovi.FirstOrDefault(item => item.id == int.Parse(vezId));

            if (vez == null)
            {
                Console.WriteLine($"{++Program.brojGreskiSustava}" + $"Vez ne postoji, vez ID: {vezId}");
                return;
            }

            Mol mol = molovi.First(item => item.id == int.Parse(molId));
            //karakterisitke vezova dali pašu u luku
            //maksimalni kapacitet
            if (luka.DohvatiMaxDopustenBrojVezovaVrste(vez.vrsta) <= luka.DohvatiTrenutniBrojVezova(vez.vrsta))
            {
                Console.WriteLine($"{++Program.brojGreskiSustava}" + $"Prekoračen broj vezova u luci, vezId: {vezId}, molId: {mol.id}");
                return;
            }

            mol.AddVez(vez);
        }


        private void ProvjeriPodatke(string[] data)
        {
            ProvjeriPodat(data);

            Mol? mol = molovi.FirstOrDefault(item => item.id == int.Parse(data[0]));

            if (mol == null)
            {
                throw new NeispravanRedakDatotekeException($"Mol ne postoji, {string.Join(" ", data)}");
            }
        }
    }
}
