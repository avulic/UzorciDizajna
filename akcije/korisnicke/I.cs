using avulic.objects;
using avulic.objects.singleton;
using avulic.objects.tablica;



namespace avulic.akcije.korisnicke
{
    public class I : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        TablePrinter printer = TablePrinter.DohvatiTablePrinter();
        AbstractTable tablica = new TableBasic();
        VirtualnoVrijemeSingleton virtualnoVrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();

        public void IzvrsiAkciju()
        {
            //AzurirajStatuseVezova();

            Console.WriteLine(virtualnoVrijeme.ToString());

            IspisiTablicu();
        }

        private void IspisiTablicu()
        {
            List<RowData> redovi = new List<RowData>();

            //TO-DO - korsitit Iterator
            foreach (Vez vez in luka.vezovi)
            {
                string vezId = vez.oznaka_veza;
                string status = vez.status.ToString();

                redovi.Add(new RowData(new[] { vezId, status }));
            }

            List<RowData> zaglavlje = new List<RowData>();
            string[] stupci = { "Vez", "Status" };
            zaglavlje.Add(new RowData(stupci));

            tablica.PostaviSadrzaj(redovi);
            tablica.Naziv = "Naziv tablice";
            tablica.Stupci = stupci;

            printer.IspisiTablivu(tablica, zaglavlje, redovi);
        }


    }
}
