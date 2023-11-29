using avulic.objects;
using avulic.objects.singleton;
using avulic.objects.tablica;
using avulic.objects.visitor;

namespace avulic.akcije.korisnicke
{
    public class ZA : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        TablePrinter printer = TablePrinter.DohvatiTablePrinter();
        AbstractTable tablica = new TableBasic();

        string _attribute;

        public ZA(string[] args)
        {
            string datum = args[1];
            string vrijeme = args[2];

            _attribute = datum + " " + vrijeme;
        }


        public void IzvrsiAkciju()
        {
            LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
            VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();

            DateTime _vrijem = vrijeme.ParsirajDatumVrijeme(_attribute);

            BrojacLuka brojac = new BrojacLuka(_vrijem);
            foreach (Vez vez in luka.vezovi)
            {
                brojac.visit(vez);
            }

            IspisiTablicu(brojac.GetCountByType());

            //luka.logger.Message();
        }

        private void IspisiTablicu(Dictionary<string, int> rezultat)
        {
            List<RowData> redovi = new List<RowData>();

            foreach (string key in rezultat.Keys)
            {
                string tip = key;
                string broj = rezultat[key].ToString();

                redovi.Add(new RowData(new[] { tip, broj }));
            }

            List<RowData> zaglavlje = new List<RowData>();
            string[] stupci = { "Tip", "Broj" };
            zaglavlje.Add(new RowData(stupci));

            tablica.PostaviSadrzaj(redovi);
            tablica.Naziv = "Naziv tablice";
            tablica.Stupci = stupci;

            printer.IspisiTablivu(tablica, zaglavlje, redovi);
        }
    }
}
