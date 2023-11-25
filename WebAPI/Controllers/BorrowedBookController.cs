using Domain.Models;
using Domain.View_Models;
using Infrastructure.Services.Custom.BookServices;
using Infrastructure.Services.Custom.BorrowedBookServices;
using Infrastructure.Services.Custom.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BorrowedBookController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly IBorrowedBookService _borrowedBookService;
        private readonly IUserService _userService;
        private readonly IBookService _bookService;
        private readonly ILogger<BorrowedBookController> _logger;
        

        public BorrowedBookController(IBorrowedBookService borrowedBookService, IUserService userService, IBookService bookService, ILogger<BorrowedBookController> logger)
        {
            _borrowedBookService = borrowedBookService;
            _userService = userService;
            _bookService = bookService;
            _logger = logger;
        }
        #endregion

        #region GetAll
        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<BorrowedBookViewModel>> GetAll()
        {
            _logger.LogInformation("Getting All Data ");
            var result = await _borrowedBookService.GetAll();

            if (result == null)
            {
                _logger.LogWarning("Borrowed Book data was Not Found");
                return BadRequest("Borrowed Book data was Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region GetById
        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<BorrowedBookViewModel>> GetById(Guid id)
        {
            _logger.LogInformation("Getting Data By Id");
            var result = await _borrowedBookService.GetById(id);

            if (result == null)
            {
                _logger.LogWarning("Borrowed Book data was Not Found");
                return BadRequest("Borrowed Book data was Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region Insert
        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert([FromForm]BorrowedBookInsertModel BorrowedBookInsertModel)
        {
            if(ModelState.IsValid)
            {
                User user = await _userService.Find(x => x.Id == BorrowedBookInsertModel.UserID);
                if(user != null)
                {
                    Book book = await _bookService.Find(x => x.Id == BorrowedBookInsertModel.BookID);
                    if(book != null)
                    {
                        _logger.LogInformation("Inserting Details ....!");
                        var result = await _borrowedBookService.Insert(BorrowedBookInsertModel);
                        if (result == true)
                        {
                            _logger.LogInformation("Data Inserted Successfuly......!");
                            return Ok("Data Inserted Successfuly......!");
                        }
                        else
                            _logger.LogWarning("Something Went Wrong ....!");
                            return BadRequest("Something Went Wrong ....!");
                    }
                    else
                        _logger.LogWarning("Book ID Is Not Found ....!");
                        return BadRequest("Book ID Is Not Found ....!");
                }
                else
                    _logger.LogWarning("User ID Is Not Found ....!");
                     return BadRequest("User ID Is Not Found ....!");
            }
            else
                _logger.LogWarning("Model State Is Not Valid ....!");
                return BadRequest("Model State Is Not Valid ....!");
        }
        #endregion

        #region Update
        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromForm]BorrowedBookUpdateModel BorrowedBookUpdateModel)
        {
            if(ModelState.IsValid)
            {
                _logger.LogInformation("Updating Data ....!");
                var result = await _borrowedBookService.Update(BorrowedBookUpdateModel);
                if (result == true)
                {
                    _logger.LogInformation("Data Updated Successfully ...!");
                    return Ok("Data Updated Successfully ...!");
                }
                else
                    _logger.LogWarning("Something Went Wrong ....!");
                    return BadRequest("Something Went Wrong ....!");
            }
            else
                _logger.LogWarning("Model State Is Not Valid ....!");
                 return BadRequest("Model State Is Not Valid ....!");
        }
        #endregion

        #region Delete
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id != Guid.Empty)
            {
                _logger.LogInformation("Deleting The Data .....!");
                var result = await _borrowedBookService.Delete(id);
                if (result == true)
                {
                    _logger.LogInformation("Data Deleted Successfully ...!");
                    return Ok("Data Deleted Successfully ...!");
                }
                else
                    _logger.LogWarning("Something Went Wrong ....!");
                    return BadRequest("Something Went Wrong ....!");
            }
            else
                _logger.LogWarning("Model State Is Not Valid ....!");
                return BadRequest("Model State Is Not Valid ....!");
        }
        #endregion
    }
}
