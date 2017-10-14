using System;
using System.Collections.Generic;
using LiveScoreUpdateSystem.Services.Data.Providers.Contracts;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using Bytes2you.Validation;
using LiveScoreUpdateSystem.Data.Models.FootballFixtures;

namespace LiveScoreUpdateSystem.Services.Data.Providers
{
    public class FixtureMailService : IFixtureMailService
    {
        private readonly IMailService mailService;
        private readonly IMailBuilder mailBuilder;

        public FixtureMailService(IMailService mailService, IMailBuilder mailBuilder)
        {
            Guard.WhenArgument(mailService, "mailService").IsNull().Throw();
            Guard.WhenArgument(mailBuilder, "mailBuilder").IsNull().Throw();

            this.mailService = mailService;
            this.mailBuilder = mailBuilder;
        }

        public void SendFixtureResultMail(Fixture fixture, IEnumerable<string> subscribersMails)
        {
            var mailSubject = this.mailBuilder.BuildFixtureMailSubject(fixture);
            var mailContent = this.mailBuilder.BuildFixtureMailContent(fixture);

            this.mailService.SendEmail(mailSubject, mailContent, subscribersMails);
        }
    }
}
