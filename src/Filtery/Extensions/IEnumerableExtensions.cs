using System.Collections.Generic;
using Filtery.Models;

namespace Filtery.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> BuildFiltery<T>(this IList<T> list, FilteryRequest filteryRequest)
        {
            // var predicate = PredicateBuilder.True<Member>();
            //
            // if (memberSearchContract?.MemberType != null)
            // {
            //     predicate = predicate.And(p => p.MemberTypeId == memberSearchContract.MemberType);
            // }
            
            
            //Scan assembly for class filter mapping file
            
            //And query
            
            //Or query
            
            //Merge Queries
            
            return new List<T>();
        }
    }
}