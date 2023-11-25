using Domain.Models;
using Domain.View_Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.BookServices
{
    public class BookService : IBookService
    {
        #region Private Variables
        private readonly IRepository<Book> _book;

        public BookService(IRepository<Book> book)
        {
            _book = book;
        }
        #endregion


        #region GetAll
        public async Task<ICollection<BookViewModel>> GetAll()
        {
            ICollection<BookViewModel> bookViewModels = new List<BookViewModel>();
            ICollection<Book> books = await _book.GetAll();

            foreach (Book book in books)
            {
                BookViewModel viewModel = new()
                {
                    Id = book.Id,
                    BookID = book.BookID,
                    Title = book.Title,
                    ISBN = book.ISBN,
                };
                bookViewModels.Add(viewModel);
            }
            return bookViewModels;
        }
        #endregion

        #region GetById
        public async Task<BookViewModel> GetById(Guid id)
        {
            var book = await _book.GetById(id);
            if (book == null)
            {
                return null;
            }
            else
            {
                BookViewModel viewModel = new()
                {
                    Id = book.Id,
                    BookID = book.BookID,
                    Title = book.Title,
                    ISBN = book.ISBN,
                };
                return viewModel;
            }
        }
        #endregion

        #region GetLast
        public Book GetLast()
        {
            return _book.GetLast();
        }
        #endregion

        #region Insert
        public Task<bool> Insert(BookInsertModel BookInsertModel)
        {
            Book book = new()
            {
                BookID = BookInsertModel.BookID,
                Title = BookInsertModel.Title,
                ISBN = BookInsertModel.ISBN
            };
            return _book.Insert(book);

        }
        #endregion

        #region Update
        public async Task<bool> Update(BookUpdateModel BookUpdateModel)
        {
            var book = await _book.GetById(BookUpdateModel.Id);
            if (book != null)
            {
                book.Title = BookUpdateModel.Title;
                book.ISBN = book.ISBN;

                var result = await _book.Update(book);
                return result;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                Book book = await _book.GetById(id);
                if (book != null)
                {
                    _ = await _book.Delete(book);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }
        #endregion

        #region Find
        public Task<Book> Find(Expression<Func<Book, bool>> match)
        {
            return _book.Find(match);
        }
        #endregion

    }
}
