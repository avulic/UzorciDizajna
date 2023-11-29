

namespace avulic.objects.composite.Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool IsCompleted { get; }

    }
}
