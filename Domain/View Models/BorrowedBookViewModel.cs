using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain.Models;

namespace Domain.View_Models
{
    public class BorrowedBookViewModel
    {
        public Guid BorrowedBookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid BookID { get; set; }
        public Guid UserID { get; set; }

        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }

    public class BorrowedBookInsertModel
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

    }

    public class BorrowedBookUpdateModel : BorrowedBookInsertModel
    {
        [Required(ErrorMessage = "Id is neccessory for updation...!")]
        public Guid Id { get; set; }
    }

}
