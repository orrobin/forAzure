using Entities;
using Data;
using System.Collections.Generic;
using System.Linq;
using Asp.Net.Project.Yad2.Services;

namespace UsersApp.Services
{
	public class UsersServiceSql : IUserService
	{
		private MyContext Context;

		public UsersServiceSql(MyContext context)
		{
			this.Context = context;
		}

		public bool Add(UserModel model)
		{
			UserModel duplicate = Context.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();
			if (duplicate == null)
			{
				Context.Users.Add(model);
				Context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Update(UserModel model)
		{
			var exist = Context.Users.Find(model.Id);
			Context.Entry(exist).CurrentValues.SetValues(model);
			Context.SaveChanges();
		}

        public bool IsExist(string userName)
        {
			UserModel userM = Context.Users.FirstOrDefault(user => user.UserName == userName);
			if (userM == null) return false;
			return true;
        }

        public bool IsUserNameAndPasswordExist(string userName, string password)
        {
			UserModel userM = Context.Users.FirstOrDefault(user => user.UserName == userName && user.Password == password);
			if (userM == null) return false;
			return true;
		}

		public UserModel GetUser(string username, string password)
		{
			UserModel user;
			user = Context.Users.FirstOrDefault(user => user.UserName == username && user.Password == password);
			return user;
		}

	}
}
