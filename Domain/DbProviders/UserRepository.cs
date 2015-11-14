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
            cmd.CommandText = "EXEC GridAllowed @login, @listId;";
            CreateTextParameter(login, "login");
            CreateIntParameter(listId, "listId");
            return 0 != (int)cmd.ExecuteScalar();
        }

        public bool ItemAllowed(string login, Item item)
        {
            if (item.id < 0)
                return GridAllowed(login, item.gridId);

            cmd.CommandText = "EXEC ItemAllowed @login, @itemId;";
            CreateTextParameter(login, "login");
            CreateIntParameter(item.id, "itemId");
            return 0 != (int)cmd.ExecuteScalar();
        }

        public bool CoordsAllowed(string login, Coords coords)
        {
            var ids = GetUsersItemsIdForGrid(login, coords.GridId);
            coords.coords = coords.coords.OrderBy(coord => coord[0]).ToArray();

            if (ids.Count != coords.coords.Length)
                return false;
                
            for (var i = 0; i < ids.Count; i++)
                if (ids[i] != coords.coords[i][0])
                    return false;

            return true;
        }

        public List<int> GetUsersItemsIdForGrid(string login, int gridId)
        {
            cmd.CommandText = "EXEC GetListItemIds @login, @id;";
            CreateTextParameter(login, "login");
            CreateIntParameter(gridId, "id");

            var res = new List<int>();
            using(var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                    res.Add(dr.GetInt32(0));
            }

            return res;
        }
    }
}
