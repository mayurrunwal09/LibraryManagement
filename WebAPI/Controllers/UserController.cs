using Domain.Models;
using Domain.View_Models;
using Infrastructure.Context;
using Infrastructure.Services.Custom.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        private readonly MainDbContext _context;

        public UserController(IUserService userService, ILogger<UserController> logger, MainDbContext context)
        {
            _userService = userService;
            _logger = logger;
            _context = context;
        }
        #endregion

        #region GetAllLibrarian
        [HttpGet(nameof(GetAllLibrarian))]
        public async Task<ActionResult<UserViewModel>> GetAllLibrarian()
        {
            _logger.LogInformation("Getting All The Records ..... !");
            var result = await _userService.GetAllLibrarian();
            if(result == null)
            {
                _logger.LogWarning("User Records Are Not Found");
                return BadRequest("User Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region GetAllMember
        [HttpGet(nameof(GetAllMember))]
        public async Task<ActionResult<UserViewModel>> GetAllMember()
        {
            _logger.LogInformation("Getting All The Records ..... !");
            var result = await _userService.GetAllMember();
            if (result == null)
            {
                _logger.LogWarning("User Records Are Not Found");
                return BadRequest("User Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region GetLibrarianById
        [HttpGet(nameof(GetLibrarianById))]
        public async Task<ActionResult<UserViewModel>> GetLibrarianById(Guid id)
        {
            _logger.LogInformation("Getting All The Records By Id ..... !");
            var result = await _userService.GetLibrarianById(id);
            if (result == null)
            {
                _logger.LogWarning("User Records Are Not Found");
                return BadRequest("User Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region GetMemberById
        [HttpGet(nameof(GetMemberById))]
        public async Task<ActionResult<UserViewModel>> GetMemberById(Guid id)
        {
            _logger.LogInformation("Getting All The Records By Id ..... !");
            var result = await _userService.GetMemberById(id);
            if (result == null)
            {
                _logger.LogWarning("User Records Are Not Found");
                return BadRequest("User Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        /* #region Insert
         [HttpPost(nameof(Insert))]
         public async Task<IActionResult> Insert([FromForm] UserInsertModel UserInsertModel)
         {
             if(ModelState.IsValid)
             {
                 _logger.LogInformation("Inserting Record ...... !");
                 var result = await _userService.Insert(UserInsertModel);
                 if(result == true)
                 {
                     _logger.LogInformation("Data Inserted Successfully .....!");
                     return Ok("Data Inserted Successfully .....!");
                 }
                 else
                 {
                     _logger.LogWarning("Something Went Wrong .... !");
                     return BadRequest("Something Went Wrong .... !");
                 }
             }
             else
             {
                 return BadRequest("Model State Is Not Valid ..... !");
             }
         }
         #endregion*/

        #region Update
        [HttpPut(nameof(Update))]       
        public async Task<IActionResult> Update([FromForm] UserUpdateModel UserUpdateModel)
        {
            if (ModelState.IsValid)
            {
                var CheckUser = await _userService.Find(x => x.UserID == UserUpdateModel.UserID && x.Id != UserUpdateModel.Id);

                if (CheckUser != null)
                {
                    return BadRequest("User ID : " + UserUpdateModel.UserID + " already Exist...!");
                }
                else
                {
                    var CheckUsername = await _userService.Find(x => x.Username == UserUpdateModel.Username && x.Id != UserUpdateModel.Id);
                    if (CheckUsername != null)
                    {
                        return BadRequest(" UserName :" + UserUpdateModel.Username + " already Exist...!");
                    }
                    else
                    {
                        var result = await _userService.Update(UserUpdateModel);
                        if (result == true)
                            return Ok("Supplier Updated Successfully...!");
                        else
                            return BadRequest("Something Went Wrong, Supplier Is Not Updated, Please Try After Sometime...!");
                    }
                }
               
            }
            else
                return NotFound("Supplier Not Found with id :" + UserUpdateModel.Id + ", Please Try Again After Sometime...!");
        }
    
        #endregion

        #region Delete
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id != Guid.Empty)
            {
                _logger.LogInformation("Deleting Record ...... !");
                var result = await _userService.Delete(id);
                if (result == true)
                {
                    _logger.LogInformation("Data Deleted Successfully .....!");
                    return Ok("Data Deleted Successfully .....!");
                }
                else
                {
                    _logger.LogWarning("Something Went Wrong .... !");
                    return BadRequest("Something Went Wrong .... !");
                }
            }
            else
            {
                return BadRequest("Model State Is Not Valid ..... !");
            }
        }
        #endregion

        #region GetBorrowedDateByUserId
        [HttpGet(nameof(GetBorrowedDateByUserId))]
        public async Task<IActionResult> GetBorrowedDateByUserId(Guid id)
        {
            var date = await _context.Borrowed_Books.Where(x => x.UserID == id).Select(e => e.BorrowDate).ToListAsync();
            if (date == null)
            {
                return BadRequest("Something Went Wrong...!");
            }

            return Ok(date);
        }
        #endregion

        #region GetBorrowedBooksByUserId
        [HttpGet(nameof(GetBorrowedBooksByUserId))]
        public IActionResult GetBorrowedBooksByUserId(Guid id)
        {
            var borrowedBooks = _context.Borrowed_Books.Where(b => b.UserID == id).Select(e => e.Books).ToList();
            return Ok(borrowedBooks);


            /*var borrowedBooks = _context.Borrowed_Books.Where(b => b.UserID == id);
            if (borrowedBooks == null)
            {
                return NotFound();
            }
            var */

            /* var borrowedBooks = _context.Borrowed_Books.FirstOrDefault(b => b.UserID == id);
             if (borrowedBooks == null)
             {
                 return NotFound();
             }
             var books = _context.Books.Where(x => x.Id == borrowedBooks.BookID).ToList();
             return Ok(books);*/

            /* var borrowed = _context.Borrowed_Books.Where(e => e.UserID == id);
             if (borrowed == null)
             {
                 return NotFound();
             }
             var books = _context.Books.FirstOrDefault(s => s.Id == borrowed.BookID);
             if (books == null)
             {
                 return NotFound();
             }
             return Ok(books);*/
        }
        #endregion

    }
}
