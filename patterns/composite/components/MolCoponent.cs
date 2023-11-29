
using avulic.objects.composite.Iterator;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.objects.composite.models
{
    public class MolComponent<T> : Component, ICollectio<T> where T : Component
    {
        private List<Component> _children = new List<Component>();

        string name;
        string description;

        public MolComponent(string name, string description)
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
            return _children[index];
        }
        
        public override int Count()
        {
            return _children.Count;
        }

        public override void Operation()
        {
            Console.WriteLine("Composite operation");

            // 
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
            return new MolIterator<T>(this);
        }

        public override IPrototype Kloniraj()
        {
            return new MolComponent<Component>("","");
        }
    }
}
