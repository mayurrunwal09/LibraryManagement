using Domain.Models;
using Domain.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.BookServices
{
    public interface IBookService
    {
        Task<ICollection<BookViewModel>> GetAll();
        Task<BookViewModel> GetById(Guid id);
        Book GetLast();
        Task<bool> Insert(BookInsertModel BookInsertModel);
        Task<bool> Update(BookUpdateModel BookUpdateModel);
        Task<bool> Delete(Guid id);
        Task<Book> Find(Expression<Func<Book, bool>> match);
    }
}
