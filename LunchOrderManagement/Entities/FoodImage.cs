using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LunchOrderManagement.Entities
{
    public class FoodImage
    {
        private string _imagesId;
        private string _fileName;
        [Key]
        public string ImagesId { get => _imagesId; set => _imagesId = value; }
        [Required]
        public string FileName { get => _fileName; set => _fileName = value; }
        [ForeignKey("Food")]
        public string FoodId { get; set; }
        public Food Food { get; set; }
    }
}
