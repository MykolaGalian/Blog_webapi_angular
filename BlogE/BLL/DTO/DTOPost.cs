using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DTOPost
    {
        public int Id { get; set; }
        public string PostBody { get; set; }

        public string PostTitle { get; set; }
        public string PostPicture { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? LastEdit { get; set; }
        public string Hashtags { get; set; }
        public bool IsBlocked { get; set; }

        public int UserInfoId { get; set; }
        public virtual DTOUser UserInfo { get; set; }

        public virtual ICollection<DTOComment> Comments { get; set; }
        public DTOPost()
        {
            Comments = new List<DTOComment>();
        }
    }
}
