using avulic.objects.Iterator;
using avulic_zadaca_3.objects.Iterator;


namespace avulic.objects.raspored
{
    public class StavkaCollection : ICollectio
    {
        private List<StavkaRasporeda> stavke = new List<StavkaRasporeda>();


        public IIterator DohvatiIterator()
        {
            return new StavkaIterator(this);
        }

        public int Count
        {
            get { return stavke.Count; }
        }

        public void AddStavka(StavkaRasporeda stavka)
        {
            stavke.Add(stavka);
        }

        public StavkaRasporeda GetStavka(int IndexPosition)
        {
            if (stavke.Count > 0)
            {
                return stavke[IndexPosition];
            }

            return null;
        }

        private class StavkaIterator : IIterator
        {
            private StavkaCollection collection;
            private int current = 0;
            private int step = 1;
            public StavkaIterator(StavkaCollection collection)
            {
                this.collection = collection;
            }
            public object First()
            {
                current = 0;
                return collection.GetStavka(current);
            }
            public object Next()
            {
                current += step;
                if (!IsCompleted)
                {
                    return collection.GetStavka(current);
                }
                else
                {
                    return null;
                }
            }
            public bool IsCompleted
            {
                get { return current >= collection.Count; }
            }
        }
    }
}
