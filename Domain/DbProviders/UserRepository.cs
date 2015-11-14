using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DbProviders
{
    public class UserRepository : DbHelper, IUserRepository
    {
        public string GetPassword(string login)
        {
            cmd.CommandText = "EXEC GetUser @login;";
            CreateTextParameter(login, "login");
            var res = cmd.ExecuteScalar();

            return res == null ? null : (String)res;
        }

        public String Login(string login, string token)
        {
            cmd.CommandText = "EXEC Login @login, @token;";
            CreateTextParameter(login, "login");
            CreateTextParameter(token, "token");
            return (String)cmd.ExecuteScalar();
        }

        public void Create(string login, string password, String role)
        {
            cmd.CommandText = "EXEC Create" + role + " @login, @pass;";
            CreateTextParameter(login, "login");
            CreateTextParameter(password, "pass");
            cmd.ExecuteNonQuery();
        }

        public bool HasPermission(string login, int listId)
        {
            cmd.CommandText = "EXEC UserHasPermission @login, @listId;";
            CreateTextParameter(login, "login");
            CreateIntParameter(listId, "listId");
            return 1 == (int)cmd.ExecuteScalar();
        }

        public async Task<String> GetRoleAsync(String login, String token)
        {
            cmd.CommandText = "EXEC Auth @login, @token;";
            CreateTextParameter(login, "login");
            CreateTextParameter(token, "token");

            var res = await cmd.ExecuteScalarAsync();
            return res == null ? null : (String)res;
        }
    }
}
