using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class AppIdentityUser : IdentityUser
    {
        private string _firstName;
        private string _middleName;
        private string _lastName;

        [Required]
        [Display(Name = "First name")]
        [MaxLength(20)]
        public string FirstName { get => _firstName; set => _firstName = value; }
        [Display(Name = "Middle name")]
        [MaxLength(40)]
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Required]
        [Display(Name = "Last name")]
        [MaxLength(80)]
        public string LastName { get => _lastName; set => _lastName = value; }
        [Required]
        [ForeignKey("Class")]
        public string ClassId { get; set; }
        public Class Class { get; set; }
        public ICollection<OrderDetail> OderDetails { get; set; }
    }
}
