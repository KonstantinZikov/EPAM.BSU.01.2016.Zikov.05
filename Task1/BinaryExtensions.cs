using System.IO;

namespace Task1.BinaryExtensions
{

    internal static class BinaryExtensions
    {
        public static bool IsEnd(this BinaryReader reader) { return reader.PeekChar() == -1; }

        public static void Write(this BinaryWriter writer, Book book)
        {
            writer.Write(book.Title);
            writer.Write(book.Author);
            writer.Write(book.Year);
        }

        public static Book ReadBook(this BinaryReader reader)
        {
            return new Book(reader.ReadString(), reader.ReadString(), reader.ReadInt32());
        }
    }
}
