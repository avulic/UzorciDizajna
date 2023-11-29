namespace avulic.objects.logger
{
    public class DebugLogger : AbstractLogger
    {
        public static List<string> log = new List<string>();

        public DebugLogger(LogLevel mask) : base(mask)
        {
            logMask = mask;
        }



        protected override void WriteMessage(string msg)
        {
            log.Add(msg);
        }

        public List<string> GetLog()
        {
            return log;
        }
    }
}
