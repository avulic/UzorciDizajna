using avulic.objects.observer;
using avulic_zadaca_3.objects.observer;

namespace avulic.objects.builder
{
    public enum StatusZahtjeva
    {
        Odobren = 1,
        Odbije = 0
    }

    public class Zahtjev
    {
        //public static int Id { get; set; }
        //public DateTime DatumVrijemeKreiranja { get; set; }


        public Brod Brod { get; set; }
        //public int id_brod { get; set; }
        //public DateTime DatumVrijemeOd { get; set; }
        public StatusZahtjeva? Status { get; set; }

        public int id_brod { get; set; }
        public DateTime datum_vrijeme_od { get; set; }
        public int trajanje_priveza_u_h { get; set; }

        //dodatno
        public DateTime datum_vrijeme_do { get; set; }


        public void DisplayReport()
        {

        }
    }
}
