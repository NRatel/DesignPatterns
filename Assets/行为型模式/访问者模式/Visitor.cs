using UnityEngine;

namespace Visitor
{
    //对象结构
    public class ObjectStructure
    {
        private ConcreteElementX elementX;
        private ConcreteElementY elementY;

        public ObjectStructure()
        {
            elementX = new ConcreteElementX();
            elementY = new ConcreteElementY();
        }

        public void VisitElementX(Visitor visitor)
        {
            //原来，在每次增加新的访问者时，都必须修改该方法，增加一个If和相应的访问操作逻辑。
            //if (visitor is ConcreteVisitorA)
            //{
            //    Debug.Log("ConcreteVisitorA 访问 ElementX");
            //}
            //else if (visitor is ConcreteVisitorB)
            //{
            //    Debug.Log("ConcreteVisitorB 访问 ElementY");
            //}

            // 现在总是这一句，保证了ObjectStructur类不因增加新的访问者（新的访问者对应新的操作）而被修改！
            // 访问者对元素的访问操作逻辑，被放到了访问者中去扩展。
            elementX.Accept(visitor);
        }

        public void VisitElementY(Visitor visitor)
        {
            elementY.Accept(visitor);
        }
    }

    //抽象元素
    public abstract class Element
    {
        public abstract void Accept(Visitor visitor);
    }

    //具体元素X
    public class ConcreteElementX : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitElementX(this);
        }
    }

    //具体元素Y
    public class ConcreteElementY : Element
    {
        public override void Accept(Visitor visitor)
        {
            visitor.VisitElementY(this);
        }
    }

    //抽象访问者
    public abstract class Visitor
    {
        //访问元素的接口，这里可以有两种写法。 
        //这两种写法都不能解决扩展“元素”时对“访问者”的修改问题（事实上该设计模式，就是在Element稳定的基础上进行的）。
        //推荐第二种（养成习惯，面向抽象编程和接口编程，尽可能将 已确定/不变 部分放在抽象类中）

        //写法1、在抽象 Visitor中 定义一个唯一访问接口，然后在每个ConcreteVisitor的实现中对每个元素进行区分对待。
        //public abstract void VisitElement(Element element);

        //写法2、在抽象 Visitor中 针对不同元素，定义不同的访问接口 VisitElementX、VisitElementY。
        public abstract void VisitElementX(ConcreteElementX element);
        public abstract void VisitElementY(ConcreteElementY element);
    }

    //具体访问者A
    public class ConcreteVisitorA : Visitor
    {
        public override void VisitElementX(ConcreteElementX element)
        {
            Debug.Log("ConcreteVisitorA 访问 ElementX");
        }

        public override void VisitElementY(ConcreteElementY element)
        {
            Debug.Log("ConcreteVisitorA 访问 ElementY");
        }
    }

    //具体访问者B
    public class ConcreteVisitorB : Visitor
    {
        public override void VisitElementX(ConcreteElementX element)
        {
            Debug.Log("ConcreteVisitorB 访问 ElementX");
        }

        public override void VisitElementY(ConcreteElementY element)
        {
            Debug.Log("ConcreteVisitorB 访问 ElementY");
        }
    }

    //客户
    public class Client
    {
        static public void Main()
        {
            ObjectStructure objectStructur = new ObjectStructure();
            ConcreteVisitorA concreteVisitorA = new ConcreteVisitorA();
            ConcreteVisitorB concreteVisitorB = new ConcreteVisitorB();

            objectStructur.VisitElementX(concreteVisitorA);
            objectStructur.VisitElementX(concreteVisitorB);

            objectStructur.VisitElementY(concreteVisitorA);
            objectStructur.VisitElementY(concreteVisitorB);
        }
    }
}

