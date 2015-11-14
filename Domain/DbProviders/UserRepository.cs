using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DbProviders
{
    public class UserRepository : DbHelper, IUserRepository, IPermissionChecker
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

        public async Task<String> GetRoleAsync(String login, String token)
        {
            cmd.CommandText = "EXEC Auth @login, @token;";
            CreateTextParameter(login, "login");
            CreateTextParameter(token, "token");

            var res = await cmd.ExecuteScalarAsync();
            return res == null ? null : (String)res;
        }


        public void UpdateName(string login, string name)
        {
            cmd.CommandText = "EXEC ChangeName @login, @name;";
            CreateTextParameter(login, "login");
            CreateTextParameter(name, "name");
            cmd.ExecuteNonQuery();
        }

        public bool GridAllowed(string login, int listId)
        {
            cmd.CommandText = "EXEC UserHasPermission @login, @listId;";
            CreateTextParameter(login, "login");
            CreateIntParameter(listId, "listId");
            return 1 == (int)cmd.ExecuteScalar();
        }

        public bool ItemAllowed(string login, Item item)
        {
            throw new NotImplementedException();
        }

        public bool CoordsAllowed(string login, Coords coords)
        {
            throw new NotImplementedException();
        }
    }
}
