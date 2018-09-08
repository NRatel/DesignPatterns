using System;

//简单工厂模式，泛型静态工厂方法实现
namespace SimpleFactory.GenericFunction
{
    //抽象产品类
    public abstract class Product { }

    //具体产品类
    public class ConcreteProduct1 : Product { }
    public class ConcreteProduct2 : Product { }
    //可扩展新的具体产品
    //...

    //工厂类
    public class Factory
    {
        //静态工厂方法
        public static T StaticFactorMethod<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class Client
    {
        static public void Main()
        {
            //使用静态工厂方法创建具体产品1
            Factory.StaticFactorMethod<ConcreteProduct1>();
            //使用静态工厂方法创建具体产品2
            Factory.StaticFactorMethod<ConcreteProduct2>();
        }
    }
}
