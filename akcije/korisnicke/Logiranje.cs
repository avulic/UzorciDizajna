using avulic.objects.logger;
using avulic.objects.singleton;


namespace avulic.akcije.korisnicke
{
    public class L : IAkcija
    {
        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();
        DebugLogger logger = Program.logger;

        string _attribute;
        public L(string[] args)
        {
            string vrijeme = args[1];

            _attribute = vrijeme;
        }

        public void IzvrsiAkciju()
        {
            logger.Message(vrijeme.ToString(), LogLevel.Info);

            foreach (var item in logger.GetLog())
            {
                Console.WriteLine(item);
            }

            logger.Message("Debug function L.IzvrsiAkciju().", LogLevel.Debug);
        }
    }
}
