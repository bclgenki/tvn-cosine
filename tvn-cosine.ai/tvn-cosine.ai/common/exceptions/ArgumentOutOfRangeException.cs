﻿namespace tvn.cosine.ai.common.exceptions
{
    public class ArgumentOutOfRangeException : Exception
    {
        public ArgumentOutOfRangeException()
            : this("")
        { }

        public ArgumentOutOfRangeException(string message)
            : base(message)
        { }

        public ArgumentOutOfRangeException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}