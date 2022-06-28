using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODAMobile.Service
{
    public interface IUserRepository
    {
        List<UserInfo> GetAllUserData();

        void InsertUser(UserInfo user);
    }
}
