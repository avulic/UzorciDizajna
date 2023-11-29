using avulic.objects;
using avulic.objects.raspored;
using avulic.objects.singleton;
using avulic.objects.tablica;



namespace avulic.akcije.korisnicke
{
    //Ispis zauzetih(Z) ili slobodnih(S) vezova određene vrste u danom vremenskom rasponu
    //pri čemu se ispisuje i vrijeme od i do kada je vez zauzet(Z) ili slobodan(S)
    public class V : IAkcija
    {
        string[] attributes;

        string vrsta_veza;
        StatusVeza status;
        DateTime datum_vrijeme_od;
        DateTime datum_vrijeme_do;

        VirtualnoVrijemeSingleton vrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme();

        public V(string[] args)
        {
            attributes = args;
        }

        private void ParsirajArgumente(string[] args)
        {
            vrsta_veza = args[0];

            datum_vrijeme_od = vrijeme.ParsirajDatumVrijeme(args[2] + " " + args[3]);
            datum_vrijeme_do = vrijeme.ParsirajDatumVrijeme(args[4] + " " + args[5]);

            if (args[1].Equals("Z"))
                status = StatusVeza.Zauzet;
            if (args[1].Equals("S"))
                status = StatusVeza.Slobodan;
            else
                Console.Write("Kriva komanda");
        }

        public void IzvrsiAkciju()
        {
            ParsirajArgumente(attributes);

            //Ispis tablice sa svim vezovima poslovne vrste koji su slobodni u intervalu
            //            od 11.10.2022. 11:43:2022 do 12.10.2022. 11:43:20
            Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();

            Dictionary<Vez, List<StavkaRasporeda>> vezovi = kapetanija.DohvatiSveVezoveZaInterval(vrsta_veza, status, datum_vrijeme_od, datum_vrijeme_do);

            IspisiTablicu(vezovi);
        }

        private void IspisiTablicu(Dictionary<Vez, List<StavkaRasporeda>> vezovi)
        {
            List<RowData> zaglavlje = new List<RowData>();
            string[] stupci = { "Vrsta", "Oznaka", "Dan", "Od", "Do" };
            zaglavlje.Add(new RowData(stupci));

            Vez lastVez = null;
            List<RowData> redovi = new List<RowData>();

            string oznaka_veza;
            string vrsta;
            string datum_od = null;
            string vrijeme_od = null;
            string vrijeme_do = null;

            List<StavkaRasporeda> stavke;
            foreach (Vez vez in vezovi.Keys)
            {
                oznaka_veza = vez.oznaka_veza;
                vrsta = vez.vrsta;

                vezovi.TryGetValue(vez, out stavke);
                foreach (StavkaRasporeda stavka in stavke)
                {
                    datum_od = stavke.FirstOrDefault().dan_u_tjednu.ToString();

                    vrijeme_od = stavke.FirstOrDefault().vrijeme_od.ToString();
                    vrijeme_do = stavke.FirstOrDefault().vrijeme_do.ToString();
                }

                string?[] data = new[] { vrsta, oznaka_veza, datum_od, vrijeme_od, vrijeme_do };
                redovi.Add(new RowData(data));
            }

            AbstractTable tablica = new TableBasic();
            tablica.PostaviSadrzaj(redovi);
            tablica.Naziv = $"{status} vezovi od {datum_vrijeme_od} do {datum_vrijeme_do}";
            tablica.Stupci = stupci;

            TablePrinter printer = TablePrinter.DohvatiTablePrinter();
            printer.IspisiTablivu(tablica, zaglavlje, redovi);
        }
    }
}
