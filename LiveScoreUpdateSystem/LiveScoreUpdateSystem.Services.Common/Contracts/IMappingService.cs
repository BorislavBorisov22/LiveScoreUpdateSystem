namespace LiveScoreUpdateSystem.Services.Common.Contracts
{
    public interface IMappingService
    {
        TMapTo Map<TMapTo>(object from);
    }
}
