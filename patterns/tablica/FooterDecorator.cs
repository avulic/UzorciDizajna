namespace avulic.objects.tablica
{
    public class FooterDecorator : Decorator
    {

        public FooterDecorator(AbstractTable tabularData) : base(tabularData)
        {

        }

        public override List<RowData> DohvatiRedke()
        {
            List<RowData> podaci = new List<RowData>();
            podaci.AddRange(Source.DohvatiRedke());

            int count = podaci.Count;
            string[] rows = new string[Source.Stupci.Length];
            Stupci = Source.Stupci;

            rows[rows.Length - 1] = count.ToString();

            podaci.Add(new RowData(rows));

            return podaci;
        }
    }
}
