using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Comment")]
    public class Comment : MyEntity
    {
        [MaxLength(255)]
        public string CommentBody { get; set; }
        [Required]
        public DateTime? DateCreation { get; set; }
        public DateTime? DateEdit { get; set; }


        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }
        public int UserInfoId { get; set; }
       

        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
       
    }

}
