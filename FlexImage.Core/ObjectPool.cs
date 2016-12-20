// ============================================
// 
// FlexImage.Core
// ObjectPool.cs
// 26/08/2012
// cflavio
// 
// ============================================

#region

using System;
using System.Collections.Generic;

#endregion

namespace FlexImage.Core
{
    public class ObjectPool
    {
        private Dictionary<Type, Dictionary<Int32, Object>> m_pool;

        public void AddItem<T>(Int32 pID, T value)
        {
            Type myType = typeof (T);

            if (!this.m_pool.ContainsKey(myType))
            {
                this.m_pool.Add(myType, new Dictionary<int, object>());
                this.m_pool[myType].Add(pID, value);
                return;
            }

            if (!this.m_pool[myType].ContainsKey(pID))
            {
                this.m_pool[myType].Add(pID, value);
                return;
            }

            this.m_pool[myType][pID] = value;
        }
    }
}