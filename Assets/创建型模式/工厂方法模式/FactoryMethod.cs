//独立负责一个产品的工厂方法
namespace FactoryMethod
{
    //抽象产品类
    public abstract class Product { }

    public class ConcreteProduct1 : Product { }

    public class ConcreteProduct2 : Product { }
    //可扩展新的具体产品
    //...

    //抽象工厂类
    public abstract class Creator
    {
        //定义创建对象的公共接口
        public abstract Product FactorMethod();
    }

    //具体工厂类1, 只用来生产产品1
    public class ConcreteCreator1 : Creator
    {
        public override Product FactorMethod()
        {
            return new ConcreteProduct1();
        }
    }

    //具体工厂类2，只用来生产产品2
    public class ConcreteCreator2 : Creator
    {
        public override Product FactorMethod()
        {
            return new ConcreteProduct2();
        }
    }

    public class Client
    {
        static public void Main()
        {
            ConcreteCreator1 factory1 = new ConcreteCreator1();
            ConcreteCreator2 factory2 = new ConcreteCreator2();

            factory1.FactorMethod();
            factory2.FactorMethod();
        }
    }
}


