using Repository.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
	public class Teacher:User
	{
		public virtual ICollection<Class>? Classes { get; set; }
	}
}
