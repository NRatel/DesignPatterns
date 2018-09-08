namespace Singleton.Lazy
{
    public sealed class Singleton
    {
        static private Singleton instance;

        static public Singleton GetInstance()
        {
            if (instance == null) { instance = new Singleton(); }
            return instance;
        }

        private Singleton() { }
    }
}
