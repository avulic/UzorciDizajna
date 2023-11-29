using avulic.objects.composite.Iterator;
using avulic.objects.composite.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.objects.composite.models
{
    public class LukaComponent<T> : Component, ICollectio<T> where T : Component
    {
        private List<Component> _children = new List<Component>();

        string name;
        string description;

        public LukaComponent(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override void Add(Component component)
        {
            _children.Add(component);
        }

        public override void Remove(Component component)
        {
            _children.Remove(component);
        }

        public override Component GetChild(int index)
        {
            return (Component)_children[index];
        }
        
        public override int Count()
        {
            return _children.Count;
        }

        public override void Operation()
        {
            foreach (Component component in _children)
            {
                component.Operation();
            }
        }

        public override void Print()
        {
            Console.WriteLine(name);
        }

        public IIterator<T> DohvatiIterator()
        {
            return new LukaIterator<T>(this);
        }

        public override IPrototype Kloniraj()
        {
            return new LukaComponent<Component>("","");
        }
    }
}
