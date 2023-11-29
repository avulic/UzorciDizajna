using avulic.objects.logger;
using avulic.objects.singleton;
using avulic.objects.tablica;


namespace avulic.akcije.korisnicke
{
    public class T : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        TablePrinter printer = TablePrinter.DohvatiTablePrinter();
        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();

        string[] _attribute;

        public T(string[] args)
        {
            _attribute = args;
        }
        public void IzvrsiAkciju()
        {

            PostaviPrinter(_attribute);

            luka.logger.Message("Uspješno izmjenjene postavke ispisa tablice", LogLevel.Info);
        }

        private void PostaviPrinter(string[] attribute)
        {
            printer.Reset();

            if (attribute.Contains("Z"))
            {
                printer.PrintajZaglavlje();
            }

            if (attribute.Contains("P"))
            {
                printer.PrintajPodnozje();
            }

            if (attribute.Contains("RB"))
            {
                printer.PrintajRedniBroj();
            }
        }
    }
}
