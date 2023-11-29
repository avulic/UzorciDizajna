using avulic.objects.composite.models;
using avulic.objects.visitor;


namespace avulic.objects
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


    public class Vez : IPrototype, IElement
    {
        public int id { get; set; }
        public string oznaka_veza { get; set; }
        public string vrsta { get; set; }
        public int cijena_veza_po_satu { get; set; }
        public double maksimalna_duljina { get; set; }
        public double maksimalna_sirina { get; set; }
        public double maksimalna_dubina { get; set; }

        public StatusVeza status { get; set; }


        public Vez()
        {

        }
        public Vez(int id, string oznaka_veza, string vrsta, int cijena_veza_po_satu, double maksimalna_duljina, double maksimalna_sirina, double maksimalna_dubina)
        {
            PostaviPodatke(id, oznaka_veza, vrsta, cijena_veza_po_satu, maksimalna_duljina, maksimalna_sirina, maksimalna_dubina);

        }
        public IPrototype Kloniraj()
        {
            return new Vez();
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

    }
}
