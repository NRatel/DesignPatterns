namespace Singleton.DoubleCheckedLocking
{
    public sealed class Singleton
    {
        private static Singleton instance;

        private static readonly object theLock = new object();

        private Singleton() { }

        public static Singleton GetInstance()
        {
            // 第一重检查, 避免 “实例已经存在时仍然执行lock获取锁, 影响性能” 的问题。
            if (instance == null)
            {
                //让线程排队执行被lock包围的代码段
                lock (theLock)
                {
                    //第二重检查, 仅让队首的线程进入if创建实例。
                    if (instance == null)
                    {
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}