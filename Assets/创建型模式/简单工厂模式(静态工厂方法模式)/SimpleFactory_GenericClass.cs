using System;

//简单工厂模式，泛型类实现
namespace SimpleFactory.GenericClass
{
    //抽象产品类
    public abstract class Product { }

    //具体产品类
    public class ConcreteProduct1 : Product { }
    public class ConcreteProduct2 : Product { }
    //可扩展新的具体产品
    //...

    //泛型工厂类
    public class Factory<T>
    {
        //工厂方法
        public T FactorMethod()
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class Client
    {
        static public void Main()
        {
            new Factory<ConcreteProduct1>().FactorMethod();
            new Factory<ConcreteProduct2>().FactorMethod();
        }
    }
}
