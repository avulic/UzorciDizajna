namespace avulic.objects.visitor
{
    public interface IVisitor
    {
        void visit(Vez berth);

        Dictionary<string, int> GetCountByType();
    }
}
