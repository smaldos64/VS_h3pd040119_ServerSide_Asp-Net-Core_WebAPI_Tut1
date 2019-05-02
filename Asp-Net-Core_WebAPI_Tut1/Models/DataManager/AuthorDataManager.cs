using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp_Net_Core_WebAPI_Tut1.Models.Repository;
using Asp_Net_Core_WebAPI_Tut1.Data;

using Microsoft.EntityFrameworkCore;

namespace Asp_Net_Core_WebAPI_Tut1.Models.DataManager
{
    public class AuthorDataManager : IDataRepository<Author, AuthorDto>
    {
        readonly DatabaseContext _databaseContext;

        public AuthorDataManager(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public IEnumerable<Author> GetAll()
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = false;

            var authors = this._databaseContext.Authors.Include(author => author.AuthorContact).ToList();
            return (authors);
        }

        // DTO Loading
        public IEnumerable<AuthorDto> GetAllDto()
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = true;

            List<Author> AuthorList = this._databaseContext.Authors.ToList();
            List<AuthorDto> AuthorListDto = new List<AuthorDto>();

            foreach (Author ThisAuthor in AuthorList)
            {
                AuthorListDto.Add(AuthorDtoMapper.MapToDto(ThisAuthor));
            }

            return AuthorListDto;
        }

        public Author Get(long id)
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = false;

            var author = this._databaseContext.Authors.SingleOrDefault(a => a.AuthorID == id);

            if (author == null)
            {
                return null;
            }

            this._databaseContext.Entry(author)
                .Reference(a => a.AuthorContact)
                .Load();

            return author;
        }

        public AuthorDto GetDto(long id)
        {
            this._databaseContext.ChangeTracker.LazyLoadingEnabled = true;

            //using (var context = new DatabaseContext())
            //{
            //    var author = context.Authors
            //           .SingleOrDefault(b => b.AuthorID == id);
            //    return AuthorDtoMapper.MapToDto(author);
            //}

            var author = this._databaseContext.Authors
                       .SingleOrDefault(b => b.AuthorID == id);
            return AuthorDtoMapper.MapToDto(author);
        }

        public void Add(Author entity)
        {
            this._databaseContext.Authors.Add(entity);
            this._databaseContext.SaveChanges();
        }

        public void Update(Author entityToUpdate, Author entity)
        {
            entityToUpdate = this._databaseContext.Authors
                .Include(a => a.BookAuthors)
                .Include(a => a.AuthorContact)
                .Single(b => b.AuthorID == entityToUpdate.AuthorID);

            entityToUpdate.AuthorName = entity.AuthorName;

            entityToUpdate.AuthorContact.Address = entity.AuthorContact.Address;
            entityToUpdate.AuthorContact.ContactNumber = entity.AuthorContact.ContactNumber;

            var deletedBooks = entityToUpdate.BookAuthors.Except(entity.BookAuthors).ToList();
            var addedBooks = entity.BookAuthors.Except(entityToUpdate.BookAuthors).ToList();

            deletedBooks.ForEach(bookToDelete =>
                entityToUpdate.BookAuthors.Remove(
                    entityToUpdate.BookAuthors
                        .First(b => b.BookID == bookToDelete.BookID)));

            foreach (var addedBook in addedBooks)
            {
                addedBook.AuthorID = entityToUpdate.AuthorID;
                this._databaseContext.Entry(addedBook).State = EntityState.Added;
            }

            try
            {
                this._databaseContext.SaveChanges();
            }
            catch (Exception error)
            {
                var ThisError = error;
            }
        }

        public void Delete(Author entity)
        {
            this._databaseContext.Remove(entity);
            this._databaseContext.SaveChanges();
        }
    }

    public class AuthorContactDto
    {
        public AuthorContactDto()
        {

        }

        public long AuthorId { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
    }

    public class AuthorDto
    {
        public AuthorDto()
        {
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public AuthorContactDto AuthorContact { get; set; }
    }

    public static class AuthorDtoMapper
    {
        public static AuthorDto MapToDto(Author author)
        {
            return new AuthorDto()
            {
                Id = author.AuthorID,
                Name = author.AuthorName,

                AuthorContact = new AuthorContactDto()
                {
                    AuthorId = author.AuthorID,
                    Address = author.AuthorContact.Address,
                    ContactNumber = author.AuthorContact.ContactNumber
                }
            };
        }
    }

}
