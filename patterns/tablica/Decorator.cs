namespace avulic.objects.tablica
{
    public abstract class Decorator : AbstractTable
    {
        protected AbstractTable Source;
        public override string Naziv { get; set; }
        public override string[] Stupci { get; set; }

        public Decorator(AbstractTable source)
        {
            Source = source;
            Naziv = source.Naziv;
            Stupci = source.Stupci;
        }

        public override abstract List<RowData> DohvatiRedke();

        //public override void OcistiSadrzaj()
        //{
        //    Source.OcistiSadrzaj();
        //}

        public override void PostaviSadrzaj(List<RowData> data)
        {
            Source.PostaviSadrzaj(data);
        }
    }

}

