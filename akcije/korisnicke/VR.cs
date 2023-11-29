using avulic.objects;
using avulic.objects.composite.models;
using avulic.objects.Iterator;
using avulic.objects.logger;
using avulic.objects.raspored;
using avulic.objects.singleton;
using System.Reflection;
using StatusVeza = avulic.objects.StatusVeza;

namespace avulic.akcije.korisnicke
{
    public class VR : IAkcija
    {
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        string _attribute;

        public VR(string[] args)
        {
            string datum = args[1];
            string vrijeme = args[2];

            _attribute = datum + " " + vrijeme;
        }
        public void IzvrsiAkciju()
        {
            //To-DO - izvrsit sve potrebene Update stanja nakon proomejne vremena
            luka.virtualno_vrijeme.PostaviNovoVrijeme(_attribute);

            AzurirajStatuseVezova();

            luka.logger.Message("Vrijeme uspješno promjenjeno", LogLevel.Info);
        }

        public void AzurirajStatuseVezova()
        {
            List<Vez> vezovi = luka.vezovi;
            RasporedSingleton raspored = luka.raspored;

            TimeOnly trenutnoVrijeme = VirtualnoVrijemeSingleton.DohvatiVritualnoVrijeme().DohvatiTrenutnoVrijeme();

            IIterator daniIterator = raspored.DohvatiDane().DohvatiIterator();
            for (Dan dan = (Dan)daniIterator.First(); !daniIterator.IsCompleted; dan = (Dan)daniIterator.Next())
            {
                IIterator stavkeIterator = dan.DohvatiStavke().DohvatiIterator();
                for (StavkaRasporeda stavka = (StavkaRasporeda)stavkeIterator.First(); !stavkeIterator.IsCompleted; stavka = (StavkaRasporeda)stavkeIterator.Next())
                {
                    Vez vez = vezovi.Find(vez => vez.id == stavka.id_vez);
                    vez.status = StatusVeza.Slobodan;
                    if (trenutnoVrijeme.IsBetween(stavka.vrijeme_od, stavka.vrijeme_do))
                    {
                        vez.status = StatusVeza.Zauzet;
                    }
                    //if (!trenutnoVrijeme.IsBetween(stavka.vrijeme_od, stavka.vrijeme_do))
                    //{
                    //    Vez vez = vezovi.Find(vez => vez.id == stavka.id_vez);
                    //    vez.status = StatusVeza.Slobodan;
                    //}
                }
            }

            LukaComponent<Component> root = new LukaComponent<Component>("","");
            Component mool1 = new MolComponent<Component>("", "");
            Component mol2 = new MolComponent<Component>("", "");
            Component vez1 = new VezComponent<Component>();
            Component brod1 = new BrodComponent("", "");

            root.Add(mool1);
            root.Add(mol2);
            mool1.Add(vez1);
            vez1.Add(brod1);

            var iterator = root.DohvatiIterator();

            for (Component vez = iterator.First(); !iterator.IsCompleted; vez = iterator.Next())
            {
                
            }
        }
    }
}
