using System.Collections.Generic;

namespace Task1
{
    public interface BookRepository
    {
        IEnumerable<Book> ReadBooks();
        void WriteBooks(IEnumerable<Book> books);
    }
}
