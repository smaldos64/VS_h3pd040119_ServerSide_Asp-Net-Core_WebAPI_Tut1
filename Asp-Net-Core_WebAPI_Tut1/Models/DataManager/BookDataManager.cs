using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Net_Core_WebAPI_Tut1.Models;
using Asp_Net_Core_WebAPI_Tut1.Models.Repository;
using Asp_Net_Core_WebAPI_Tut1.Data;

using Microsoft.EntityFrameworkCore;  
// Obs husk at bruge dette NameSpace. Ellers er Include ikke mulig !!!

namespace Asp_Net_Core_WebAPI_Tut1.Models.DataManager
{
    public class BookDataManager : IDataRepository<Book, BookDto>
    {
        readonly DatabaseContext _databaseContext;

        public BookDataManager(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        // Eager Loading
        //public IEnumerable<Book> GetAll()
        //{
        //    var books = this._databaseContext.Books.Include(x => x.Publisher).Include(x => x.BookCategory).
        //        Include(x => x.BookAuthors).ToList();

        //    return (books);
        //}

        // Explicit loading
        //public IEnumerable<Book> GetAll()
        //{
        //    this._databaseContext.ChangeTracker.LazyLoadingEnabled = false;

        //    List<Book> BookList = this._databaseContext.Books.ToList();


        //    foreach (Book ThisBook in BookList)
        //    {
        //        this._databaseContext.Entry(ThisBook)
        //            .Collection(b => b.BookAuthors)
        //            .Load();

        //        this._databaseContext.Entry(ThisBook)
        //            .Reference(b => b.Publisher)
        //            .Load();

        //        this._databaseContext.Entry(ThisBook)
        //            .Reference(b => b.BookCategory)
        //            .Load();
        //    }

        //    return BookList;
        //}

        // Lazy loading
        public IEnumerable<Book> GetAll()
        {
            var books = this._databaseContext.Books.ToList();

            return (books);
        }

        // DTO Loading
        public IEnumerable<BookDto> GetAllDto()
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = true;

            List<Book> BookList = this._databaseContext.Books.ToList();
            List<BookDto> BookListDto = new List<BookDto>();

            foreach (Book ThisBook in BookList)
            {
                BookListDto.Add(BookDtoMapper.MapToDto(ThisBook));
            }

            return BookListDto;
        }

        // Explicit loading
        public Book Get(long id)
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = false;

            var book = this._databaseContext.Books.SingleOrDefault(b => b.BookID == id);

            if (book == null)
            {
                return null;
            }

            this._databaseContext.Entry(book)
                .Collection(b => b.BookAuthors)
                .Load();

            this._databaseContext.Entry(book)
                .Reference(b => b.Publisher)
                .Load();

            this._databaseContext.Entry(book)
                .Reference(b => b.BookCategory)
                .Load();

            return book;
        }

        // DTO Loading
        public BookDto GetDto(long id)
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = true;

            var book = this._databaseContext.Books
                       .SingleOrDefault(b => b.BookID == id);
            return BookDtoMapper.MapToDto(book);
        }

        public void Add(Book entity)
        {

        }

        public void Update(Book entityToUpdate, Book entity)
        {

        }

        public void Delete(Book entity)
        {

        }
    }

    // Klassen her bør placeres i CategoryDatamanager.cs filen, når den bliver implementeret.
    public class CategoryDto
    {
        public CategoryDto()
        {

        }

        public long BookCategoryID { get; set; }
        public string BookCategoryName { get; set; }
    }

    // Klassen her bør placeres i PublisherDatamanager.cs filen, når den bliver implementeret.
    public class PublisherDto
    {
        public PublisherDto()
        {

        }

        public long PublisherID { get; set; }
        public string PublisherName { get; set; }
    }

    public class BookAuthorsDto
    {
        public BookAuthorsDto()
        {

        }

        public long BookID;

        public long AuthorID;

        //public string AuthorName;

        public AuthorDto Author;

        //public BookAuthorsDto(long BookID, long AuthorID, string AuthorName)
        //{
        //    this.BookID = BookID;
        //    this.AuthorID = AuthorID;
        //    this.AuthorName = AuthorName;
        //}

        public BookAuthorsDto(long BookID, long AuthorID, AuthorDto Author)
        {
            this.BookID = BookID;
            this.AuthorID = AuthorID;
            this.Author = Author;
        }
    }

    public class BookDto
    {
        public BookDto()
        {
        }

        public long BookID { get; set; }

        public string Title { get; set; }

        public CategoryDto Category { get; set; }

        public PublisherDto Publisher { get; set; }

        public List<BookAuthorsDto> BookAuthors { get; set; }
    }

    public static class BookDtoMapper
    {
        public static BookDto MapToDto(Book book)
        {
            BookDto BookDto_Object = new BookDto();

            BookDto_Object.BookID = book.BookID;
            BookDto_Object.Title = book.Title;

            BookDto_Object.Category = new CategoryDto();
            BookDto_Object.Category.BookCategoryID = book.BookCategoryID;
            BookDto_Object.Category.BookCategoryName = book.BookCategory.BookCategoryName;

            BookDto_Object.Publisher = new PublisherDto();
            BookDto_Object.Publisher.PublisherID = book.PublisherID;
            BookDto_Object.Publisher.PublisherName = book.Publisher.PublisherName;

            BookDto_Object.BookAuthors = new List<BookAuthorsDto>();

            foreach (BookAuthor value in book.BookAuthors)
            {
                //BookDto_Object.BookAuthors.Add(new BookAuthorsDto(BookID: value.BookID, AuthorID: value.AuthorID, AuthorName: value.Author.AuthorName));
                BookDto_Object.BookAuthors.Add(new BookAuthorsDto(BookID: value.BookID, AuthorID: value.AuthorID, Author: AuthorDtoMapper.MapToDto(value.Author)));
            }

            return (BookDto_Object);
        }
    }
}
