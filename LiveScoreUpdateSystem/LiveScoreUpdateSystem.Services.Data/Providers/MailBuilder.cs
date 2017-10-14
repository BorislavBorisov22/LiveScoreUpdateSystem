using LiveScoreUpdateSystem.Services.Data.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;

namespace LiveScoreUpdateSystem.Services.Data.Providers
{
    public class MailBuilder : IMailBuilder
    {
        public string BuildFixtureMailSubject(Fixture fixture)
        {
            return string.Format("{0} - {1} game report", fixture.HomeTeam.Name, fixture.AwayTeam.Name);
        }

        public string BuildFixtureMailContent(Fixture fixture)
        {
            var builder = new StringBuilder();
            builder.AppendLine(string.Format("Here is the latest report from the game between {0} and {1}", fixture.HomeTeam.Name, fixture.AwayTeam.Name));
            builder.AppendLine(string.Format(
                "Match result: {0} {1} {2} {3}",
                fixture.HomeTeam.Name,
                fixture.ScoreHomeTeam,
                fixture.ScoreAwayTeam,
                fixture.AwayTeam.Name));

            var gameEventsRepots = fixture.FixtureEvents.Select(f => string.Format("{0} {1} -> {2} {3}",
                f.Minute, f.FixtureEventType,
                f.InvolvedPlayer.FirstName,
                f.InvolvedPlayer.LastName));

            builder.AppendLine(string.Join(Environment.NewLine, gameEventsRepots));
            builder.AppendLine();
            builder.AppendLine("Kind regards,");
            builder.AppendLine("LiveScoreUpdateSystem\' team");

            return builder.ToString().Trim();
        }
    }
}
