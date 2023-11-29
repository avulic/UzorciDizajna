using avulic.akcije.korisnicke;

namespace avulic.akcije
{
    public class CreatorNaredbi : ICreator
    {

        public IAkcija KreirajAkcijuFM(string userInput)
        {

            if (userInput.StartsWith("VR"))
            {
                string[] Attributes = userInput.Split(' ');
                return new VR(Attributes);
            }
            else if (userInput.StartsWith("I"))
            {
                return new I();
            }
            else if (userInput.StartsWith("T"))
            {
                string[] Attributes = userInput.Split(' ');
                return new T(Attributes);
            }
            else if (userInput.StartsWith("Q"))
            {
                return new Q();
            }
            else if (userInput.StartsWith("ZA"))
            {
                string[] Attributes = userInput.Split(' ');
                return new ZA(Attributes);
            }
            else if (userInput.StartsWith("ZP"))
            {
                string[] Attributes = userInput.Split(' ');
                return new ZP(Attributes);
            }
            else if (userInput.StartsWith("L"))
            {
                string[] Attributes = userInput.Split(' ');
                return new L(Attributes);
            }
            else if (userInput.StartsWith("V"))
            {
                string[] Attributes = userInput.Split(' ');
                return new V(Attributes);
            }
            else if (userInput.StartsWith("ZD"))
            {
                string[] Attributes = userInput.Split(' ');
                return new ZD(Attributes);
            }
            else if (userInput.StartsWith("UR"))
            {
                string[] Attributes = userInput.Split(' ');
                return new UR(Attributes);
            }
            else if (userInput.StartsWith("SPS"))
            {
                string[] Attributes = userInput.Split(' ');
                return new SPS(Attributes);
            }
            else if (userInput.StartsWith("VPS"))
            {
                string[] Attributes = userInput.Split(' ');
                return new VPS(Attributes);
            }
            else
            {
                throw new NeispravnaNaredbeException("Akcija nepodržana");
            }
        }
    }
}
