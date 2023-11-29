using avulic.objects.composite.models;

namespace avulic.objects.observer
{
    public class Kanal : Subject, IPrototype
    {
        public int id { get; set; }
        public int frekvencija { get; set; }
        public int maksimalanBroj { get; set; }

        private List<Observer> pretplatniciPoruka = new List<Observer>();
        public string Poruka { get; set; }



        public Kanal()
        {

        }

        public Kanal(int id, int frekvencija, int maksimalanBroj)
        {
            PostaviPodatke(id, frekvencija, maksimalanBroj);
        }
        public IPrototype Kloniraj()
        {
            return new Kanal();
        }
        public void PostaviPodatke(int id, int frekvencija, int maksimalanBroj)
        {
            this.id = id;
            this.frekvencija = frekvencija;
            this.maksimalanBroj = maksimalanBroj;
        }
        public string[] ToStringAtributi()
        {
            string[] atributiBrod = { "id", "frekvencija", "maksimalanBroj" };
            return atributiBrod;
        }
        public string[] ToStringData()
        {
            string[] data = { id.ToString(), frekvencija.ToString(), maksimalanBroj.ToString() };
            return data;
        }
        internal int BrojSpojenihBrodova()
        {
            return pretplatniciPoruka.Count();
        }

        //To-DO može bacit NotImplementated za Kapetaniju
        internal bool BrodSpojen(Brod brod)
        {
            return pretplatniciPoruka.Any(b => b.GetId() == brod.id);
        }


        public override bool Spoji(Observer brod)
        {
            if (maksimalanBroj < pretplatniciPoruka.Count)
            {
                pretplatniciPoruka.Add(brod);
                return true;
            }

            return false;
        }
        public override bool Odjavi(Observer brod)
        {
            if (pretplatniciPoruka.Remove(brod))
                return true;

            return false;
        }

        private string message;
        public string State
        {
            get { return message; }
            set
            {
                message = value;
                Obavjesti();
            }
        }

        public override void Obavjesti()
        {
            foreach (Observer brod in pretplatniciPoruka)
            {
                brod.Update(message);
            }
        }
    }
}
