using System.Globalization;

namespace avulic.objects.singleton
{
    public class VirtualnoVrijemeSingleton
    {
        private static readonly VirtualnoVrijemeSingleton instance = new VirtualnoVrijemeSingleton();

        private static DateTime _trenutnoVrijeme;
        private static TimeSpan _diff;
        protected VirtualnoVrijemeSingleton()
        {
            _trenutnoVrijeme = DateTime.Now;
        }
        public static VirtualnoVrijemeSingleton DohvatiVritualnoVrijeme()
        {
            return instance;
        }

        public string ToString()
        {
            return _trenutnoVrijeme.ToString("dd.MM.yyyy hh:mm:ss", CultureInfo.GetCultureInfo("hr-HR"));
        }

        public void PostaviNovoVrijeme(string novoVrijeme)
        {
            // "1.2.2022 21:30:255";
            DateTime _novoVrijeme = ParsirajDatumVrijeme(novoVrijeme);
            _diff = _novoVrijeme.Subtract(_trenutnoVrijeme);
            _trenutnoVrijeme = _trenutnoVrijeme.Add(_diff);
        }

        public void PostaviNovoVrijeme(DateTime novoVrijeme)
        {
            // "1.2.2022 21:30:255";
            _diff = novoVrijeme.Subtract(_trenutnoVrijeme);
            _trenutnoVrijeme = _trenutnoVrijeme.Add(_diff);
        }

        public void UsporediDatume(string novoVrijeme)
        {
            var dateString = "1.2.2022 21:30:255";
            _trenutnoVrijeme = ParsirajDatumVrijeme(novoVrijeme);
        }

        public TimeOnly DohvatiTrenutnoVrijeme()
        {
            TimeOnly vrijem = TimeOnly.FromDateTime(_trenutnoVrijeme);
            return vrijem;
        }

        public DateTime DohvatiTrenutnoDatumVrijeme()
        {
            return _trenutnoVrijeme;
        }


        public DateTime ParsirajDatumVrijeme(string vrijeme)
        {
            DateTime dateTime;

            try
            {
                dateTime = DateTime.ParseExact(vrijeme, "dd.MM.yyyy. hh:mm:ss",
                                 CultureInfo.GetCultureInfo("hr-HR"));
            }
            catch (FormatException e)
            {
                Console.WriteLine("VirtualnoVrijeme.ParsirajVrijeme() krivi format veremena");
                throw;
            }
            return dateTime;
        }

        public TimeOnly ParsirajVrijeme(string vrijeme)
        {

            TimeOnly Time;
            try
            {
                Time = TimeOnly.ParseExact(vrijeme, "HH:mm",
                               CultureInfo.GetCultureInfo("hr-HR"));
            }
            catch (FormatException e)
            {
                Console.WriteLine("VirtualnoVrijeme.ParsirajVrijeme() krivi foramt veremena");
                throw;
            }
            return Time;
        }
    }

}
