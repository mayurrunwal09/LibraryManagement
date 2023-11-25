using Domain.Models;
using Domain.View_Models;
using Infrastructure.Common;
using Infrastructure.Repositories;
using Infrastructure.Services.Custom.UserTypeServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Custom.UserServices
{
    public class UserService : IUserService
    {
        #region Private Variables
        private readonly IRepository<User> _user;
        private readonly IUserTypeService _userType;

        public UserService(IRepository<User> user, IUserTypeService userType)
        {
            _user = user;
            _userType = userType;
        }
        #endregion

        #region GetAllLibrarian
        public async Task<ICollection<UserViewModel>> GetAllLibrarian()
        {
            var userTypeli = await _userType.Find(x => x.TypeName == "librarian");
           

            ICollection<UserViewModel> userViewModels = new List<UserViewModel>();
            ICollection<User> users = await _user.FindAll(x => x.TypeId == userTypeli.Id);

            foreach(User user in users)
            {
                UserViewModel viewModel = new()
                { 
                    Id = user.Id,
                    UserID = user.UserID,
                    Username = user.Username,
                    Password = Encryptor.DecryptString(user.Password),
                };

                UserTypeViewModel userTypeViewModel = new();
                if(userTypeli != null)
                {
                    userTypeViewModel.Id = userTypeli.Id;
                    userTypeViewModel.TypeName = userTypeli.TypeName;

                    viewModel.UserType.Add(userTypeViewModel);
                }
                userViewModels.Add(viewModel);
            }
            if (users == null)
                return null;
            return userViewModels;
        }
        #endregion

        #region GetAllLibrarian
        public async Task<ICollection<UserViewModel>> GetAllMember()
        {
            var userTypeli = await _userType.Find(x => x.TypeName == "member");


            ICollection<UserViewModel> userViewModels = new List<UserViewModel>();
            ICollection<User> users = await _user.FindAll(x => x.TypeId == userTypeli.Id);

            foreach (User user in users)
            {
                UserViewModel viewModel = new()
                {
                    Id = user.Id,
                    UserID = user.UserID,
                    Username = user.Username,
                    Password = Encryptor.DecryptString(user.Password),
                };

                UserTypeViewModel userTypeViewModel = new();
                if (userTypeli != null)
                {
                    userTypeViewModel.Id = userTypeli.Id;
                    userTypeViewModel.TypeName = userTypeli.TypeName;

                    viewModel.UserType.Add(userTypeViewModel);
                }
                userViewModels.Add(viewModel);
            }
            if (users == null)
                return null;
            return userViewModels;
        }
        #endregion

        #region GetlibrarianById
        public async Task<UserViewModel> GetLibrarianById(Guid id)
        {
            var user = await _user.GetById(id);
            var userType = await _userType.Find(x => x.TypeName == "librarian");

            if (user == null)
            {
                return null;
            }
            else
            {
                if (user.TypeId == userType.Id)
                {
                    UserViewModel viewModel = new()
                    {
                        Id = user.Id,
                        UserID = user.UserID,
                        Username = user.Username,
                        Password = Encryptor.DecryptString(user.Password),
                    };
                    UserTypeViewModel userTypeViewModel = new();
                    if (userType != null)
                    {
                        userTypeViewModel.Id = userType.Id;
                        userTypeViewModel.TypeName = userType.TypeName;

                        viewModel.UserType.Add(userTypeViewModel);
                    }

                    return viewModel;
                }
                else
                    return null;
                
            }
        }
        #endregion

        #region GetMemberById
        public async Task<UserViewModel> GetMemberById(Guid id)
        {
            var user = await _user.GetById(id);
            var userType = await _userType.Find(x => x.TypeName == "member");

            if (user == null)
            {
                return null;
            }
            else
            {
                if (user.TypeId == userType.Id)
                {
                    UserViewModel viewModel = new()
                    {
                        Id = user.Id,
                        UserID = user.UserID,
                        Username = user.Username,
                        Password = Encryptor.DecryptString(user.Password),
                    };
                    UserTypeViewModel userTypeViewModel = new();
                    if (userType != null)
                    {
                        userTypeViewModel.Id = userType.Id;
                        userTypeViewModel.TypeName = userType.TypeName;

                        viewModel.UserType.Add(userTypeViewModel);
                    }

                    return viewModel;
                }
                else
                    return null;

            }
        }
        #endregion

        #region GetByName
        public async Task<UserViewModel> GetByName(string name)
        {
            var user = await _user.GetByName(name);
            if (user == null)
            {
                return null;
            }
            else
            {
                UserViewModel viewModel = new()
                {
                    Id = user.Id,
                    UserID = user.UserID,
                    Username = user.Username,
                };
                return viewModel;
            }
        }
        #endregion

        #region GetLast
        public User GetLast()
        {
            return _user.GetLast();
        }
        #endregion

        #region InsertLibrarian
        public async Task<bool> InsertLibrarian(UserInsertModel userInsertModel)
        {
            var userType = await _userType.Find(x => x.TypeName == "librarian");
              
            if(userType != null)
            {
                User user = new()
                {
                    UserID = userInsertModel.UserID,
                    Username = userInsertModel.Username,
                    TypeId = userType.Id,
                    Password = Encryptor.EncryptString(userInsertModel.Password),
                };
                return await _user.Insert(user);
            }
            else
                return false;
           
        }
        #endregion

        #region InsertMember
        public async Task<bool> InsertMember(UserInsertModel userInsertModel)
        {
            var userType = await _userType.Find(x => x.TypeName == "member");

            if (userType != null)
            {
                User user = new()
                {
                    UserID = userInsertModel.UserID,
                    Username = userInsertModel.Username,
                    TypeId = userType.Id,
                    Password = Encryptor.EncryptString(userInsertModel.Password),
                };
                return await _user.Insert(user);
            }
            else
                return false;

        }
        #endregion

        #region Update
        public async Task<bool> Update(UserUpdateModel userUpdateModel)
        {
            var user = await _user.GetById(userUpdateModel.Id);
            if(user != null)
            {
                user.UserID = userUpdateModel.UserID;
                user.Username = userUpdateModel.Username;
                user.Password = Encryptor.EncryptString(userUpdateModel.Password);

                var result = await _user.Update(user);
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
                User user = await _user.GetById(id);
                if (user != null)
                {
                    _ = await _user.Delete(user);
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
        public Task<User> Find(Expression<Func<User, bool>> match)
        {
            return _user.Find(match);
        }
        #endregion


    }
}
