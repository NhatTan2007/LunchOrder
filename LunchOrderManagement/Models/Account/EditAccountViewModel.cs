using LunchOrderManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Account
{
    public class EditAccountViewModel
    {
        private string _userId;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _email;
        private string _oldPassword;
        private string _newpassword;
        private string _confirmNewPasword;
        private string _phoneNumber;
        private string _classId;

        public string UserId { get => _userId; set => _userId = value; }
        public string FullName => $"{_firstName} {_lastName}";
        [Required(ErrorMessage = "First name is required")]
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
        [RegularExpression(@"(^[\w])+([\w._])*\@([\w{2,}\-])+(\.[\w]{2,4})$", ErrorMessage = "Email not valid")]
        public string Email { get => _email; set => _email = value; }
        [MaxLength(150)]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get => _oldPassword; set => _oldPassword = value; }
        [MaxLength(150)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get => _newpassword; set => _newpassword = value; }
        [MaxLength(150)]
        [Compare("NewPassword", ErrorMessage = "Confirm new password not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        public string ConfirmNewPasword { get => _confirmNewPasword; set => _confirmNewPasword = value; }
        [RegularExpression(@"(^0)+(3[2-9]|5[6|8|9]|7[0|6-9]|8(?!7|0)\d|9(?!5)[\d])+([0-9]{7})", ErrorMessage = "Phone number is valid")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        [Required(ErrorMessage = "Please select your class")]
        [Display(Name = "Your class")]
        public string ClassId { get => _classId; set => _classId = value; }
        public List<Class> Classes { get; set; }
        
    }
}
