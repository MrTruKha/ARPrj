using ARPrj.DataAccess.Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.DataAccess.Repositoeies
{
    public interface IRoleRepository : IBaseIdentityRepository<IdentityRole>
    {
        bool AddRole(IdentityRole entity);
        void DeleteRole(IdentityRole entity);
        void UpdateRole(IdentityRole entity);
        IQueryable<IdentityRole> GetRoles();
    }
    public class RoleRepository : BaseIdentityRepository<IdentityRole>, IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(ARPrjContext context):base(context)
        {

            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }
        public bool AddRole(IdentityRole entity)
        {
            _roleManager.Create(entity);
            return true;
        }

        public void DeleteRole(IdentityRole entity)
        {
            //Delete(entity);
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            var result = _roleManager.Roles;
            return result;
        }

        public void UpdateRole(IdentityRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
