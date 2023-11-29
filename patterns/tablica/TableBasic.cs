namespace avulic.objects.tablica
{
    public class RowData
    {
        public List<string> Data { get; set; }

        public RowData(List<string> data)
        {
            Data = data;
        }
        public RowData(string[] data)
        {
            Data = data.ToList();
        }

        public string[] ToArray()
        {
            string[] array = Data.ToArray();

            return array;
        }
    }

    public class TableBasic : AbstractTable
    {
        public List<RowData> Rows { get; set; }

        public override string Naziv { get; set; }

        public override string[] Stupci { get; set; }


        public TableBasic()
        {
            Rows = new List<RowData>();
        }

        public void Print2()
        {
            int maxLength = 0;
            foreach (RowData row in Rows)
            {
                foreach (string item in row.Data)
                {
                    if (item.Length > maxLength)
                        maxLength = item.Length;
                }
            }

            foreach (RowData row in Rows)
            {
                foreach (string item in row.Data)
                {
                    if (double.TryParse(item, out double result))
                    {
                        Console.Write("{0,10}", item);
                    }
                    else
                    {
                        Console.Write("{0,-10}", item);
                    }
                }
                Console.WriteLine();
            }
        }

        public override void PostaviSadrzaj(List<RowData> rows)
        {
            foreach (RowData row in rows)
            {
                Rows.Add(row);
            }
        }

        public override List<RowData> DohvatiRedke()
        {

            return Rows;
        }

    }
}






