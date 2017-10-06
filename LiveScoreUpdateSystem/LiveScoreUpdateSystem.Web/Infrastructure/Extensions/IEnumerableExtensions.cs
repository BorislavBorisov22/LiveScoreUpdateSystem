using LiveScoreUpdateSystem.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveScoreUpdateSystem.Web.Infrastructure.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T2> Map<T1, T2>(this IEnumerable<T1> collection)
        {
            return collection.Select(el => MappingService.MappingProvider.Map<T2>(el));
        }
    }
}