using avulic.akcije;
using avulic.objects;
using avulic.objects.logger;
using avulic.objects.observer;
using avulic.objects.raspored;
using avulic.objects.singleton;
using avulic.parsers;
using avulic.parsers.factory;
using avulic.patterns.exceptions;
using System.Globalization;

namespace avulic
{
    public class Program
    {
        static string[] ObvezneAkcijaZaUcitavanje = { "-l", "-b", "-v", "-m", "-mv", "-k" };
        static string[] OpcionalneAkcijaZaUcitavanje = { "-r" };
        static Dictionary<string, string> akcije = new Dictionary<string, string>();

        static string[] korisnickeAkcije = { "I", "VR", "V", "UR", "ZD", "ZP", "Q", "F", "T", "ZA", "L" };

        static LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        static VirtualnoVrijemeSingleton virtualnoVrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();
        static public int brojGreskiSustava = 0;
        public static DebugLogger logger;


        public static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("hr-HR");

            try
            {
                args = Console.ReadLine().Split(" ");

                InicijalizirajSustav(args);

                CekajNaredbeKorisnika();
            }
            catch (NeispravanRedakDatotekeException e)
            {
                Console.WriteLine($"{e.Message}");
            }
            catch (NeispravnaNaredbeException e)
            {
                Console.WriteLine($"{++brojGreskiSustava} " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Nešto je pošlo po krivu! Program.Main(), {e.Message}");
            }
        }

        private static void InicijalizirajSustav(string[] args)
        {
            PostaviLogger();
            //logger.Message("Loggeri postavljeni.", LogLevel.Debug);

            ParsirajUlazneAkcije(args);

            ParsirajDatoteke(args);

            IspisiKomponente();
        }

        private static void IspisiKomponente()
        {
            //var harbour = new LukaComponent("ime", "opis");
            //var dock = new MolComponent("ime", "opis");
            //var vez = new VezComponent("ime", "opis");
            //var vez1 = new VezComponent("ime", "opis");

            //dock.Add(vez);
            //dock.Add(vez1);
            //harbour.Add(dock);


            //CompositeIterator iterator = new CompositeIterator(harbour);
            //for (Component component = iterator.First(); !iterator.IsDone(); component = iterator.Next())
            //{
            //    component.Print();
            //}


            //ConcreteAggregate collection = new ConcreteAggregate();
            //collection.DodajOrganizaciju(dijete);

            //IIterator i = collection.DohvatiIterator();
            //Component item = (Component)i.First();

            //while (item != null)
            //{
            //    item.DisplayStrukturu(depth + 2, null);
            //    item = i.Next();
            //}
        }

        private static void CekajNaredbeKorisnika()
        {
            CreatorNaredbi naredbeCreator = new CreatorNaredbi();
            string? userInput;
            IAkcija akcija;

            while (true)
            {
                Console.WriteLine($"{Environment.NewLine}=================== Program =====================");
                Console.Write("Unesite naredbu: ");
                userInput = Console.ReadLine();

                if (string.IsNullOrEmpty(userInput))
                {
                    throw new NeispravnaNaredbeException();
                }

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase) || userInput.Equals("quit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Write($"{Environment.NewLine}=================== KRAJ =====================");
                    Thread.Sleep(1000);
                    break; // Exit the loop
                }

                try
                {
                    akcija = naredbeCreator.KreirajAkcijuFM(userInput);
                    akcija.IzvrsiAkciju();
                }
                catch (NeispravnaNaredbeException e)
                {
                    throw e;
                }
            }
        }

        private static void PostaviLogger()
        {
            logger = new DebugLogger(LogLevel.Info | LogLevel.Debug);
            InfoLogger logger1 = new InfoLogger(LogLevel.Info);
            WarningLogger loggerWarning = new WarningLogger(LogLevel.Warning);

            logger.SetNext(logger1);
            logger1.SetNext(loggerWarning);

            luka.logger = logger;
        }

        public static void ParsirajDatoteke(string[] ulazneAkcije)
        {
            string imeDatoteke;
            IParser parser;

            KreatorParsera kreatorParsera = new KreatorParsera();

            akcije.TryGetValue("-k", out imeDatoteke);
            parser = kreatorParsera.KreirajParserFM("-k", imeDatoteke);
            luka.kanali = (List<Kanal>)parser.Parsiraj();

            akcije.TryGetValue("-l", out imeDatoteke);
            parser = kreatorParsera.KreirajParserFM("-l", imeDatoteke);
            luka = (LukaSingleton)parser.Parsiraj();
            if (!luka.PodaciPostavljeni())
                throw new NeispravanRedakDatotekeException("");

            akcije.TryGetValue("-b", out imeDatoteke);
            parser = kreatorParsera.KreirajParserFM("-b", imeDatoteke);
            luka.brodovi = (List<Brod>)parser.Parsiraj();

            akcije.TryGetValue("-v", out imeDatoteke);
            parser = kreatorParsera.KreirajParserFM("-v", imeDatoteke);
            luka.vezovi = (List<Vez>)parser.Parsiraj();

            akcije.TryGetValue("-m", out imeDatoteke);
            parser = kreatorParsera.KreirajParserFM("-m", imeDatoteke);
            luka.molovi = (List<Mol>)parser.Parsiraj();

            akcije.TryGetValue("-mv", out imeDatoteke);
            parser = kreatorParsera.KreirajParserFM("-mv", imeDatoteke);
            luka.molovi = (List<Mol>)parser.Parsiraj();

            if (akcije.TryGetValue("-r", out imeDatoteke))
            {
                parser = kreatorParsera.KreirajParserFM("-r", imeDatoteke);
                List<StavkaRasporeda> stavke = (List<StavkaRasporeda>)parser.Parsiraj();
                luka.raspored = RasporedSingleton.DohvatiRaspored();
                luka.raspored.DodajStavke(stavke);
            }
        }

        private static Dictionary<string, string> ParsirajUlazneAkcije(string[] args)
        {
            ProvjeriUlaznuAkciju(args);

            for (int i = 0; i < args.Length; i = i + 2)
            {
                akcije.Add(args[i], args[i + 1]);
            }

            return akcije;
        }

        public static void ProvjeriUlaznuAkciju(string[] args)
        {
            string naredba;
            string argument;

            args = args.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (args.Length % 2 != 0)
            {
                throw new NeispravnaNaredbeException("Naredba mora imati naredbu i putanju do datoteke");
            }

            for (int i = 0; i < args.Length; i += 2)
            {
                naredba = args[i];
                argument = args[i + 1];

                if (argument.StartsWith('-'))
                {
                    throw new NeispravnaNaredbeException("Naredba nema oblik '- naziv_datoteka' ");
                }

                if (!argument.Contains(".csv"))
                {
                    throw new NeispravnaNaredbeException("Tip datoteke mora biti csv");
                }

                if (!naredba.StartsWith('-'))
                {
                    throw new NeispravnaNaredbeException("Naredba nema oblik '- naziv_datoteka' ");
                }

                if (!(ObvezneAkcijaZaUcitavanje.Contains(naredba) || OpcionalneAkcijaZaUcitavanje.Contains(naredba)))
                {
                    throw new NeispravnaNaredbeException("Unesena naredba nije podržana");
                }

                if (args.Count(elem => elem.Equals(naredba)) > 1)
                {
                    throw new NeispravnaNaredbeException("Naredbe se ne smiju ponavljat");
                }
            }
        }
    }
}