namespace avulic.patterns.exceptions
{
    [Serializable]
    public class NeispravnaNaredbeException : Exception
    {
        public NeispravnaNaredbeException() : base() { }
        public NeispravnaNaredbeException(string message) : base(message) { }
        public NeispravnaNaredbeException(string message, Exception inner) : base(message, inner) { }
    }

    public class NeispravanRedakDatotekeException : Exception
    {
        public NeispravanRedakDatotekeException() : base() { }
        public NeispravanRedakDatotekeException(string message) : base(message) { }
        public NeispravanRedakDatotekeException(string message, Exception inner) : base(message, inner) { }
    }

    public class NeispravnoZaglavljekDatotekeException : Exception
    {
        public NeispravnoZaglavljekDatotekeException() : base() { }
        public NeispravnoZaglavljekDatotekeException(string message) : base(message) { }
        public NeispravnoZaglavljekDatotekeException(string message, Exception inner) : base(message, inner) { }
    }
}
