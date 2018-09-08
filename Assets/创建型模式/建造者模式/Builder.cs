using System.Collections.Generic;

namespace Builder
{
    //辅助测试Part类
    public class Part
    {
        public string partName;
        public Part(string partName) { this.partName = partName; }
    }

    //产品类
    public class Product
    {
        public string productName;
        //产品类中对Part有其固定的组织方法，是组织部件的算法中的不变部分。 如此处，产品名由部件名按传入顺序组成。
        //Director中也会对Part进行组织，是组织部件的算法中的变化部分。如此例，组织了部件的种类、数量、和传入顺序。
        public Product(List<Part> partList)
        {
            productName = "";
            partList.ForEach((Part p) => {
                productName += p.partName;
            });
        }
    }

    //抽象建造者类
    public abstract class Builder
    {
        public abstract Part BuildPart1();
        public abstract Part BuildPart2();
        public abstract Part BuildPart3();
    }

    //具体建造者类
    public class ConcreteBuilderX : Builder
    {
        public override Part BuildPart1()
        {
            return new Part("1X");
        }

        public override Part BuildPart2()
        {
            return new Part("2X");
        }
        public override Part BuildPart3()
        {
            return new Part("3X");
        }
    }

    //具体建造者类
    public class ConcreteBuilderY : Builder
    {
        public override Part BuildPart1()
        {
            return new Part("1Y");
        }

        public override Part BuildPart2()
        {
            return new Part("2Y");
        }
        public override Part BuildPart3()
        {
            return new Part("3Y");
        }
    }

    //导演类
    public class Director
    {
        //例：建造某个产品A的方法。
        public Product ConstructA()
        {
            //建造产品A 需要 ConcreteBuilderX 中提供的产品部件种类。
            //通过选择不同的ConcreteBuilder，使产品产生部件种类多样性。
            Builder builder = new ConcreteBuilderX();
            //产品A需要 只需要两个第三部分和一个第一部分，并且按331的顺序传入。
            //通过组织，使产品产生部件的种类组成(进行有限选择)、数量、顺序多样性。
            Part part3 = builder.BuildPart3();
            Part part3s = builder.BuildPart3();
            Part part1 = builder.BuildPart1();

            List<Part> parts = new List<Part>();
            parts.Add(part3);
            parts.Add(part3s);
            parts.Add(part1);
            //传入构造参数创建A
            return new Product(parts);
        }

        //例：建造某个产品B的方法。
        public Product ConstructB()
        {
            //建造产品B 需要 ConcreteBuilderY 中提供的产品部件种类。
            Builder builder = new ConcreteBuilderY();
            //产品B需要 只需要三个第三部分和一个第二部分、一个第三部分，并且按11123的顺序传入。
            Part part1 = builder.BuildPart1();
            Part part1s = builder.BuildPart1();
            Part part1ss = builder.BuildPart1();
            Part part2 = builder.BuildPart2();
            Part part3 = builder.BuildPart3();

            List<Part> parts = new List<Part>();
            parts.Add(part1);
            parts.Add(part1s);
            parts.Add(part1ss);
            parts.Add(part2);
            parts.Add(part3);
            return new Product(parts);
        }
    }

    //客户类
    public class Client
    {
        static public void Main()
        {
            Director director = new Director();
            Product productA = director.ConstructA();
            //Debug.Log(productA.productName); 
            
            Product productB = director.ConstructB();
            //Debug.Log(productB.productName); 
        }
    }
}
