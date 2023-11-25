using Domain.BaseEntity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models
{
    public class Borrowed_Book : BaseEntityClass
    {
        [Required(ErrorMessage = "Enter Your Book BorrowDate ...!")]
        [Display(Name = "Book BorrowDate")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = ("Please Enter Valid Date Format {dd/mm/yyyy}...!"))]
        public DateTime BorrowDate { get; set; }

        [Required(ErrorMessage = "Enter Your Book ReturnDate ...!")]
        [Display(Name = "Book ReturnDate")]
        [Column(TypeName = "date")]
        [DataType(DataType.Date, ErrorMessage = ("Please Enter Valid Date Format {dd/mm/yyyy}...!"))]
        public DateTime ReturnDate { get; set; }


        public Guid BookID { get; set; }
        public Guid UserID { get; set; }
       
        public virtual User User { get; set; }
        public virtual Book Books { get; set;}
       
    }
}
