using avulic.objects.Iterator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace avulic.objects.composite.models
{
    public abstract class Component : IPrototype
    {
        public abstract void Add(Component component);
        public abstract void Remove(Component component);
        public abstract Component GetChild(int index);
        public abstract void Operation();
        public abstract int Count();
        public abstract void Print();
        public abstract IPrototype Kloniraj();
    }
}
