using avulic.objects.composite.models;

namespace avulic.objects.raspored
{
    public class StavkaRasporeda : IPrototype
    {
        public int id_vez { get; set; }
        public int id_brod { get; set; }
        public DayOfWeek dan_u_tjednu { get; set; }
        public TimeOnly vrijeme_od { get; set; }
        public TimeOnly vrijeme_do { get; set; }

        public StavkaRasporeda()
        {

        }
        public StavkaRasporeda(int id_vez, int id_brod, DayOfWeek dan_u_tjednu, TimeOnly vrijeme_od, TimeOnly vrijeme_do)
        {
            PostaviPodatke(id_vez, id_brod, dan_u_tjednu, vrijeme_od, vrijeme_do);
        }
        public IPrototype Kloniraj()
        {
            return new StavkaRasporeda();
        }
        public void PostaviPodatke(int id_vez, int id_brod, DayOfWeek dan_u_tjednu, TimeOnly vrijeme_od, TimeOnly vrijeme_do)
        {
            this.id_vez = id_vez;
            this.id_brod = id_brod;
            this.dan_u_tjednu = dan_u_tjednu;
            this.vrijeme_od = vrijeme_od;
            this.vrijeme_do = vrijeme_do;
        }
        public string[] ToStringAtributi()
        {
            string[] atributiZahtjev = { "id_vez", "id_brod", "dani_u_tjednu", "vrijeme_od", "vrijeme_do" };
            return atributiZahtjev;
        }
        public string[] ToStringData()
        {
            string[] data = { id_vez.ToString(), id_brod.ToString(), dan_u_tjednu.ToString(), vrijeme_od.ToString(), vrijeme_do.ToString() };
            return data;
        }
    }
}
