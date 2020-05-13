using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace newFace
{
    public static class TaskConvertor
    {
        public static Task<T> ToTask<T>(this T anyObject)
        {
            return Task.FromResult(
                anyObject
            );
        }
    }
}
