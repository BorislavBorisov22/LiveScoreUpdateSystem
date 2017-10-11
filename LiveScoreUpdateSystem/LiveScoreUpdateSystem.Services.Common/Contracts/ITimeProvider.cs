using System;

namespace LiveScoreUpdateSystem.Services.Common.Contracts
{
    public interface ITimeProvider
    {
        DateTime CurrentDate { get; }
    }
}
