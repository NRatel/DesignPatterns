using System;
using System.Reflection;
using UnityEngine;

namespace Singleton.Generic
{
    //泛型单例抽象类
    public abstract class Singleton<T> where T : class
    {
        static private T instance;

        static public T GetInstance()
        {
            return instance;
        }

        //静态构造
        static Singleton()
        {
            //instance = new T();   //不可行。需要“公有的无参构造函数”
            //instance = (T)Activator.CreateInstance(typeof(T));  //不可行，这种反射构造方式需要“公有的构造函数”
            //instance = (T)Assembly.GetAssembly(typeof(T)).CreateInstance(typeof(T).ToString());//不可行。这种反射构造方式需要“公有的构造函数”

            //可以利用反射在类外部调用类的私有无参构造函数进行构造！！！
            var ctors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
            var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0);
            if (ctor == null)
            {
                throw new Exception("\"" + typeof(T).ToString() + "\"类中不存在私有无参构造函数");
            }
            instance = (T)ctor.Invoke(null);
        }

        //为了能被子类继承，父类的构造必须不能私有
        //private Singleton() { }
        protected Singleton() { }

        //销毁单例的静态方法
        static public void Dispose()
        {
            instance = null;
        }
    }

    //通过继承实现单例类
    sealed public class A : Singleton<A>
    {
        private A() { }
    }

    //通过组合实现单例类(可以继承其他类)
    sealed public class B 
    {
        private B() { }

        static public B GetInstance()
        {
            //Singleton<T>类不仅可以当做单例基类，还可作为一个单例工厂（传入类名，传出单例）
            return Singleton<B>.GetInstance();
        }
    }

    //直接在客户中，使用单例工厂获取单例
    sealed public class C
    {
        private C() { }
    }

    public class Client
    {
        static public void Main()
        {
            //方式一、继承的单例
            A a = A.GetInstance();
            //方式二、内部调用静态泛型工厂
            B b = B.GetInstance();
            //方式三、直接使用静态泛型工厂
            C c = Singleton<C>.GetInstance();

            //A a2 = new A();   //无法直接构造, 达成单例目的
            //B b2 = new B();   //无法直接构造, 达成单例目的
            //C c2 = new C();   //无法直接构造, 达成单例目的

            //直观说明"继承"和"静态工厂"这两种方式最终都是一个单例。
            Debug.Log(A.GetInstance() == Singleton<A>.GetInstance()); //返回true
        }
    }
}


