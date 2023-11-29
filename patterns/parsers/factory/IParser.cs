using avulic_zadaca_3.objects;
using System.Text;

namespace avulic.parsers.factory
{
    public abstract class IParser
    {
        public string[] atributi;
        public string _fileName;

        public abstract object Parsiraj();

        public static DateTime ParsirajDatumVrijeme(string vrijeme)
        {
            return DateTime.ParseExact(vrijeme, "dd.MM.yyyy. HH:mm:ss",
                               System.Globalization.CultureInfo.GetCultureInfo("hr-HR"));
        }

        public static TimeOnly ParsirajVrijeme(string vrijeme)
        {
            return TimeOnly.ParseExact(vrijeme, "HH:mm",
                               System.Globalization.CultureInfo.GetCultureInfo("hr-HR"));
        }

        public void ProvjeriZaglavlje(string[] lines)
        {
            string[] zaglavlje = lines[0].Split(";");

            if (zaglavlje.Count() != atributi.Count())
            {
                throw new NeispravnoZaglavljekDatotekeException($"Broj atributa neispravan: {zaglavlje.Count()}");
            }

            if (!atributi.SequenceEqual(zaglavlje))
            {
                throw new NeispravnoZaglavljekDatotekeException($"Nepotpuni atributi: {zaglavlje}");
            }
        }

        public string[] DohvatiPodatke()
        {
            //string RunningPath = AppDomain.CurrentDomain.BaseDirectory;
            //string FileName = string.Format("{0}resources\\" + _fileName, Path.GetFullPath(Path.Combine(RunningPath, @"..\..\..\")));
            //string[] lines = File.ReadAllLines(FileName, Encoding.UTF8);
            //string[] lines = File.ReadAllLines(@"..\resources\" + _fileName, Encoding.UTF8);

            string filePath = Path.GetFullPath(_fileName, Environment.CurrentDirectory);
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

            return lines;
        }

        public bool ProvjeriPodat(string[] data)
        {
            if (atributi.Count() != data.Count())
            {
                throw new NeispravanRedakDatotekeException($"{string.Join(" ", data)}");
            }

            foreach (string item in data)
            {
                if (string.IsNullOrEmpty(item))
                {
                    throw new NeispravanRedakDatotekeException($"{string.Join(" ", data)}");
                }
            }

            return true;
        }

        public double ParsDecimal(string data)
        {
            data = data.Replace(".", ",");

            bool res = double.TryParse(data, out double output);

            if (res)
            {
                return output;
            }

            throw new NeispravanRedakDatotekeException($" format decmalnog broja: {data}");
        }
    }
}
