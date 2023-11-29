using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.objects.memento
{
    public class Memento
    {
        public string oznaka;

        public List<Vez> vezovi;

        public DateTime trenutnoVrijeme;

        public Memento(List<Vez> vezovi, string oznaka, DateTime trenutnoVrijeme)
        {
            this.vezovi = vezovi;
            this.oznaka = oznaka;
            this.trenutnoVrijeme = trenutnoVrijeme;

        }
    }
}
