using System;
using System.Linq;
using Task1;
using NLog;
using static Task1Console.StringSet;

namespace Task1Console
{
    class Program
    {
        static Logger logger; 
        static BookManager manager;
       
        static public void Initialize()
        {
            logger = LogManager.GetCurrentClassLogger();
            try
            {
                manager = new BookManager(DaoFactory.Dao);                
            }
            catch(Exception ex)
            {
                logger.Fatal(ex.Message);
                throw;
            }
            try
            {
                manager.LoadBooks();
            }
            catch (BookManagerException ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
            logger.Trace("Initialized successfully");
            Console.WriteLine(welcomeMessage);
        }

        static public void Finish()
        {
            Save();
            logger.Trace("Application finished");
        }

        static void Main(string[] args)
        {
            Initialize();           
            Console.WriteLine(helpMessage);
            MainLoop();
            Finish();
        }

        static void MainLoop()
        {
            string key = "";
            while (key != "7")
            {
                key = Console.ReadLine();
                switch (key)
                {
                    case "1": AddBook(); break;
                    case "2": RemoveBook(); break;
                    case "3": FindBy(); break;
                    case "4": SortBy(); break;
                    case "5": ShowBooks(); break;
                    case "6": Save(); break;
                    case "7": break;
                    case "help": Console.WriteLine(helpMessage); break;
                    default: Console.WriteLine("Unknown command."); break;
                }
            }
        }

        static void AddBook()
        {
            var book = InputBookData();
            if (book != null)
            {
                try
                {
                    manager.AddBook(book);
                    logger.Trace($"Book added: {book}");
                    Console.WriteLine("Added successfully");
                }
                catch (BookManagerException ex)
                {
                    logger.Error(ex.Message);
                    Console.WriteLine(ex.Message);
                    
                }
            }
        }

        static void RemoveBook()
        {
            var book = InputBookData();
            if (book != null)
            {
                try
                {
                    manager.RemoveBook(book);
                    logger.Trace($"Book removed: {book}");
                    Console.WriteLine("Removed successfully.");
                }
                catch (BookManagerException ex)
                {
                    logger.Error(ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static Book InputBookData()
        {
            Console.Write("Book title: ");
            var title = Console.ReadLine();
            if (title.Length == 0)
            {
                Console.WriteLine("Title can't be empty.");
                return null;
            }
            Console.Write("Author: ");
            var author = Console.ReadLine();
            if (author.Length == 0)
            {
                Console.WriteLine("Author can't be empty.");
                return null;
            }
            Console.Write("Year of publication: ");
            var yearString = Console.ReadLine();
            int year = -1;
            if (!int.TryParse(yearString, out year)
                || year < 0 || year > 2020)
            {
                Console.WriteLine(wrongYearInputMessage);
            }
            return new Book(title, author, year);
        }

        static void FindBy()
        {
            Func<Book, bool> tag;
            Console.WriteLine(findByMessage);
            var key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    Console.Write("Title: ");
                    var title = Console.ReadLine();
                    tag = Book => Book.Title == title;
                    break;
                case "2":
                    Console.Write("Author: ");
                    var author = Console.ReadLine();
                    tag = Book => Book.Author == author;
                    break;
                case "3":
                    Console.Write("Year: ");
                    int year;
                    var yearString = Console.ReadLine();
                    if (!int.TryParse(yearString, out year)
                    || year < 0 || year > 2020)
                    {
                        Console.WriteLine(wrongYearInputMessage);
                        return;
                    }
                    tag = Book => Book.Year == year;
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    return;
            }
            try
            {
                var books = manager.FindByTag(tag);
                if (books.Count() == 0)
                {
                    logger.Trace("Search books - no matches.");
                    Console.WriteLine("No matches.");
                }
                else
                {
                    logger.Trace($"Search books - {books.Count()} found.");
                    foreach (var book in books)
                    {
                        Console.WriteLine(book);
                    }
                }
            }
            catch (BookManagerException ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        static void SortBy()
        {
            Comparison<Book> comparison;
            Console.WriteLine(sortByMessage);
            var key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    comparison = (b1, b2) => b1.Title.CompareTo(b2.Title);
                    break;
                case "2":
                    comparison = (b1, b2) => b1.Author.CompareTo(b2.Author);
                    break;
                case "3":
                    comparison = (b1, b2) => b1.Year - b2.Year;
                    break;
                default:
                    Console.WriteLine("Unknown command.");
                    return;
            }
            try
            {
                manager.SortBooksByTag(comparison);
                logger.Trace("Books sorted.");
                Console.WriteLine("Sorted successfully");
            }
            catch (BookManagerException ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        static void ShowBooks()
        {
            var collection = manager.Books;
            logger.Trace("Books showed.");
            if (collection.Count == 0)
                Console.WriteLine("No books.");
            else
            {
                foreach (var book in collection)
                {
                    Console.WriteLine(book);
                }
            }
        }

        static void Save()
        {
            try
            {
                manager.SaveBooks();
                logger.Trace("Books saved.");
                Console.WriteLine("Saved successfully.");
            }
            catch (BookManagerException ex)
            {
                logger.Error(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
