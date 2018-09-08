//#define Net4;

namespace Singleton.LazyT
{
    //注意：仅.NET4 以上可用
#if Net4
    public sealed class Singleton
    {
        static private readonly Lazy<Singleton> lazy = new Lazy<Singleton>(() => new Singleton());

        public static Singleton GetInstance
        {
        return lazy.Value;
        }

        private Singleton() { }
    }
#endif
}
