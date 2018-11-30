using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Decorator
{
    //抽象组件
    public abstract class Component
    {
        public abstract void DoSth();
    }

    //具体组件
    public class ConcretComponent : Component
    {
        public override void DoSth()
        {
            Debug.Log("做X事情");
        }
    }

    //抽象装饰类
    public abstract class Decorator : Component
    {
        public Component component;
        public Decorator(Component component)
        {
            this.component = component;
        }

        //这里方便起见，将DoSth定位一个模板方法。实际可以由子类决定DoSth时，除了做原来的事，还要怎么做别的事。
        public override void DoSth()
        {
            this.component.DoSth();
            DoOtherThing();
        }

        public abstract void DoOtherThing();
    }

    //具体装饰类A
    public class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component component) : base(component) { }

        //增加其他事情
        public override void DoOtherThing()
        {
            Debug.Log("做A事情");
        }
    }

    //具体装饰类B
    public class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component component) : base(component) { }

        //增加其他事情
        public override void DoOtherThing()
        {
            Debug.Log("做B事情");
        }
    }

    //具体装饰类C
    public class ConcreteDecoratorC : Decorator
    {
        public ConcreteDecoratorC(Component component) : base(component) { }

        //增加其他事情
        public override void DoOtherThing()
        {
            Debug.Log("做C事情");
        }
    }

    public class Client
    {
        static public void Main()
        {
            //正常情况下，具体做X事。
            ConcretComponent cc = new ConcretComponent();
            cc.DoSth();

            //现在想让组件类，除了能做X事，还能做A事。
            //此时不用修改这个组件类。
            //而是将其放入具体装饰器（或称为“包装器”更贴切）中。
            ConcreteDecoratorA cdA = new ConcreteDecoratorA(cc);
            ConcreteDecoratorB cdB = new ConcreteDecoratorB(cc);
            ConcreteDecoratorB cdC = new ConcreteDecoratorB(cc);

            cdA.DoSth();    //既能干X、又能干A
            cdB.DoSth();    //既能干X、又能干B
            cdC.DoSth();    //既能干X、又能干C

            //------------------------------------------------------------------
            //除了上边那样简单的单层装饰，还能进行任意组合的装饰

            new ConcreteDecoratorB(cdA).DoSth();    //既能干X、又能干A、又能干B
            new ConcreteDecoratorA(cdB).DoSth();    //同上，顺序变化

            new ConcreteDecoratorC(cdA).DoSth();    //既能干X、又能干A、又能干C
            new ConcreteDecoratorA(cdC).DoSth();    //同上，顺序变化

            new ConcreteDecoratorC(cdB).DoSth();    //既能干X、又能干B、又能干C
            new ConcreteDecoratorB(cdC).DoSth();    //同上，顺序变化

            new ConcreteDecoratorA(new ConcreteDecoratorB(cdC)).DoSth();    //既能干X、又能干A、又能干B、又能干C
            new ConcreteDecoratorA(new ConcreteDecoratorC(cdB)).DoSth();    //同上，顺序变化
            new ConcreteDecoratorB(new ConcreteDecoratorA(cdC)).DoSth();    //同上，顺序变化
            new ConcreteDecoratorB(new ConcreteDecoratorC(cdA)).DoSth();    //同上，顺序变化
            new ConcreteDecoratorC(new ConcreteDecoratorA(cdB)).DoSth();    //同上，顺序变化
            new ConcreteDecoratorA(new ConcreteDecoratorB(cdA)).DoSth();    //同上，顺序变化

            //------------------------------------------------------------------
            //甚至可以同方法多次装饰
            new ConcreteDecoratorA(cdA).DoSth();
            new ConcreteDecoratorA(new ConcreteDecoratorA(cdA)).DoSth();
        }
    }
}