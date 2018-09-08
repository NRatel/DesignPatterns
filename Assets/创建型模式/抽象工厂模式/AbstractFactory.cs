namespace AbstractFactory
{
    #region 抽象层
    //抽象产品类
    public abstract class AbstractProduct { }

    //抽象产品 X族
    public abstract class AbstractProductX : AbstractProduct { }

    //抽象产品 Y族
    public abstract class AbstractProductY : AbstractProduct { }

    //抽象工厂：定义两个接口，生产X族产品，也生产Y族产品。
    public abstract class AbstractCreator
    {
        public abstract AbstractProductX CreateProductX();
        public abstract AbstractProductY CreateProductY();
    }
    #endregion

    #region 具体层
    //具体工厂, 生产A级X族，A级Y族产品
    public class CretorA : AbstractCreator
    {
        public override AbstractProductX CreateProductX() { return new ProductAX(); }
        public override AbstractProductY CreateProductY() { return new ProductAY(); }
    }

    //具体工厂, 生产B级X族，B级Y族产品
    public class CretorB : AbstractCreator
    {
        public override AbstractProductX CreateProductX() { return new ProductBX(); }
        public override AbstractProductY CreateProductY() { return new ProductBY(); }
    }

    //具体产品，A级X族
    public class ProductAX : AbstractProductX { }

    //具体产品，A级Y族
    public class ProductAY : AbstractProductY { }

    //具体产品，B级X族
    public class ProductBX : AbstractProductX { }

    //具体产品，B级Y族
    public class ProductBY : AbstractProductY { }
    #endregion

    public class Client
    {
        //客户类 只依赖抽象工厂和抽象产品类, 不依赖实际工厂和产品类
        //
        static public void Main()
        {
            //建造不同等级的工厂
            AbstractCreator cretorA = new CretorA();
            AbstractCreator cretorB = new CretorB();

            //在工厂类中创建抽象产品的具体子类的实例。

            //通过A级工厂生产X族产品
            AbstractProduct productAX = cretorA.CreateProductX();
            //通过A级工厂生产Y族产品
            AbstractProduct productAY = cretorA.CreateProductY();
            //通过B级工厂生产X族产品
            AbstractProduct productBX = cretorB.CreateProductX();
            //通过B级工厂生产Y族产品
            AbstractProduct productBY = cretorB.CreateProductY();
        }
    }
}