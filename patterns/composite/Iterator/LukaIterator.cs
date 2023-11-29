using avulic.objects.composite.Iterator;
using avulic.objects.composite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.patterns.composite.Iterator
{
    public class LukaIterator<T> : IIterator<T> where T : Component
    {
        private LukaComponent<T> collection;
        private int current = 0;
        private int step = 1;

        public LukaIterator(LukaComponent<T> collection)
        {
            this.collection = collection;
        }

        public T First()
        {
            current = 0;
            return (T)collection.GetChild(current);
        }

        public T Next()
        {
            current += step;
            if (!IsCompleted)
            {
                return (T)collection.GetChild(current);
            }
            else
            {
                return null;
            }
        }

        public bool IsCompleted
        {
            get { return current >= collection.Count(); }
        }
    }
}
