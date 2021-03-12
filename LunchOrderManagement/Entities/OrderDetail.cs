using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class OrderDetail
    {
        private string _oderDetailId;
        private string _dateOrder;

        [Key]
        public string OderDetailId { get => _oderDetailId; set => _oderDetailId = value; }
        [Required]
        [ForeignKey("AppIdentityUser")]
        public string UserId { get; set; }
        public AppIdentityUser User { get; set; }
        [Required]
        [ForeignKey("Food")]
        public string FoodId { get; set; }
        public Food Food { get; set; }
        [Required]
        public string DateOrder { get => _dateOrder; set => _dateOrder = value; }

    }
}
