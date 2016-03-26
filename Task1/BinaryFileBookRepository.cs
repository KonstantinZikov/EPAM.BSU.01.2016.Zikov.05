using System;
using System.Collections.Generic;
using Task1.BinaryExtensions;
using System.IO;

namespace Task1
{
    public class BinaryFileBookRepository : BookRepository
    {
        private string _path;

        public BinaryFileBookRepository(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path is null.");
            _path = path;
        }

        public IEnumerable<Book> ReadBooks()
        {
            using (var reader = new BinaryReader(File.Open(_path, FileMode.Open)))
                while (!reader.IsEnd())
                    yield return reader.ReadBook();          
        }

        public void WriteBooks(IEnumerable<Book> books)
        {
            using (var writer = new BinaryWriter(File.Open(_path, FileMode.Create)))
                foreach (var book in books)
                    writer.Write(book);
        }
    }
}
