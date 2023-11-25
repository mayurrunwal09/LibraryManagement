using Domain.Models;
using Domain.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.UserTypeServices
{
    public interface IUserTypeService
    {
        Task<ICollection<UserTypeViewModel>> GetAll();
        Task<UserTypeViewModel> GetById(Guid id);
        Task<bool> Insert(UserTypeInsertModel UserInsertModel);
        Task<bool> Update(UserTypeUpdateModel UserTypeUpdateModel);
        Task<bool> Delete(Guid id);
        Task<UserType> Find(Expression<Func<UserType, bool>> match);
    }
}
