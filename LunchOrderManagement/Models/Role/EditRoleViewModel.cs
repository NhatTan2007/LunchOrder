using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Role
{
    public class EditRoleViewModel
    {
        private string _roleId;
        private string _roleName;

        public string RoleId { get => _roleId; set => _roleId = value; }
        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "Role name")]
        public string RoleName { get => _roleName; set => _roleName = value; }
    }
}
