using avulic.objects.logger;
using avulic.objects.singleton;
using avulic.objects.tablica;


namespace avulic.akcije.korisnicke
{
    public class Q : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        TablePrinter printer = TablePrinter.DohvatiTablePrinter();

        public Q()
        {

        }
        public void IzvrsiAkciju()
        {
            luka.logger.Message("Dovidjorno", LogLevel.Info);
            Thread.Sleep(1000);

            Environment.Exit(0);
        }
    }
}
