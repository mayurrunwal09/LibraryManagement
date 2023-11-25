using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.View_Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }     
        public string Username { get; set; }        
        public string Password { get; set; }
        public List<UserTypeViewModel> UserType { get; set; } = new List<UserTypeViewModel>();
    }

    public class UserInsertModel
    {
        [Required(ErrorMessage = "Please Enter User Id...!")]
        [RegularExpression(@"(?:\s|^)#[A-Za-z0-9]+(?:\s|$)", ErrorMessage = "User ID start with # and Only Number and character are allowed eg(#User1001)")]
        [StringLength(10)]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Enter Your User Name ...!")]
        [Display(Name = "User Name")]
        [Column(TypeName = "Varchar(50)")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }

    public class UserUpdateModel : UserInsertModel
    {
        [Required(ErrorMessage = "Id is neccessory for updation...!")]
        public Guid Id { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Please Enter UserName...!")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }

}
