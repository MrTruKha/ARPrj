using ARPrj.DataAccess.Common;
using ARPrj.DataAccess.Model;
using ARPrj.Model.Models.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;

namespace ARPrj.DataAccess.Repositoeies
{
    public interface IUserRepository : IBaseIdentityRepository<UserCommon>
    {
        bool AddUser(UserCommon entity, string roleName);
        void LockUser(string username, bool status);
        void UnLockUser(string username, bool status);
        IQueryable<UserModel> GetUserByName(string userName);
        bool CheckEmail(string email, string userName);
        bool UpdateRoleUser(string userName, string roleName);
        void UpdateUser(UserCommon entity);
        IQueryable<UserCommon> GetUsers();
        IQueryable<UserModel> GetUsersAndRole();
        bool IsLocked(string username);
        IQueryable<UserModel> GetUserById(string userId);
    }
    public class UserRepository : BaseIdentityRepository<UserCommon>, IUserRepository
    {
        private readonly UserManager<UserCommon> _userManager;
        private readonly DbSet<IdentityRole> _dbSetRole;
        private readonly DbSet<IdentityUserRole> _dbSetUserRole;
        public UserRepository(ARPrjContext context) : base(context)
        {
            _userManager = new UserManager<UserCommon>(new UserStore<UserCommon>(context));
            _dbSetRole = context.Set<IdentityRole>();
            _dbSetUserRole = context.Set<IdentityUserRole>();
        }
        public bool AddUser(UserCommon entity, string roleName)
        {
            try
            {
                _userManager.Create(entity, entity.PasswordHash);
                _userManager.AddToRole(entity.Id, roleName);
                return true;
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("AddUser", e.Message);
                return true;
            }
        }
        public IQueryable<UserModel> GetUsersAndRole()
        {
            try
            {
                var result = (from u in _userManager.Users
                              join ur in _dbSetUserRole on u.Id equals ur.UserId
                              join r in _dbSetRole on ur.RoleId equals r.Id
                              where !r.Name.Equals("Admin")
                              select new UserModel
                              {
                                  UserId = u.Id,
                                  UserName = u.UserName,                               
                                  Email = u.Email,
                                  RoleName = r.Name,
                                  IsActive = u.LockoutEnabled,
                              }).AsQueryable();
                return result;
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("GetUsersAndRole", e.Message);
                return null;
            }
        }

        public void LockUser(string username, bool status = true)
        {
            try
            {
                var old = _userManager.FindByName(username);
                old.Status = status;
                Update(old);
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("LockUser", e.Message);
                throw e;
            }
        }

        public void UnLockUser(string username, bool status = false)
        {
            try
            {
                var old = _userManager.FindByName(username);
                old.Status = status;
                Update(old);
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("UnLockUser", e.Message);
                throw e;
            }
        }

        public IQueryable<UserModel> GetUserByName(string userName)
        {
            try
            {
                var result = (from u in _userManager.Users
                              where u.UserName.Equals(userName)
                              join ur in _dbSetUserRole on u.Id equals ur.UserId
                              join r in _dbSetRole on ur.RoleId equals r.Id
                              where !r.Name.Equals("Admin")
                              select new UserModel
                              {
                                  UserId = u.Id,
                                  UserName = u.UserName,
                                  Email = u.Email,
                                  RoleName = r.Name,
                                  IsActive = u.LockoutEnabled,
                              }).AsQueryable();
                return result;
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("GetUserByName", e.Message);
                return null;
            }

        }

        public bool CheckEmail(string email, string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                try
                {
                    var result = _userManager.FindByEmail(email);
                    if (result != null)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    ARPrj.Common.Common.ExceptionLogger("CheckEmail", e.Message);
                    return false;
                }
            }
            else
            {
                try
                {
                    var result = Dbset.Any(x=>x.Email.Equals(email) && !x.UserName.Equals(userName));
                    return result;
                }
                catch (Exception e)
                {
                    ARPrj.Common.Common.ExceptionLogger("CheckEmail", e.Message);
                    return false;
                }
            }
        }

        public IQueryable<UserCommon> GetUsers()
        {
            var result = _userManager.Users.AsQueryable();
            return result;
        }

        public void UpdateUser(UserCommon entity)
        {
            try
            {
                var old = _userManager.FindByName(entity.UserName);
                if (!string.IsNullOrEmpty(entity.PasswordHash))
                {
                    old.Email = entity.Email;
                    old.FullName = entity.FullName;
                    old.PasswordHash = _userManager.PasswordHasher.HashPassword(entity.PasswordHash);
                    _userManager.Update(old);
                }
                else
                {
                    old.Email = entity.Email;
                    old.FullName = entity.FullName;
                    _userManager.Update(old);
                }
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("UpdateUser", e.Message);
                throw e;
            }
        }

        public bool UpdateRoleUser(string userName, string roleName)
        {
            try
            {
                var user = _userManager.FindByName(userName).Id;
                var role = _userManager.GetRoles(user).SingleOrDefault();
                _userManager.RemoveFromRole(user, role);
                _userManager.AddToRole(user, roleName);
                return true;
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("UpdatRoleUser", e.Message);
                return false;
            }
        }

        public bool IsLocked(string username)
        {
            var result = _userManager.Users.Any(x => x.UserName.Equals(username) && x.Status == false);
            return result;
        }

        public IQueryable<UserModel> GetUserById(string userId)
        {
            try
            {
                var result = (from u in _userManager.Users
                              where u.Id.Equals(userId)
                              join ur in _dbSetUserRole on u.Id equals ur.UserId
                              join r in _dbSetRole on ur.RoleId equals r.Id
                              select new UserModel
                              {
                                  UserId = u.Id,
                                  UserName = u.UserName,
                                  Email = u.Email,
                                  RoleName = r.Name,
                                  IsActive = u.LockoutEnabled,
                              }).AsQueryable();
                return result;
            }
            catch (Exception e)
            {
                ARPrj.Common.Common.ExceptionLogger("GetUserById", e.Message);
                return null;
            }
        }
    }
}
