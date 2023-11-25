using Domain.BaseEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserType : BaseEntityClass
    {
        [Required(ErrorMessage = "User Type is required...!")]
        [StringLength(10)]
        public string TypeName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
