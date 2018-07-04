using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SyntacsApp.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [StringLength(255), Required]
        public string CommentBody { get; set; }
        public string Alias { get; set; }
        public int UserID { get; set; }
        public int UpVote { get; set; }
        public int ErrExampleID { get; set; }
    }
}
