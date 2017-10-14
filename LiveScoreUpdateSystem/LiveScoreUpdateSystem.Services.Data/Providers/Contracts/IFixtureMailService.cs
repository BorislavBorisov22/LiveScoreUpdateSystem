using LiveScoreUpdateSystem.Data.Models.FootballFixtures;
using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Data.Providers.Contracts
{
    public interface IFixtureMailService
    {
        void SendFixtureResultMail(Fixture fixture, IEnumerable<string> subscribersMails);
    }
}
