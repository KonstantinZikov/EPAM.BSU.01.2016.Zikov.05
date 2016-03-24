using System.Collections.Generic;

namespace Task1
{
    public interface BookDao
    {
        IEnumerable<Book> ReadBooks();
        void WriteBooks(IEnumerable<Book> books);
    }
}
