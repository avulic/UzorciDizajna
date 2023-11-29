namespace avulic.objects.visitor
{
    public interface IElement
    {
        void accept(IVisitor visitor);
    }
}
