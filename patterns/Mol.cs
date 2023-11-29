using avulic.objects.composite.models;
using avulic.objects.Iterator;

namespace avulic.objects
{
    public class Mol : IPrototype
    {
        public int id { get; set; }
        public string naziv { get; set; }
        private VezCollection vezovi { get; set; }


        public Mol()
        {
            vezovi = new VezCollection();
        }

        public Mol(int id, string naziv)
        {
            PostaviPodatke(id, naziv);
        }

        public void PostaviPodatke(int id, string naziv)
        {
            this.id = id;
            this.naziv = naziv;
        }
        public string[] ToStringAtributi()
        {
            string[] atributiBrod = { "id", "naziv" };
            return atributiBrod;
        }
        public string[] ToStringData()
        {
            string[] data = { id.ToString(), naziv };
            return data;
        }

        public IPrototype Kloniraj()
        {
            return new Mol();
        }

        public VezCollection GetVezovi()
        {
            return vezovi;
        }

        public void AddVez(Vez vez)
        {
            vezovi.AddVez(vez);
        }
    }
}
