using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class DTOUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } //for Identity
        public string Password { get; set; } //for Identity
        public string Login { get; set; }
        public string UserAvatar { get; set; }
        public string Address { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime? DateRegistration { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public List<string> Roles { get; set; }



    }
}
