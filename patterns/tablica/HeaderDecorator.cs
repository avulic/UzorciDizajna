namespace avulic.objects.tablica
{
    public class HeaderDecorator : Decorator
    {
        protected List<RowData> Zaglavlje;

        public HeaderDecorator(AbstractTable source, List<RowData> zaglavlje) : base(source)
        {
            Zaglavlje = zaglavlje;
        }

        public override List<RowData> DohvatiRedke()
        {
            List<RowData> podaci = new List<RowData>();
            podaci.AddRange(Source.DohvatiRedke());

            string[] nazivTablice = new string[Source.Stupci.Length];
            nazivTablice[0] = Source.Naziv;
            Stupci = Source.Stupci;

            podaci.Insert(0, new RowData(Source.Stupci));
            podaci.Insert(0, new RowData(nazivTablice));

            return podaci;
        }
    }
}
