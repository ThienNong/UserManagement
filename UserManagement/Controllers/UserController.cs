using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models.Views;
using UserManagement.Models.Cassandra;
using UserManagement.Services.BLL;
using Cassandra;

namespace UserManagement.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("GetAllUsers")]
        public ForFrontEnd<List<User>> GetAllUsers()
        {
            List<User> data = null;

            try
            {
                data = new UserService().GetAllUsers();

                if (data == null)
                    return ForFrontEnd<List<User>>.False(data, "Không có dữ liệu!", null);
            }
            catch (ServerErrorException e)
            {
                return ForFrontEnd<List<User>>.False(data, "Lỗi!", e);
            }
            catch (NoHostAvailableException e)
            {
                return ForFrontEnd<List<User>>.False(data, "Lỗi!", e);
            }
            catch (Exception e)
            {
                return ForFrontEnd<List<User>>.False(data, "Lỗi!", e);
            }

            return ForFrontEnd<List<User>>.True(data);
        }

        [HttpGet]
        [Route("GetAUser")]
        public ForFrontEnd<User> GetUser(string UID)
        {
            User data = null;
            
            try
            {
                data = new UserService().GetUser(UID);

                if (data == null)
                    return ForFrontEnd<User>.False(data, "Không tồn tại user này!", null);
            }
            catch (Exception e)
            {
                return ForFrontEnd<User>.False(data, "Lỗi!", e);
            }
            return ForFrontEnd<User>.True(data);
        }

        [HttpPost]
        [Route("AddUser")]
        public ForFrontEnd<User> AddUser([FromBody]User user)
        {
            User data = null;

            try
            {
                data = new UserService().Add(user);
                new UserProfileService().Add(new UserProfile("", "", "", "", "", user.UID));

                if (data == null)
                    return ForFrontEnd<User>.False(data, "User này đã tồn tại!", null);
            }
            catch (Exception e)
            {
                return ForFrontEnd<User>.False(data, "Lỗi!", e);
            }
            return ForFrontEnd<User>.True(data);
        }

        [HttpPut]
        [Route("UpdateUser")]
        public ForFrontEnd<User> UpdateUser([FromBody] User user)
        {
            User data = null;

            try
            {
                data = new UserService().Update(user);

                if (data == null)
                    return ForFrontEnd<User>.False(data, "User này không tồn tại!", null);
            }
            catch (Exception e)
            {
                return ForFrontEnd<User>.False(data, "Lỗi!", e);
            }
            return ForFrontEnd<User>.True(data);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public ForFrontEnd<User> DeleteUser([FromBody] User user)
        {
            User data = null;

            try
            {
                data = new UserService().Delete2(user);

                if (data == null)
                    return ForFrontEnd<User>.False(data, "User này không tồn tại!", null);
            }
            catch (Exception e)
            {
                return ForFrontEnd<User>.False(data, "Lỗi!", e);
            }
            return ForFrontEnd<User>.True(data);
        }
    }
}
