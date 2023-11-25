using Domain.Models;
using Domain.View_Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.BorrowedBookServices
{
    public class BorrowedBookService : IBorrowedBookService
    {
        #region Private variables And Constructor
        private readonly IRepository<Borrowed_Book> _borrowdBook;
        private readonly IRepository<User> _user;
        private readonly IRepository<Book> _book;

        public BorrowedBookService(IRepository<Borrowed_Book> borrowdBook, IRepository<User> user, IRepository<Book> book)
        {
            _borrowdBook = borrowdBook;
            _book = book;
            _user = user;
        }
        #endregion

        #region GetAll
        public async Task<ICollection<BorrowedBookViewModel>> GetAll()
        {
            ICollection<BorrowedBookViewModel> BorrowedBookViewModel = new List<BorrowedBookViewModel>();
            ICollection<Borrowed_Book> books = await _borrowdBook.GetAll();

            foreach(Borrowed_Book book in books)
            {
                BorrowedBookViewModel viewModel = new()
                { 
                    BorrowedBookID = book.Id,
                    BorrowDate = book.BorrowDate,
                    ReturnDate = book.ReturnDate,
                    BookID = book.BookID,
                    UserID = book.UserID
                };
                BorrowedBookViewModel.Add(viewModel);
            }
            return BorrowedBookViewModel;
        }
        #endregion

        #region GetById
        public async Task<BorrowedBookViewModel> GetById(Guid id)
        {
            var result = await _borrowdBook.GetById(id);
            if(result == null)
            {
                return null;
            }
            else
            {
                BorrowedBookViewModel viewModel = new()
                {
                    BorrowedBookID = result.Id,
                    BorrowDate = result.BorrowDate,
                    ReturnDate = result.ReturnDate,
                    BookID = result.BookID,
                    UserID = result.UserID
                };

                return viewModel;
            }

        }
        #endregion

        #region Insert
        public async Task<bool> Insert(BorrowedBookInsertModel BorrowedBookInsertModel)
        {
            var user = await _user.Find(x => x.Id == BorrowedBookInsertModel.UserID);
            var book = await _book.Find(x => x.Id == BorrowedBookInsertModel.BookID);

            var result = await _borrowdBook.Find(x => x.UserID == user.Id && x.BookID == book.Id);

            if(BorrowedBookInsertModel.UserID == user.Id && BorrowedBookInsertModel.BookID == book.Id)
            {
                Borrowed_Book viewModel = new()
                {
                    BorrowDate = BorrowedBookInsertModel.BorrowDate,
                    ReturnDate = BorrowedBookInsertModel.ReturnDate,
                    BookID = BorrowedBookInsertModel.BookID,
                    UserID = BorrowedBookInsertModel.UserID
                };

                var borrow = await _borrowdBook.Insert(viewModel);
                if (borrow == true)
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;

        }
        #endregion

        #region Update
        public async Task<bool> Update(BorrowedBookUpdateModel BorrowedBookUpdateModel)
        {
            Borrowed_Book borrowed_Book = await _borrowdBook.GetById(BorrowedBookUpdateModel.Id);

            borrowed_Book.BorrowDate = BorrowedBookUpdateModel.BorrowDate;
            borrowed_Book.ReturnDate = BorrowedBookUpdateModel.ReturnDate;
            borrowed_Book.UserID = borrowed_Book.UserID;
            borrowed_Book.BookID = borrowed_Book.BookID;

            var result = await _borrowdBook.Update(borrowed_Book);

            if (result == true)
            {
                return true;
            }
            else
                return false;

        }
        #endregion

        #region Delete
        public async Task<bool> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                Borrowed_Book book = await _borrowdBook.GetById(id);
                if (book != null)
                {
                   _ = _borrowdBook.Delete(book);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Find
        public Task<Borrowed_Book> Find(Expression<Func<Borrowed_Book, bool>> match)
        {
            return _borrowdBook.Find(match);
        }
        #endregion
    }
}
