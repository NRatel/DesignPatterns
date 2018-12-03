using UnityEngine;

namespace ChainOfResponsibility
{
    //抽象处理者
    public abstract class Handler
    {
        public string name;         //名称
        public int level;           //自身等级
        public Handler successor;   //持有一个后继责任对象（另一个处理者）

        public Handler(string name, int level, Handler successor = null)
        {
            this.name = name;
            this.level = level;
            this.successor = successor;
        }

        //定义一个模板方法，确定责任传递规则，约定一个条件，满足条件时自己处理，否则交给后继者处理。
        public void HandleRequest(int needLevel)
        {   
            if (level >= needLevel)     //处理者等级大于等于请求要求的等级时，才可处理
            {
                this.HandleImp();
            }
            else
            {
                if (successor != null)
                {
                    //交给后继者处理。
                    successor.HandleRequest(needLevel);
                }
                else
                {
                    //注意，这里要处理好，无后继者时的情况。
                    //如，只好勉强自己处理。
                    this.HandleImp();
                }
            }
        }

        public abstract void HandleImp();
    }

    //具体处理者
    public class ConcreteHandler : Handler
    {
        public ConcreteHandler(string name, int level, Handler successor = null) : base(name, level, successor) { }

        public override void HandleImp()
        {
            Debug.Log("我来处理请求，我是: " + this.name);
        }
    }

    public class Client
    {
        static public void Main()
        {
            //组装责任链
            Handler handlerA = new ConcreteHandler("ConcreteHandlerA", 9);
            Handler handlerB = new ConcreteHandler("ConcreteHandlerB", 3, handlerA);
            Handler handlerC = new ConcreteHandler("ConcreteHandlerC", 2, handlerB);
            Handler handlerD = new ConcreteHandler("ConcreteHandlerD", 1, handlerC);

            handlerD.HandleRequest(1);  //请求要求等级为1时，D就可以处理
            handlerD.HandleRequest(5);  //请求要求等级为5时，A才能处理
        }
    }
}