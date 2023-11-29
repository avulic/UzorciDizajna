using avulic.parsers.factory;

namespace avulic.parsers
{
    public interface ICreator
    {
        public IParser KreirajParserFM(string naredba, string imeDatoteke);

    }
}
