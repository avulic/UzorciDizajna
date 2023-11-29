using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avulic.objects.composite.models
{
    public class BrodComponent : Component
    {
        string name;
        string description;

        public BrodComponent(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        public override void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public override Component GetChild(int index)
        {
            throw new NotImplementedException();
        }

        public override int Count()
        {
            throw new NotImplementedException();
        }


        public override void Operation()
        {
            Console.WriteLine("Leaf operation");
        }

        public override void Print()
        {
            Console.WriteLine(name);
        }

        public override IPrototype Kloniraj()
        {
            return new BrodComponent("", "");
        }
    }
}
