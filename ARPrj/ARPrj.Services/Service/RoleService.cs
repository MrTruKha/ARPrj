using ARPrj.DataAccess.Repositoeies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Service
{
    public interface IRoleService
    {
        bool AddRole(IdentityRole entity);
        List<IdentityRole> GetRoles();
    }
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public bool AddRole(IdentityRole entity)
        {
            var result = _roleRepository.AddRole(entity);
            return true;
        }

        public List<IdentityRole> GetRoles()
        {
            var result = _roleRepository.GetRoles().ToList();
            return result;
        }
    }
}
