using ODAMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ODAMobile.IService
{
    public interface ILoginService
    {
        Task<login_message_rs> Login(string username, string password);
    }
}
