using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace FractalApi.Controllers
{
    public class AuthController : ApiController
    {
        private IUserRepository db;

        public AuthController(IUserRepository db)
        {
            this.db = db;
        }

        [HttpPost]
        public String Login(User user)
        {
            user.Login = EvalMD5(user.Login);
            user.Password = EvalMD5(user.Password);

            var realPassword = db.GetPassword(user.Login);

            if (realPassword == null || user.Password != realPassword)
                throw new HttpResponseException(HttpStatusCode.Unauthorized);

            var token = CreateToken();
            var name = db.Login(user.Login, token);

            return user.Login + ":" + token + "#" + name;
        }

        [Authorize]
        [HttpPost]
        public void Check()
        {
        }

        [Authorize]
        [HttpPost]
        public bool HasPermission(int id)
        {
            return db.HasPermission(User.Identity.Name, id);
        }

        [Authorize]
        [HttpPost]
        public void ChangeName(String name)
        {
            db.UpdateName(User.Identity.Name, name);
        }

        [HttpGet]
        public String CreateUser(String login, String password)
        {
            login = EvalMD5(login);
            password = EvalMD5(password);

            db.Create(login, password, "User");

            return login + " <br />" + password;
        }

        [HttpGet]
        public String CreateAdmin(String login, String password)
        {
            login = EvalMD5(login);
            password = EvalMD5(password);

            db.Create(login, password, "Admin");

            return login + " <br />" + password;
        }

        private String EvalMD5(String s)
        {
            using (var md5Hash = MD5.Create())
            {
                byte[] d = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(s));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < d.Length; i++)
                {
                    sBuilder.Append(d[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        private String CreateToken()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
