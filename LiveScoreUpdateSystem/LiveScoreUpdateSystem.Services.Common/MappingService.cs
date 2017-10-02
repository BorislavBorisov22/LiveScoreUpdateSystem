using AutoMapper;
using LiveScoreUpdateSystem.Services.Common.Contracts;
using System;

namespace LiveScoreUpdateSystem.Services.Common
{
    public class MappingService : IMappingService
    {
        public TMapTo Map<TMapTo>(object from)
        {
            if (from == null)
            {
                throw new ArgumentNullException("Mapping from object cannot be null!");
            }

            return Mapper.Map<TMapTo>(from);
        }
    }
}
