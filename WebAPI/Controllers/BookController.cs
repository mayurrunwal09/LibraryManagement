using Domain.View_Models;
using Infrastructure.Context;
using Infrastructure.Services.Custom.BookServices;
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
    public class BookController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly MainDbContext _context;

        public BookController(IBookService userService, ILogger<BookController> logger, MainDbContext context)
        {
            _bookService = userService;
            _logger = logger;
            _context = context;
        }
        #endregion

        #region GetAll
        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<BookViewModel>> GetAll()
        {
            _logger.LogInformation("Getting All The Records ..... !");
            var result = await _bookService.GetAll();
            if (result == null)
            {
                _logger.LogWarning("Book Records Are Not Found");
                return BadRequest("Book Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region GetById
        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<BookViewModel>> GetById(Guid id)
        {
            _logger.LogInformation("Getting All The Records By Id ..... !");
            var result = await _bookService.GetById(id);
            if (result == null)
            {
                _logger.LogWarning("Book Records Are Not Found");
                return BadRequest("Book Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region Insert
        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert([FromForm] BookInsertModel BookInsertModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Inserting Record ...... !");
                var result = await _bookService.Insert(BookInsertModel);
                if (result == true)
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
        #endregion

        #region Update
        [HttpPut(nameof(Update))]
        public async Task<IActionResult> Update([FromForm] BookUpdateModel BookUpdateModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Record ...... !");
                var result = await _bookService.Update(BookUpdateModel);
                if (result == true)
                {
                    _logger.LogInformation("Data Updated Successfully .....!");
                    return Ok("Data Updated Successfully .....!");
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

        #region Delete
        [HttpDelete(nameof(Delete))]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id != Guid.Empty)
            {
                _logger.LogInformation("Deleting Record ...... !");
                var result = await _bookService.Delete(id);
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

        #region GetBorrowedBookByTitle
        [HttpGet(nameof(GetBorrowedBookByTitle))]
        public IActionResult GetBorrowedBookByTitle(string title)
        {
            var borrowedBooks = _context.Borrowed_Books.Where(b => b.Books.Title.ToLower() == title.ToLower()).ToList();
            return Ok(borrowedBooks);


            /* var user = await _context.Borrowed_Books.Include(b => b.Books).Where(b => b.Books.Title == name).ToListAsync();
             if (user == null)
             {
                 return BadRequest("Something Went Wrong...!");
             }
             return Ok(user);*/



            /* var book =  _context.Books.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());
             if (book == null)
             {
                 return BadRequest("Something Went Wrong...!");
             }

             var borrowed = _context.Borrowed_Books.Where(e => e.BookID == book.Id).ToList();
             return Ok(borrowed);*/
        }
        #endregion
    }
}
