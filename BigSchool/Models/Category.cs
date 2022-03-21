using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchool.Models
{
    public class Category
    {
        public byte Id { get; set; }
        [Required]      //Hiển thị thông báo nếu không nhập dữ liệu
        [StringLength(255)]
        public string Name { get; set; }
    }
}