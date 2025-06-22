using Google.OrTools.Sat;
using Repository.Entities;
using Repository.Interfaces;
using Service.SeatAllocation.Interfaces;
using Service.SeatAllocation.Logic.Rules;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.SeatAllocation.Logic.Solver
{
	public class StudentContext : IRuleSet
	{
        public List<Student> Students { get; private set; }
		public List<Chair> Chairs { get; private set; }
		public CpModel Model { get; private set; }
		public Dictionary<string, IntVar> StudentChairVars { get; private set; }
		public LinearExprBuilder Objective { get; private set; }
		public int NumStudents => Students?.Count ?? 0;
		public int NumChairs => Chairs?.Count ?? 0;

        public StudentContext(List<Student> students, List<Chair> chairs)
		{
			Students = students;
			Chairs = chairs;
            Model = new CpModel();
            //
            StudentChairVars = new Dictionary<string, IntVar>();
			Objective = LinearExpr.NewBuilder();
        }



		public IEnumerable<IConstraintRule> GetConstraintRules()
		{
			IEnumerable<IConstraintRule> constraintRules = new List<IConstraintRule>();
			constraintRules.Append(new AllDifferentConstraintRule());
			return constraintRules;
		}

		public IEnumerable<IScoringRule> GetScoringRules()
		{
			IEnumerable<IScoringRule> scoringRules = new List<IScoringRule>();
			scoringRules.Append(new BackSeatInFront());
			scoringRules.Append(new ExtremeSeatInCenter());
			scoringRules.Append(new FavoriteFriendsInNearbySeat());
			scoringRules.Append(new HistorySeatsNotRepeat());
			scoringRules.Append(new LowAttentionLevelInCenter());
			scoringRules.Append(new LowAttentionLevelNotNearWindowOrDoor());
			scoringRules.Append(new NonFavoriteFriendsNotNearBySeat());

			return scoringRules;
		}

			//public bool AreChairsNear(int chairId1, int chairId2)
			//{
			//	// החישוב לפי Row/Column
			//	return false;
			//}

			//public Chair GetChairById(int id)
			//{
			//	return null;// repositoryChair.GetById(id);
			//}
			//private readonly IRepository<Chair, int> repositoryChair;
			//public Class Class { get; set; }
		}
}
