using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Models.Role
{
    public class RoleViewModel
    {
        private string _roleId;
        private string _roleName;

        public string RoleId { get => _roleId; set => _roleId = value; }
        public string RoleName { get => _roleName; set => _roleName = value; }
    }
}
