using System;

//泛型工厂方法
namespace FactoryMethod.GenericFunction
{
    //抽象产品类
    public abstract class Product { }

    //具体产品类
    public class ConcreteProduct1 : Product { }
    public class ConcreteProduct2 : Product { }
    //可扩展新的具体产品
    //...

    //抽象工厂类
    public abstract class Creator
    {
        //定义创建对象的公共接口
        public abstract T FactorMethod<T>() where T : Product;
    }

    //具体工厂类
    public class ConcreteCreator : Creator
    {
        //实现创建对象的公共接口
        public override T FactorMethod<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class Client
    {
        static public void Main()
        {
            //建造工厂
            ConcreteCreator factory = new ConcreteCreator();
            //创建具体产品1
            factory.FactorMethod<ConcreteProduct1>();
            //创建具体产品2
            factory.FactorMethod<ConcreteProduct2>();
        }
    }
}


