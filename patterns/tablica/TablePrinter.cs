using avulic_zadaca_3.objects.tablica;

namespace avulic.objects.tablica
{
    public class TablePrinter
    {
        private static readonly TablePrinter instance = new TablePrinter();

        static bool P = false;
        static bool Z = false;
        static bool RB = false;

        int[] columnWidths;

        protected TablePrinter()
        {
        }

        public static TablePrinter DohvatiTablePrinter()
        {
            return instance;
        }

        public void IspisiTablivu(AbstractTable tablica, List<RowData> header, List<RowData> footer)
        {
            AbstractTable tablicaNova = tablica;

            if (RB == true)
            {
                tablicaNova = new RedniBrojDecorator(tablicaNova);
            }

            if (P == true)
            {
                tablicaNova = new FooterDecorator(tablicaNova);
            }

            if (Z == true)
            {
                tablicaNova = new HeaderDecorator(tablicaNova, header);
            }

            Print3(tablicaNova);
        }

        internal void PrintajZaglavlje()
        {
            Z = true;
        }

        internal void PrintajPodnozje()
        {
            P = true;
        }

        internal void PrintajRedniBroj()
        {
            RB = true;
        }

        public void Print3(AbstractTable tablica)
        {
            IEnumerable<RowData> redovi = tablica.DohvatiRedke();


            columnWidths = GetColumnWidths(redovi.Skip(1));

            if (Z)
            {
                PrintZaglevlje(redovi.Take(2));
            }

            PrintSadrzaj(redovi);

            if (P)
                PrintajPodnozje(redovi.Last());

        }

        private void PrintZaglevlje(IEnumerable<RowData> zaglavlje)
        {

            PrintHorizontalLine();

            PrintSadrzajReda(zaglavlje.First(), true);

            PrintHorizontalLine();

            PrintSadrzajReda(zaglavlje.Last());

        }

        private void PrintSadrzaj(IEnumerable<RowData> sadrzaj)
        {
            if (Z)
            {
                sadrzaj = sadrzaj.Skip(2);
            }

            if (P)
            {
                sadrzaj = sadrzaj.Take(new Range(0, sadrzaj.Count() - 1));
            }

            PrintHorizontalLine();

            foreach (RowData row in sadrzaj)
            {
                PrintSadrzajReda(row);
            }
        }

        private void PrintajPodnozje(RowData podnozje)
        {
            PrintHorizontalLine();

            PrintSadrzajReda(podnozje);

            PrintHorizontalLine();
        }

        public int[] GetColumnWidths(IEnumerable<RowData> sadrzaj)
        {
            int columns = sadrzaj.First().Data.Count;
            int[] widths = new int[columns];
            int width = 0;

            foreach (RowData row in sadrzaj)
            {
                for (int i = 0; i < columns; i++)
                {
                    width = 0;
                    if (!string.IsNullOrEmpty(row.Data[i]))
                        width = row.Data[i].Length;

                    if (width > widths[i])
                    {
                        widths[i] = width;
                    }
                }
            }

            return widths;
        }

        public bool IsNumeric(string cell)
        {
            double result;
            return double.TryParse(cell, out result);
        }

        public void PrintSadrzajReda(RowData row)
        {
            Console.Write("|");
            for (int i = 0; i < row.Data.Count; i++)
            {
                string cell = row.Data[i];
                if (string.IsNullOrEmpty(cell))
                    cell = "";
                int width = columnWidths[i];

                Console.Write(" ");
                if (IsNumeric(cell))
                {
                    Console.Write(cell.PadLeft(width));
                }
                else
                {
                    Console.Write(cell.PadRight(width));
                }
                Console.Write(" |");
            }
            Console.WriteLine();
        }

        public void PrintSadrzajReda(RowData row, bool merge)
        {
            Console.Write("|");

            string cell = row.Data[0];
            if (string.IsNullOrEmpty(cell))
                cell = " ";

            int max = columnWidths.Sum() + columnWidths.Count() * 2;
            int pad = (int)Math.Ceiling(((double)max - cell.Length) / 2);

            Console.Write(" ");
            string novi = cell.PadLeft(pad + cell.Length);
            Console.Write(novi);

            novi = "|".PadLeft(pad + 1);
            Console.Write(novi);

            Console.WriteLine();
        }

        private void PrintHorizontalLine()
        {
            Console.Write("+");
            foreach (int width in columnWidths)
            {
                Console.Write(new string('-', width + 2) + "+");
            }
            Console.WriteLine();
        }

        internal void Reset()
        {
            P = false;
            Z = false;
            RB = false;
        }
    }
}
