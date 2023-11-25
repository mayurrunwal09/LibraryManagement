using Domain.Models;
using Domain.View_Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.UserTypeServices
{
    public class UserTypeService : IUserTypeService
    {
        #region Private Variables
        private readonly IRepository<UserType> _userType;

        public UserTypeService(IRepository<UserType> user)
        {
            _userType = user;
        }
        #endregion


        #region GetAll
        public async Task<ICollection<UserTypeViewModel>> GetAll()
        {
            ICollection<UserTypeViewModel> userViewModels = new List<UserTypeViewModel>();
            ICollection<UserType> userTypes = await _userType.GetAll();

            foreach (UserType userType in userTypes)
            {
                UserTypeViewModel viewModel = new()
                {
                    Id = userType.Id,
                    TypeName = userType.TypeName,                    
                };
                userViewModels.Add(viewModel);
            }
            return userViewModels;
        }
        #endregion

        #region GetById
        public async Task<UserTypeViewModel> GetById(Guid id)
        {
            var userType = await _userType.GetById(id);
            if (userType == null)
            {
                return null;
            }
            else
            {
                UserTypeViewModel viewModel = new()
                {
                    Id = userType.Id,
                    TypeName = userType.TypeName,
                };
                
                return viewModel;
            }
        }
        #endregion

        #region Insert
        public Task<bool> Insert(UserTypeInsertModel userTypeInsertModel)
        {
            UserType user = new()
            {                            
                TypeName = userTypeInsertModel.TypeName,
            };
            return _userType.Insert(user);

        }
        #endregion

        #region Update
        public async Task<bool> Update(UserTypeUpdateModel UserTypeUpdateModel)
        {
            var user = await _userType.GetById(UserTypeUpdateModel.Id);
            if (user != null)
            {
                user.TypeName = UserTypeUpdateModel.TypeName;

                var result = await _userType.Update(user);
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
                UserType user = await _userType.GetById(id);
                if (user != null)
                {
                    _ = await _userType.Delete(user);
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
        public Task<UserType> Find(Expression<Func<UserType, bool>> match)
        {
            return _userType.Find(match);
        }
        #endregion
    }
}
