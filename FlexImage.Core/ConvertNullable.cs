// ============================================
// 
// FlexImage.Core
// ConvertNullable.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace FlexImage.Core
{
    public static class ConvertNullable
    {
        public static long[] ConvertNullableLongArrayToLongArray(long?[] nullableArray)
        {
            var list = new List<long>();
            for (int i = 0; i < nullableArray.Count(); i++)
            {
                if (nullableArray[i].HasValue)
                    list.Add((long) nullableArray[i]);
            }

            long[] longArray = list.ToArray();

            return longArray;
        }
    }
}