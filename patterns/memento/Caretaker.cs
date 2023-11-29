using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.objects.memento
{
    public class Caretaker
    {
        private List<Memento> mementos = new List<Memento>();


        public void Add(Memento state)
        {
            mementos.Add(state);
        }
        public Memento? Get(string oznaka)
        {
            return mementos.FirstOrDefault(m => m.oznaka.Equals(oznaka));
        }
    }
}
