using avulic.objects.Iterator;


namespace avulic.objects.raspored
{
    public class DanCollection<T> : ICollectio<T>
    {
        //private List<StavkaRasporeda> stavke = new List<StavkaRasporeda>();
        //private Dictionary<Dan, StavkaCollection> sadrzaj = new Dictionary<Dan, StavkaCollection>();
        private List<Dan> sadrzaj = new List<Dan>();

        public IIterator<T> DohvatiIterator()
        {
            return new DanIterator<T>(this);
        }

        public int Count
        {
            get { return sadrzaj.Count; }
        }

        public void AddDan(Dan dan)
        {
            sadrzaj.Add(dan);
        }

        public Dan GetDan(Dan dan)
        {
            return sadrzaj.FirstOrDefault(dan);
        }

        public Dan? GetDan(DayOfWeek dan)
        {
            return sadrzaj.FirstOrDefault(d => d.DohvatiDan() == dan);
        }

        public Dan? GetDan(int dan)
        {
            return sadrzaj.FirstOrDefault(d => (int)d.DohvatiDan() == dan);
        }

        public bool AddStavkaZaDan(DayOfWeek dan, StavkaRasporeda stavka)
        {
            Dan? result = sadrzaj.FirstOrDefault(d => d.DohvatiDan().Equals(dan));
            if (result == null)
                return false;

            result.AddStavka(stavka);

            return true;
        }
        public bool AddStavkeZaDan(DayOfWeek dan, StavkaCollection stavke)
        {
            Dan? result = sadrzaj.FirstOrDefault(d => d.DohvatiDan().Equals(dan));
            if (result == null)
            {
                return false;
            }

            result.AddStavke(stavke);

            return true;
        }

        public bool GetStavkeZaDan(Dan dan, out StavkaCollection stavke)
        {
            stavke = sadrzaj.FirstOrDefault(dan).DohvatiStavke();
            if (stavke == null)
            {
                return false;
            }
            return true;
        }
        public bool GetStavkeZaDan(DayOfWeek dan, out StavkaCollection stavke)
        {
            stavke = sadrzaj.FirstOrDefault(d => d.DohvatiDan() == dan).DohvatiStavke();
            if (stavke == null)
            {
                return false;
            }
            return true;
        }


        private class DanIterator<T> : IIterator<T>
        {
            private DanCollection<T> collection;
            private int current = 0;
            private int step = 1;
            public DanIterator(DanCollection<T> collection)
            {
                this.collection = collection;
            }
            public T First()
            {
                current = 0;
                return collection.GetDan(current);
            }
            public T Next()
            {
                current += step;
                if (!IsCompleted)
                {
                    return collection.GetDan(current);
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
