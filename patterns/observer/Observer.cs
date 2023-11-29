namespace avulic.objects.observer
{
    abstract public class Observer
    {
        abstract public void Update(string message);
        abstract public void PosaljiPoruku(string message);


        abstract public int GetId();
    }
}
