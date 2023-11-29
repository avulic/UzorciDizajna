namespace avulic.objects.logger
{
    public class InfoLogger : AbstractLogger
    {
        public InfoLogger(LogLevel mask) : base(mask)
        {
            logMask = mask;
        }


        protected override void WriteMessage(string msg)
        {
            Console.WriteLine(msg);
        }
    }

}
