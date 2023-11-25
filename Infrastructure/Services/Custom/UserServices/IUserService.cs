using Domain.Models;
using Domain.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.UserServices
{
    public interface IUserService
    {
        Task<ICollection<UserViewModel>> GetAllLibrarian();
        Task<ICollection<UserViewModel>> GetAllMember();
        Task<UserViewModel> GetLibrarianById(Guid id);
        Task<UserViewModel> GetMemberById(Guid id);
        User GetLast();
        Task<bool> InsertLibrarian(UserInsertModel userInsertModel );
        Task<bool> InsertMember(UserInsertModel userInsertModel);
        Task<bool> Update(UserUpdateModel userUpdateModel);
        Task<bool> Delete(Guid id);
        Task<User> Find(Expression<Func<User, bool>> match);
    }
}
