using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace Asp.Net.Project.Yad2.Services
{
    public interface IUserService
    {
        bool Add(UserModel model);

        void Update(UserModel model);

        bool IsExist(string userName);

        bool IsUserNameAndPasswordExist(string userName, string password);

        UserModel GetUser(string username, string password);
    }
}
