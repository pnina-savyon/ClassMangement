using Google.OrTools.Sat;
using Repository.Entities;
using Repository.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Solver
{
	public class StudentContext
	{
		private readonly IRepository<Chair, int> repositoryChair;
		private List<Student> students { get;}
		private List<Chair> chairs { get; }
		private Dictionary<int, IntVar> studentChairVars { get; }
		private CpModel model { get; }

		//
		public StudentContext(IRepository<Chair, int> repositoryChair ,List<Student> students, List<Chair> chairs, CpModel model)
		{
			this.repositoryChair = repositoryChair;
			this.students = students;//?? throw new ArgumentNullException(nameof(students));
			this.chairs = chairs;// ?? throw new ArgumentNullException(nameof(chairs));
			this.model = model;//?? throw new ArgumentNullException(nameof(model));
			this.studentChairVars = new Dictionary<int, IntVar>();
		}



		public bool AreChairsNear(int chairId1, int chairId2)
		{
			// החישוב לפי Row/Column
			return false;
		}

		public Chair GetChairById(int id)
		{
			return null;// repositoryChair.GetById(id);
		}
	}
}
