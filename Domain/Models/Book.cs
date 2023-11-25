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
    public class Book : BaseEntityClass
    {
        [Required(ErrorMessage = "Please Enter BookID...!")]
        [RegularExpression(@"(?:\s|^)#[A-Za-z0-9]+(?:\s|$)", ErrorMessage = "BookID start with # and Only Number and character are allowed eg(#User1001)")]
        [StringLength(10)]
        public string BookID { get; set; }

        [Required(ErrorMessage = "Enter Your Title Name ...!")]
        [Display(Name = "Title Name")]
        [Column(TypeName = "Varchar(50)")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter Your ISBN Number ...!")]
        [Display(Name = "ISBN Number")]
        [Column(TypeName = "Varchar(50)")]
        public string ISBN { get; set; }

        public virtual ICollection<Borrowed_Book> Borrowed_Books { get; set; }
    }
}
