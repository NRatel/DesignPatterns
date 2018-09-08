using System;

//简单工厂模式，根据条件创建
namespace SimpleFactory
{
    //抽象产品类
    public abstract class Product { }

    //具体产品类
    public class ConcreteProduct1 : Product { }
    public class ConcreteProduct2 : Product { }

    //示例, 使用枚举作为创建参数
    public enum ProductType
    {
        Product1,
        Product2
    }
    //可扩展新的具体产品
    //...

    //工厂类
    public class Factory
    {
        //静态工厂方法
        public static Product StaticFactorMethod(ProductType productType)
        {
            Product product = null;
            switch (productType)
            {
                case (ProductType.Product1):
                    product = new ConcreteProduct1();
                    break;
                case (ProductType.Product2):
                    product = new ConcreteProduct2();
                    break;
                //...
            }
            return product;
        }
    }

    public class Client
    {
        static public void Main()
        {
            //使用静态工厂方法创建具体产品1
            Factory.StaticFactorMethod(ProductType.Product1);
            //使用静态工厂方法创建具体产品2
            Factory.StaticFactorMethod(ProductType.Product2);
        }
    }
}
