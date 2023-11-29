using avulic.objects.builder;
using avulic.objects.observer;
using avulic.objects.singleton;


namespace avulic.objects
{
    public class ZahtjevRezervacijaBuilder : IZahtjevBuilder
    {

        Kapetanija kapetanija = Kapetanija.DohvatiKapetanijaSingleton();



        private Zahtjev _request;

        public ZahtjevRezervacijaBuilder()
        {
            _request = new Zahtjev();
        }

        public void SetIdBrod(int id_brod)
        {
            Brod brod;

            _request.id_brod = id_brod;

            if (kapetanija.DohvatiBrod(_request.id_brod, out brod))
            {
                //luka.logger.Message($"Brod ne postoji.", LogLevel.Info);
                return;
            }
            _request.Brod = brod;
        }

        public void SetDatumVrijemeOd(DateTime datum_vrijeme_od)
        {
            _request.datum_vrijeme_od = datum_vrijeme_od;
        }

        public void SetTrajanje(int trajanje_priveza_u_h)
        {
            _request.datum_vrijeme_do = _request.datum_vrijeme_od.AddHours(trajanje_priveza_u_h);
        }

        //TO-DO pronači najekonomičniji vez
        public void Obradi()
        {
            Vez? vez;

            //zahtjev mora biti minimalno 1 h
            if (_request.trajanje_priveza_u_h >= 1)
            {
                //luka.logger.Message($"Minimalno trajanje zahtjeva je 1h.", LogLevel.Info);
                SetStatus(StatusZahtjeva.Odbije);
                return;
            }

            //poklapanje s odobrenim rezervacijama
            if (kapetanija.RezervacijaZaDatumVrijemePostoji(_request))
            {
                //luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
                SetStatus(StatusZahtjeva.Odbije);
                return;
            }

            //preklapanje s rasporedom
            if (!kapetanija.DohvatiSlobodanVez(_request.Brod, out vez, _request.datum_vrijeme_od, _request.datum_vrijeme_do))
            {
                //luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
                SetStatus(StatusZahtjeva.Odbije);
                return;
            }

            //TO-DO pronači najekonomičniji vez
            if (kapetanija.BrodOdgovaraVezu(vez, _request.Brod))
            {
                //luka.logger.Message($"Ne postoji slobodan vez.", LogLevel.Info);
                SetStatus(StatusZahtjeva.Odbije);
                return;
            }

            SetStatus(StatusZahtjeva.Odbije);
        }

        public void SetStatus(StatusZahtjeva status)
        {
            _request.Status = status;
        }

        public void SpremiZahtjev()
        {
            kapetanija.PohraniZahtjev(_request);
        }

        public Zahtjev GetRequest()
        {
            return _request;
        }



        //public void SetParameter4(bool? param3)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetParameter5(bool param3)
        //{
        //    throw new NotImplementedException();
        //}

        //public void SetParameter6(bool param3)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
