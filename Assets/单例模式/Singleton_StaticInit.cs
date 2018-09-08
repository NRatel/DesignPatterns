namespace Singleton.StaticInit
{
    public sealed class Singleton
    {
        static private readonly Singleton instance;

        static public Singleton GetInstance()
        {
            return instance;
        }

        //静态构造
        static Singleton()
        {
            instance = new Singleton();
        }

        private Singleton() { }
    }
}
