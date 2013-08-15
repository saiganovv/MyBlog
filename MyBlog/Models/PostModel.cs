using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime CurrentDate { get; set; }

        public bool Published { get; set; }

        public virtual List<CommentModel> PostComments { get; set; }

        public virtual UserProfile User { get; set; }
    }
}