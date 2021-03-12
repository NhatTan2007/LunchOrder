using LunchOrderManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Account
{
    public class RegisterAccountViewModel
    {
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _confirmPasword;
        private string _phoneNumber;
        private string _classId;
        private bool _isAgreeTerm;

        public string FullName => $"{_firstName} {_lastName}";
        [Required(ErrorMessage ="First name is required")]
        [Display(Name = "First name")]
        [MaxLength(20)]
        public string FirstName { get => _firstName; set => _firstName = value; }
        [MaxLength(40)]
        public string MiddleName { get => _middleName; set => _middleName = value; }
        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last name")]
        [MaxLength(40)]
        public string LastName { get => _lastName; set => _lastName = value; }
        [MaxLength(150)]
        [Required(ErrorMessage = "Email is required")]
        [Remote("EmailInUse", "Account", ErrorMessage = "Email is used by another account")]
        [RegularExpression(@"(^[\w])+([\w._])*\@([\w{2,}\-])+(\.[\w]{2,4})$", ErrorMessage = "Email not valid")]
        public string Email { get => _email; set => _email = value; }
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(150)]
        [DataType(DataType.Password)]
        public string Password { get => _password; set => _password = value; }
        [Required(ErrorMessage = "Confirm password is required")]
        [MaxLength(150)]
        [Compare("Password", ErrorMessage = "Confirm password not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPasword { get => _confirmPasword; set => _confirmPasword = value; }
        [Required(ErrorMessage = "Please agree with our term before continue")]
        public bool IsAgreeTerm { get => _isAgreeTerm; set => _isAgreeTerm = value; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"(^0)+(3[2-9]|5[6|8|9]|7[0|6-9]|8(?!7|0)\d|9(?!5)[\d])+([0-9]{7})", ErrorMessage = "Phone number is valid")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        [Required(ErrorMessage ="Please select your class")]
        [Display(Name ="Your class")]
        public string ClassId { get => _classId; set => _classId = value; }
        public List<Class> Classes { get; set; }

    }
}
