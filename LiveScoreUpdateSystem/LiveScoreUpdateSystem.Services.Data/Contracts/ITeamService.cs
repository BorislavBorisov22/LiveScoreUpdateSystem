using LiveScoreUpdateSystem.Data.Models.FootballFixtures;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface ITeamService
    {
        void Add(Team team, string leagueName);
    }
}
