using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DTOComment
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateEdit { get; set; }


        public int UserInfoId { get; set; }
        public virtual DTOUser UserInfo { get; set; }

        public int PostId { get; set; }
        public virtual DTOPost Post { get; set; }
        
      
    }
}
