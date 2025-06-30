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
		public Dictionary<string, int> StudentScores { get; set; }
        public Dictionary<string, int>  InlayChairOfStudent{ get; set; }
        public LinearExprBuilder Objective { get; private set; }
		public int NumStudents => Students?.Count ?? 0;
		public int NumChairs => Chairs?.Count ?? 0;
		public List<IScoringRule> ScoringRules { get; set; }


		public StudentContext(List<Student> students, List<Chair> chairs)
		{
			Students = students;
			Chairs = chairs;
            Model = new CpModel();
            //
            StudentChairVars = new Dictionary<string, IntVar>();
			StudentScores = new Dictionary<string, int>();
			InlayChairOfStudent = new Dictionary<string, int>();

			Objective = LinearExpr.NewBuilder();
        }


        public IEnumerable<IConstraintRule> GetConstraintRules()
        {
            List<IConstraintRule> constraintRules = new List<IConstraintRule>();
            constraintRules.Add(new AllDifferentConstraintRule());
            return constraintRules;
        }


        public IEnumerable<IScoringRule> GetScoringRules()
		{
			List<IScoringRule> scoringRules = new List<IScoringRule>();
			scoringRules.Add(new BackSeatInFront());
			scoringRules.Add(new ExtremeSeatInCenter());
			scoringRules.Add(new FavoriteFriendsInNearbySeat());
			scoringRules.Add(new HistorySeatsNotRepeat());
			scoringRules.Add(new LowAttentionLevelInCenter());
			scoringRules.Add(new LowAttentionLevelNotNearWindowOrDoor());
			scoringRules.Add(new NonFavoriteFriendsNotNearBySeat());

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
