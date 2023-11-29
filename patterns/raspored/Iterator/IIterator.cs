using avulic.objects.composite.models;

namespace avulic.objects.Iterator
{
    public interface IIterator
    {
        object First();
        object Next();
        bool IsCompleted { get; }

    }
}
