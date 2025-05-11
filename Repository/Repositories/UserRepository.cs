using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{

	public abstract class UserRepository<TUser> : IRepository<TUser, string> where TUser : User
	{
		public abstract TUser AddItem(TUser item);

		public abstract TUser DeleteItem(string id);

		public abstract List<TUser> GetAll();

		public abstract TUser GetById(string id);

		public abstract TUser UpdateItem(string id, TUser item);
	}
}// מה ההבדל בין רפוזחטורי לבין סרביס למה ברפוזיטורי הינו צריכות לעשות עם פונקציות אבסטרקטיות ולרשת את יוזר ובסרביס לא
