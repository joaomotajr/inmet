using System;
using FlexImage.WebService.LoginWebReference;

namespace FlexImage.WebService
{
    public class WSLogin
    {
        public LoginServiceService ProxyLogin = new LoginServiceService();
        
        private readonly login request = new login();
        private loginResponse userLogin = new loginResponse();

        private string endPoint = "";

        public string EndPoint
        {
            get { return endPoint; }
            set { 
                endPoint = value;
                ProxyLogin.Url = endPoint + "LoginService";
            }
        }

        public loginResponse UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        public string DoLogin(string userName, string stationName, string ip, string hash)
        {            
            request.userName = userName;
            request.stationName = stationName;
            request.localIP = ip;            
            request.userPassword = hash;
            
            try
            {
                userLogin = ProxyLogin.login(request);
            }
            catch (Exception e)
            {
                string msg = "";
                if (e.Message == "login.invalid")
                    msg = "Login inválido";
                else
                    msg = "Login::" + e;

                return msg;
            }
            return "";
        }
    }
}