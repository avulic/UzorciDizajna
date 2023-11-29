namespace avulic.objects.tablica
{
    public class RedniBrojDecorator : Decorator
    {

        public RedniBrojDecorator(AbstractTable tabularData) : base(tabularData)
        {
        }

        public override List<RowData> DohvatiRedke()
        {
            var array = new string[base.Stupci.Length + 1];
            array[0] = "Br.";
            base.Stupci.CopyTo(array, 1);

            Source.Stupci = array;
            Stupci = array;

            List<RowData> podaci = new List<RowData>();
            podaci.AddRange(Source.DohvatiRedke());

            int count = 1;
            foreach (RowData item in podaci)
            {
                item.Data.Insert(0, count.ToString());
                count++;
            }

            return podaci;
        }
    }
}
