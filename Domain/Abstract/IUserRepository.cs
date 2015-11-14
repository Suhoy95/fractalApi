using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        String GetPassword(String login);
        String Login(String login, String token);
        void Create(String login, String password, String role);
        bool HasPermission(string login, int listId);
    }
}
