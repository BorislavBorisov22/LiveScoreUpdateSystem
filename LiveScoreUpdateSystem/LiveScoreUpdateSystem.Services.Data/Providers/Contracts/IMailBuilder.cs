using LiveScoreUpdateSystem.Data.Models.FootballFixtures;

namespace LiveScoreUpdateSystem.Services.Data.Providers.Contracts
{
    public interface IMailBuilder
    {
        string BuildFixtureMailSubject(Fixture fixture);

        string BuildFixtureMailContent(Fixture fixture);
    }
}
