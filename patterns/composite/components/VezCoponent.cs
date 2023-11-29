
using avulic.objects.composite.Iterator;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.objects.composite.models
{
    public enum StatusVeza
    {
        Zauzet = 0,
        Slobodan = 1
    }

    public static class VrstaVeza
    {
        public static readonly string Putnicki = "PU";
        public static readonly string Poslovni = "PO";
        public static readonly string Ostali = "OS";
    }


    public class VezComponent<T> : Component where T : Component
    {
        private List<Component> _children = new List<Component>();

        string name;
        string description;

        public int id { get; set; }
        public string oznaka_veza { get; set; }
        public string vrsta { get; set; }
        public int cijena_veza_po_satu { get; set; }
        public double maksimalna_duljina { get; set; }
        public double maksimalna_sirina { get; set; }
        public double maksimalna_dubina { get; set; }
        public StatusVeza status { get; set; }



        public VezComponent(int id, string oznaka_veza, string vrsta, int cijena_veza_po_satu, double maksimalna_duljina, double maksimalna_sirina, double maksimalna_dubina)
        {
            PostaviPodatke(id, oznaka_veza, vrsta, cijena_veza_po_satu, maksimalna_duljina, maksimalna_sirina, maksimalna_dubina);
        }

        public VezComponent()
        {

        }

        public void PostaviPodatke(int id, string oznaka_veza, string vrsta, int cijena_veza_po_satu, double maksimalna_duljina, double maksimalna_sirina, double maksimalna_dubina)
        {
            this.id = id;
            this.oznaka_veza = oznaka_veza;
            this.vrsta = vrsta;
            this.cijena_veza_po_satu = cijena_veza_po_satu;
            this.maksimalna_duljina = maksimalna_duljina;
            this.maksimalna_sirina = maksimalna_sirina;
            this.maksimalna_dubina = maksimalna_dubina;

            status = StatusVeza.Slobodan;
        }

        public string[] ToStringAtributi()
        {
            string[] atributiVez = { "id", "oznaka_veza", "vrsta", "cijena_veza_po_satu", "maksimalna_duljina", "maksimalna_sirina", "maksimalna_dubina" };
            return atributiVez;
        }
       
        public string[] ToStringData()
        {
            string[] data = { id.ToString(), oznaka_veza, vrsta, cijena_veza_po_satu.ToString(),
                            maksimalna_duljina.ToString(), maksimalna_sirina.ToString(), maksimalna_dubina.ToString() };
            return data;
        }

        public void accept(IVisitor visitor)
        {
            visitor.visit(this);
        }



        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override Component GetChild(int index)
        {
            return (Component)_children[index];
        }

        public override int Count()
        {
            return _children.Count;
        }

        public override void Operation()
        {
            foreach (Component component in _children)
            {
                component.Operation();
            }
        }

        public override void Print()
        {
            Console.WriteLine(name);
        }

        public IIterator<T> DohvatiIterator()
        {
            return new VezIterator<T>(this);
        }

        public override IPrototype Kloniraj()
        {
            return new VezComponent<Component>();
        }
    }
}