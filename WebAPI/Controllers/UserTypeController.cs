using Domain.Models;
using Domain.View_Models;
using Infrastructure.Context;
using Infrastructure.Services.Custom.UserServices;
using Infrastructure.Services.Custom.UserTypeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        #region Private Variables and Constructor
        private readonly IUserTypeService _userTypeService;
        private readonly ILogger<UserTypeController> _logger;

       

        public UserTypeController(IUserTypeService userService, ILogger<UserTypeController> logger)
        {
            _userTypeService = userService;
            _logger = logger;
            ;
        }
        #endregion

        #region GetAll
        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<UserTypeViewModel>> GetAll()
        {
            _logger.LogInformation("Getting All The Records ..... !");
            var result = await _userTypeService.GetAll();
            if (result == null)
            {
                _logger.LogWarning("UserType Records Are Not Found");
                return BadRequest("UserType Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region GetById
        [HttpGet(nameof(GetById))]
        public async Task<ActionResult<UserTypeViewModel>> GetById(Guid id)
        {
            _logger.LogInformation("Getting All The Records By Id ..... !");
            var result = await _userTypeService.GetById(id);
            if (result == null)
            {
                _logger.LogWarning("UserType Records Are Not Found");
                return BadRequest("UserType Records Are Not Found");
            }
            return Ok(result);
        }
        #endregion

        #region Insert
        [HttpPost(nameof(Insert))]
        public async Task<IActionResult> Insert([FromForm] UserTypeInsertModel UserTypeInsertModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Inserting Record ...... !");
                var result = await _userTypeService.Insert(UserTypeInsertModel);
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
        public async Task<IActionResult> Update([FromForm] UserTypeUpdateModel UserTypeUpdateModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating Record ...... !");
                var result = await _userTypeService.Update(UserTypeUpdateModel);
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
                var result = await _userTypeService.Delete(id);
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
    }
}
