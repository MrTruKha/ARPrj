using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Model.Models.User
{
    public class UsersRoleModel
    {
        public int UsersRolesId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Role.RoleModel Role { get; set; }
        public virtual UserModel User { get; set; }
    }
}
