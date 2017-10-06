using LiveScoreUpdateSystem.Data.Models.FootballFixtures;

namespace LiveScoreUpdateSystem.Services.Data.Contracts
{
    public interface IPlayerService
    {
        void Add(Player playerToAdd, string teamName, string countryName);
    }
}
