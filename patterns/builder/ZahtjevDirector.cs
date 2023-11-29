namespace avulic.objects.builder
{
    public class ZahtjevDirector
    {
        IZahtjevBuilder _builder;
        public ZahtjevDirector(IZahtjevBuilder builder)
        {
            _builder = builder;
        }
        public Zahtjev KreriajZahtjev(int id_brod, DateTime datum_vrijeme_od, int trajanje_priveza_u_h)
        {
            _builder.SetIdBrod(id_brod);
            _builder.SetDatumVrijemeOd(datum_vrijeme_od);
            _builder.SetTrajanje(trajanje_priveza_u_h);
            //_builder.SetParameter3(osobaUloga);
            return _builder.GetRequest();
        }
    }
}
