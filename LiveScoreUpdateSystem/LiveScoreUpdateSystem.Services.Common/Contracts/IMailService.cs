using System.Collections.Generic;

namespace LiveScoreUpdateSystem.Services.Common.Contracts
{
    public interface IMailService
    {
        void SendEmail(string subject, string content, IEnumerable<string> emails);
    }
}
