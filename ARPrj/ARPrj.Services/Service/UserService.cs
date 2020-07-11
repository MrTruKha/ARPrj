using ARPrj.DataAccess.Model;
using ARPrj.DataAccess.Repositoeies;
using ARPrj.Model.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPrj.Service
{
    public interface IUserService
    {
        bool AddUser(UserCommon entity, string roleName);
        void LockUser(UserCommon entity);
        void UnLockUser(UserCommon entity);
        bool IsLocked(string userName);
        UserModel GetUserByName(string userName);
        bool CheckEmail(string email, string userName);
        void UpdateRoleUser(string userName, string roleName);
        List<UserCommon> GetUsers();
        List<UserModel> GetUserDTOs();
        UserModel GetUserById(string userId);
        void UpdateUser(UserCommon user);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool AddUser(UserCommon entity, string roleName)
        {
            var result = _userRepository.AddUser(entity, roleName);
            return result;
        }

        public bool CheckEmail(string email, string userName)
        {
            var result = _userRepository.CheckEmail(email,userName);
            return result;
        }

        public UserModel GetUserByName(string userName)
        {
            var user = _userRepository.GetUserByName(userName).SingleOrDefault();
            return user;
        }

        public List<UserCommon> GetUsers()
        {
            var users = _userRepository.GetUsers().ToList();
            return users;
        }

        public List<UserModel> GetUserDTOs()
        {
            var result = _userRepository.GetUsersAndRole().ToList();
            return result;
        }

        public void LockUser(UserCommon entity)
        {
            _userRepository.LockUser(entity.UserName, entity.Status);
        }

        public void UnLockUser(UserCommon entity)
        {
            _userRepository.LockUser(entity.UserName, entity.Status);
        }

        public void UpdateRoleUser(string userName, string roleName)
        {
            _userRepository.UpdateRoleUser(userName, roleName);
        }

        public UserModel GetUserById(string userId)
        {
            var user = _userRepository.GetUserById(userId).SingleOrDefault();
            return user;
        }

        public void UpdateUser(UserCommon user)
        {
            _userRepository.UpdateUser(user);
        }

        public bool IsLocked(string userName)
        {
            return _userRepository.IsLocked(userName);
        }
    }
}
