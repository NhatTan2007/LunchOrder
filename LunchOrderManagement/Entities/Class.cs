using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class Class
    {
        private string _classId;
        private string _className;
        [Key]
        public string ClassId { get => _classId; set => _classId = value; }
        public string ClassName { get => _className; set => _className = value; }
        public ICollection<AppIdentityUser> User { get; set; }
    }
}
