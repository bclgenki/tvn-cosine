﻿namespace tvn.cosine.ai.common.exceptions
{
    public class ArgumentNullException : Exception
    {
        public ArgumentNullException(string message)
            : base(message)
        { }

        public ArgumentNullException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
