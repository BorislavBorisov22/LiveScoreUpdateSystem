using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using LiveScoreUpdateSystem.Data.Repositories.Contracts;
using LiveScoreUpdateSystem.Services.Data.Abstraction;
using LiveScoreUpdateSystem.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LiveScoreUpdateSystem.Services.Data
{
    public class UserService : DataService<User>, IUserService
    {
        private readonly IEfRepository<Team> teamsRepo;

        public UserService(IEfRepository<User> dataSet, IEfRepository<Team> teamsRepo)
            : base(dataSet)
        {
            Guard.WhenArgument(teamsRepo, "teamsRepo").IsNull().Throw();

            this.teamsRepo = teamsRepo;
        }

        public void SubscribeUserForTeamResults(string username, IEnumerable<string> teamsNames)
        {
            var subscribingUser = this.Data.All.FirstOrDefault(u => u.UserName == username);
            if (subscribingUser == null)
            {
                return;
            }

            var subscriptionTeams = this.teamsRepo
                .All
                .Where(t => teamsNames.Any(tn => tn == t.Name) &&
                            !t.Subscribers.Any(s => s.UserName == subscribingUser.UserName))
            .ToList();

            foreach (var teamSubscribingTo in subscriptionTeams)
            {

                teamSubscribingTo.Subscribers.Add(subscribingUser);
                this.teamsRepo.Update(teamSubscribingTo);
            }
        }
    }
}
