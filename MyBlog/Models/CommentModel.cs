using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class CommentModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string UserName { get; set; }

        public virtual PostModel Post { get; set; }
    }
}