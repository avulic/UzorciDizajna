namespace avulic.objects.logger
{
    public enum LogLevel
    {
        None = 0,
        Info = 1,
        Debug = 2,
        Warning = 4
    }

    public abstract class AbstractLogger
    {
        protected LogLevel logMask;

        protected AbstractLogger next;



        public AbstractLogger(LogLevel mask)
        {
            logMask = mask;
        }


        public void SetNext(AbstractLogger log)
        {
            next = log;
            //return log;
        }

        public void Message(string msg, LogLevel severity)
        {
            if ((severity & logMask) != LogLevel.None)
            {

                WriteMessage(msg);
            }
            if (next != null)
            {
                next.Message(msg, severity);
            }
        }

        abstract protected void WriteMessage(string msg);
    }

}
