using Domain.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : BaseEntityClass
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
        public Guid TypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Borrowed_Book> Borrowed_Books { get; set; }
    }
}
