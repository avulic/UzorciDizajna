using avulic.objects.Iterator;
using avulic.objects.raspored;
using avulic.objects.singleton;


namespace avulic.objects.visitor
{
    public class BrojacLuka : IVisitor
    {
        private Dictionary<string, int> _counts;
        TimeOnly _vrijeme;
        LukaSingleton luka = LukaSingleton.DohvatiLukaSingleton();
        RasporedSingleton raspored = RasporedSingleton.DohvatiRaspored();

        DayOfWeek _dan;

        public BrojacLuka(DateTime _attribute)
        {
            _counts = new Dictionary<string, int>();
            _vrijeme = TimeOnly.FromDateTime(_attribute);
            _dan = _attribute.DayOfWeek;
        }

        public void visit(Vez berth)
        {
            StavkaCollection stavkeDana = raspored.DohvatiStavkeDana(_dan);

            IIterator daniIterator = raspored.DohvatiDane().DohvatiIterator();
            for (StavkaCollection stavke = (StavkaCollection)daniIterator.First(); !daniIterator.IsCompleted; stavke = (StavkaCollection)daniIterator.Next())
            {
                IIterator stavkeIterator = stavke.DohvatiIterator();
                for (StavkaRasporeda stavka = (StavkaRasporeda)stavkeIterator.First(); !stavkeIterator.IsCompleted; stavka = (StavkaRasporeda)stavkeIterator.Next())
                {
                    if (_vrijeme.IsBetween(stavka.vrijeme_od, stavka.vrijeme_do) && berth.status == StatusVeza.Zauzet)
                    {
                        string boatType = berth.vrsta;
                        if (_counts.ContainsKey(boatType))
                        {
                            _counts[boatType]++;
                        }
                        else
                        {
                            _counts.Add(boatType, 1);
                        }
                    }
                }
            }
        }

        public Dictionary<string, int> GetCountByType()
        {
            return _counts;
        }

    }
}
