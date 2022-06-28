using Newtonsoft.Json.Linq;
using ODAMobile.Constants;
using ODAMobile.IService;
using ODAMobile.Models;
using ODAMobile.RestClient;
using ODAMobile.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ODAMobile.Service
{
    public class LoginService : ILoginService
    {
        private readonly IRestClient restClient;

        public LoginService(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        public async Task<login_message_rs> Login(string username, string password)
        {
            JObject @params = new JObject()
            {
                new JProperty("username", username.Trim()),
                new JProperty("password", StringFormatUtil.Encrypt(password))
            };
            login_message_rs rs = await restClient.PostAsync<login_message_rs, JObject>(ApiURL.URL_MAIN + "/NguoiDung/Login", @params);
            return rs;
        }
    }
}
