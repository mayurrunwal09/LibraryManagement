
using Domain.HelperClass;
using Domain.Models;
using Domain.View_Models;
using Infrastructure.Common;
using Infrastructure.Services.Custom.UserServices;
using Infrastructure.Services.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Net;
using WebAPI.Middleware.Auth;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Private Variable And Controller
        private readonly ILogger _logger;
        private readonly IUserService _userservice;
        private readonly IJWTAuthManager _authManager;
        private readonly IWebHostEnvironment _environment;
        private readonly IService<UserType> _serviceUserType;

        public LoginController(ILogger<LoginController> logger,IUserService user, IJWTAuthManager authManager, IWebHostEnvironment environment,IService<UserType> serviceUserType)
        {
            _logger = logger;
            _userservice = user;
            _authManager = authManager;
            _environment = environment;
            _serviceUserType = serviceUserType;      
        }
        #endregion

        #region Login And Registration section

        [HttpPost(nameof(UserLogin))]
        [AllowAnonymous]
        public async Task<IActionResult> UserLogin(LoginModel loginModel)
        {
            Response<string> response = new();
            if(ModelState.IsValid)
            {
                var user = await _userservice.Find(x => x.Username == loginModel.UserName && x.Password == Encryptor.EncryptString(loginModel.Password));
                if (user == null)
                {
                    response.Message = "Invalid User / Passwaord , Please Provide Valid User and Password";
                    response.Status = (int)HttpStatusCode.NotFound;
                    return NotFound(response);
                }
                response.Message = _authManager.GenerateJWT(user);
                response.Status = (int)HttpStatusCode.OK;
                return Ok(response);
            }
            else
            {
                response.Message = "Invalid Login Information, Please Enter Valid Credentials...!";
                response.Status = (int)HttpStatusCode.NotAcceptable;
                return BadRequest(response);
            }
        }
        #endregion

        #region RegisterLibrarian
        [HttpPost(nameof(RegisterLibrarian))]
        public async Task<IActionResult> RegisterLibrarian([FromForm]UserInsertModel UserInsertModel)
        {
            if(ModelState.IsValid)
            {
                var userType = await _serviceUserType.Find(x => x.TypeName.ToLower().Trim() == "librarian");
                if(userType != null)
                {
                    var userId = await _userservice.Find(x => x.UserID == UserInsertModel.UserID);
                    if(userId != null)
                    {
                        return BadRequest("User Id Already Exist .... !");
                    }
                    else
                    {
                        var userName = await _userservice.Find(x => x.Username == UserInsertModel.Username);
                        if (userName != null)
                        {
                            return BadRequest("User Name Already Exist ...!");
                        }
                        else
                        {
                            var result = await _userservice.InsertLibrarian(UserInsertModel);
                            if (result == true)
                            {
                                return Ok("User Register Successfully .... !");
                            }
                            else
                            {
                                return BadRequest("Something Went Wrong ... !");
                            }
                        }
                    }
                }
                else
                {
                    return BadRequest("UserType is not exist ...!");
                }
            }
            else
            {
                return BadRequest("Model State Is not valid ... !");
            }

        }
        #endregion

        #region RegisterMember
        [HttpPost(nameof(RegisterMember))]
        public async Task<IActionResult> RegisterMember([FromForm] UserInsertModel UserInsertModel)
        {
            if (ModelState.IsValid)
            {
                var userType = await _serviceUserType.Find(x => x.TypeName.ToLower().Trim() == "member");
                if (userType != null)
                {
                    var userId = await _userservice.Find(x => x.UserID == UserInsertModel.UserID);
                    if (userId != null)
                    {
                        return BadRequest("User Id Already Exist .... !");
                    }
                    else
                    {
                        var userName = await _userservice.Find(x => x.Username == UserInsertModel.Username);
                        if (userName != null)
                        {
                            return BadRequest("User Name Already Exist ...!");
                        }
                        else
                        {
                            var result = await _userservice.InsertMember(UserInsertModel);
                            if (result == true)
                            {
                                return Ok("User Register Successfully .... !");
                            }
                            else
                            {
                                return BadRequest("Something Went Wrong ... !");
                            }
                        }
                    }
                }
                else
                {
                    return BadRequest("UserType is not exist ...!");
                }
            }
            else
            {
                return BadRequest("Model State Is not valid ... !");
            }

        }
        #endregion


    }
}
