using System;

namespace Task1
{
    public class BookManagerException : Exception
    {
        public BookManagerException() : base() { }
        public BookManagerException(string message) : base(message) { }
        public BookManagerException(string message, Exception innerException) : base(message, innerException) { }
    }
}
