namespace avulic.objects.tablica
{
    public abstract class AbstractTable
    {
        public abstract string Naziv { get; set; }
        public abstract string[] Stupci { get; set; }

        public abstract void PostaviSadrzaj(List<RowData> data);

        public abstract List<RowData> DohvatiRedke();


    }
}

