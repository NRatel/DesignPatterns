namespace Singleton.Generic
{
    //泛型单例抽象类
    public abstract class Singleton<T> where T : class, new()
    {
        static private readonly T instance;

        static public T GetInstance()
        {
            return instance;
        }

        //静态构造
        static Singleton()
        {
            // T要能被实例化，必须在where中添加 new() 约束，同时T必须提供一个公有的无参构造函数
            instance = new T();
        }

        //为了能被子类继承，只能放弃父类中对单例的构造函数进行私有化这个要点
        //private Singleton() { }
    }

    //通过继承实现实际的单例类
    public class A : Singleton<A>
    {
        //为了能够被实例化，只能放弃子类中对单例的构造函数进行私有化这个要点
        //private A() { }
    }

    public class Client
    {
        private void Main()
        {
            //期望这样访问
            A instatnce = A.GetInstance();

            //实际因为没私有化构造方法，可以在外部执行多次实例化
            A a = new A();
            A aa = new A();
            A aaa = new A();
        }
    }
}


