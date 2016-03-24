namespace Task1
{
    public class Book
    {
        public Book() {}

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public string Title { get; set; } = "Default";
        public string Author { get; set; } = "Unknown";
        public int Year { get; set; } = 0;

        public override string ToString()
        {
            return $"{Title} {Author} {Year}";
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Book)) return false;
            return Equals(obj as Book);
        }

        public bool Equals(Book book)
        {
            if (ReferenceEquals(this, book)) return true;
            if (ReferenceEquals(book, null)) return false;
            if (Title.Equals(book.Title) &&
                Author.Equals(book.Author) &&
                Year == book.Year)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode() ^ Author.GetHashCode() ^ Year;
        }
    }
}
