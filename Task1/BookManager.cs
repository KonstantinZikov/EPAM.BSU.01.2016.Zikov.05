using System;
using System.Collections.Generic;
using System.IO;

namespace Task1
{
    public class BookManager
    {
        private BookDao _dao;
        private List<Book> books = new List<Book>();
        public List<Book> Books
        {
            get { return new List<Book>(books); }
        }

        public BookManager(BookDao dao)
        {
            if (dao == null)
                throw new ArgumentNullException("dao is null.");
            _dao = dao;
        }

        public void AddBook(Book book)
        {
            if (books.Contains(book))
                throw new BookManagerException("the book has already been added to the collection.");
            books.Add(book);
        }

        public void RemoveBook(Book book)
        {
            if(!books.Remove(book))
                throw new BookManagerException("the book is not in the collection.");
        }

        public IEnumerable<Book> FindByTag(Func<Book,bool> tag)
        {
            if (tag == null)
                throw new BookManagerException("tag is null.");
            foreach (var book in books)
                if (tag(book))
                    yield return book;
        }

        public void SortBooksByTag(Comparison<Book> comparison)
        {
            if (comparison == null)
                throw new BookManagerException("comparison is null.");
            books.Sort(comparison);
        }

        public void LoadBooks()
        {
            try
            {
                books = new List<Book>(_dao.ReadBooks());
            }
            catch (IOException ex)
            {
                throw new BookManagerException($"Can't load books - {ex.Message}",ex);
            }
        }     
          
        public void SaveBooks()
        {
            try
            {
                _dao.WriteBooks(books);
            }
            catch (IOException ex)
            {
                throw new BookManagerException($"Can't save books - {ex.Message}", ex);
            }
        }

    }
}
