using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    [Table("Post")]
    public class Post : MyEntity
    {
        [MaxLength(10000)]   
        public string PostBody { get; set; }
        [MaxLength(1000)]
        public string PostTitle { get; set; }
        public string PostPicture { get; set; }
        [Required]
        public DateTime? DateCreate { get; set; }
        public DateTime? LastEdit { get; set; }
        public string Hashtags { get; set; }
        public bool IsBlocked { get; set; }


        [ForeignKey("UserInfoId")]
        public virtual UserInfo UserInfo { get; set; }
        public int UserInfoId { get; set; }
        

        public virtual ICollection<Comment> Comments { get; set; }
        
       
    }
}
