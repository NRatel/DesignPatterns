namespace Singleton.StaticNestedClass
{
    public sealed class Singleton
    {
        //此静态内部类，只有在第一次使用时才会加载
        static private class Holder
        {
            static public readonly Singleton instance = new Singleton();
        }

        static public Singleton GetInstance()
        {
            return Holder.instance;
        }

        private Singleton() { }
    }
}
