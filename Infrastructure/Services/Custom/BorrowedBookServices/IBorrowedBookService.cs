using Domain.Models;
using Domain.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.BorrowedBookServices
{
    public interface IBorrowedBookService
    {
        Task<ICollection<BorrowedBookViewModel>> GetAll();
        Task<BorrowedBookViewModel> GetById(Guid id);
        Task<bool> Insert(BorrowedBookInsertModel BorrowedBookInsertModel);
        Task<bool> Update(BorrowedBookUpdateModel BorrowedBookUpdateModel);
        Task<bool> Delete(Guid id);
        Task<Borrowed_Book> Find(Expression<Func<Borrowed_Book, bool>> match);
    }
}
