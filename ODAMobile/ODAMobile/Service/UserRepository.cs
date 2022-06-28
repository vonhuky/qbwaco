using ODAMobile.Helpers;
using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Service
{
    public class UserRepository : IUserRepository
    {
        DatabaseHelper _helper;
        public UserRepository()
        {
            _helper = new DatabaseHelper();
        }

        public void InsertUser(UserInfo user)
        {
            _helper.InsertUser(user);   
        }

        public List<UserInfo> GetAllUserData()
        {
            return _helper.GetAllUserData();
        }
    }
}
