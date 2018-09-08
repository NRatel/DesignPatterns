namespace Singleton.Hungry
{
    public sealed class Singleton
    {
        static private readonly Singleton instance = new Singleton();

        static public Singleton GetInstance()
        {
            return instance;
        }

        private Singleton() { }
    }
}
