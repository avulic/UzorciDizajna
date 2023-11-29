

using avulic.objects;
using avulic.parsers.factory;

namespace avulic.parsers
{
    //konkri kretor
    public class KreatorParsera : ICreator
    {
        public IParser KreirajParserFM(string naredba, string imeDatoteke)
        {
            if (naredba.Equals("-v"))
            {
                return new ParserVez(imeDatoteke);
            }
            if (naredba.Equals("-l"))
            {
                return new ParserLuka(imeDatoteke);
            }
            if (naredba.Equals("-b"))
            {
                return new ParserBrod(imeDatoteke);
            }
            if (naredba.Equals("-r"))//opcionalno
            {
                return new ParserStavkeRasporeda(imeDatoteke);
            }
            if (naredba.Equals("-m"))
            {
                return new ParserMol(imeDatoteke);
            }
            if (naredba.Equals("-mv"))
            {
                return new ParserMolVez(imeDatoteke);
            }
            if (naredba.Equals("-k"))
            {
                return new ParserKanal(imeDatoteke);
            }
            if (naredba.Equals("UR"))
            {
                return new ParserZahtjevRezrevacije(imeDatoteke);
            }
            else
            {
                throw new NeispravnaNaredbeException("Unknown action type detected!");
            }
        }
    }
}
