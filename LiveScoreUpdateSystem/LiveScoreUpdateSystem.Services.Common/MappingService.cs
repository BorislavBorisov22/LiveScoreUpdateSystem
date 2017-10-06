using AutoMapper;
using Bytes2you.Validation;
using LiveScoreUpdateSystem.Services.Common.Contracts;

namespace LiveScoreUpdateSystem.Services.Common
{
    public class MappingService : IMappingService
    {
        static MappingService()
        {
            MappingProvider = new MappingService();
        }

        public static IMappingService MappingProvider { get; set; }

        public TMapTo Map<TMapTo>(object from)
        {
            Guard.WhenArgument(from, "Object to map").IsNull().Throw();

            return Mapper.Map<TMapTo>(from);
        }
    }
}
