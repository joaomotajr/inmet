// ============================================
// 
// FlexImage.Core
// Singleton.cs
// 26/08/2012
// cflavio
// 
// ============================================

namespace FlexImage.Core
{
    public class GenericSingleton<T> where T : class, new()
    {
        private static T instance;

        public static T GetInstance()
        {
            lock (typeof (T))
            {
                if (instance == null)
                    instance = new T();
                return instance;
            }
        }
    }
}