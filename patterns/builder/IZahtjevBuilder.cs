namespace avulic.objects.builder
{
    public interface IZahtjevBuilder
    {
        public void SetIdBrod(int id_brod);
        public void SetDatumVrijemeOd(DateTime datum_vrijeme_od);
        public void SetTrajanje(int trajanje_priveza_u_h);
        public void Obradi();
        public void SpremiZahtjev();

        //public void SetParameter3(bool param3);

        //public void SetParameter4(bool? param3);
        //public void SetParameter5(bool param3);
        //public void SetParameter6(bool param3);
        public Zahtjev GetRequest();
    }
}
