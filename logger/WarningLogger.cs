namespace avulic.objects.logger
{
    public class WarningLogger : AbstractLogger
    {
        public WarningLogger(LogLevel mask) : base(mask)
        {
            logMask = mask;
        }

        protected override void WriteMessage(string msg)
        {
            Console.WriteLine(++Program.brojGreskiSustava + msg);
        }
    }
}
