using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flyweight
{
    //抽象蝇量
    public abstract class Flyweight
    {
        public abstract void Operation(string externalState);
    }

    //具体蝇量, 有可共享的内部状态
    public class ConcreteFlyweight : Flyweight
    {
        private readonly string internalState;  //声明为readonly,不可改变。
        public string externalState;

        //内部状态在构造时赋值
        public ConcreteFlyweight(string internalState)
        {
            this.internalState = internalState;
        }

        //外部状态可在Client中操作
        public override void Operation(string externalState)
        {
            this.externalState = externalState;
        }
    }

    //具体蝇量, 无可共享的内部状态
    public class UnsharedConcreteFlyweight: Flyweight
    {
        public string externalState;
        public UnsharedConcreteFlyweight(){ }

        //外部状态可在Client中操作
        public override void Operation(string externalState)
        {
            this.externalState = externalState;
        }
    }

    //这里想要说明 复用和共享是两个相互独立的概念，UnsharedConcreteFlyweight 也进行可以复用。
    //因为，一个蝇量工厂应该只对应一种蝇量对象（方便扩展）。
    //所以，这里抽象出一个蝇量工厂的基类，再分别派生两个蝇量工厂类 SharedFlyweightFactory 和 UnsharedFlyweightFactory。
    public abstract class FlyweightFactory
    {
        public FlyweightFactory(int initCount)
        {
            for (int i = 0; i < initCount; i++)
            {
                CreateFlyweight();
            }
        }

        public List<Flyweight> flyweights = new List<Flyweight>();

        //从蝇量工厂中拿出蝇量对象
        public Flyweight GetFlyweight()
        {
            if (flyweights.Count > 0)
            {
                Flyweight flyweight = flyweights[0];
                flyweights.RemoveAt(0);
                return flyweight;
            }
            else
            {
                CreateFlyweight();
                return GetFlyweight();
            }
        }

        //将不使用的蝇量对象放回蝇量工厂
        public void SetFlyweight(Flyweight flyweight)
        {
            //重置外部状态
            flyweight.Operation(null);
            flyweights.Add(flyweight);
        }

        public abstract void CreateFlyweight();
    }

    //有可共享的内部状态的蝇量工厂
    public class SharedFlyweightFactory : FlyweightFactory
    {
        public SharedFlyweightFactory(int initCount): base(initCount) {}

        //创建蝇量对象
        public override void CreateFlyweight()
        {
            flyweights.Add(new ConcreteFlyweight("这个字符串代表不变、共享的“一些”内部状态"));
        }
    }

    //无可共享的内部状态的蝇量工厂
    public class UnsharedFlyweightFactory : FlyweightFactory
    {
        public UnsharedFlyweightFactory(int initCount) : base(initCount) { }

        //创建蝇量对象
        public override void CreateFlyweight()
        {
            flyweights.Add(new UnsharedConcreteFlyweight());
        }
    }

    //客户
    public class Client
    {
        static public void Main()
        {
            //创建可共享内部状态的蝇量工厂，以一定数量初始化。
            FlyweightFactory sff = new SharedFlyweightFactory(2);

            //获取蝇量对象
            Flyweight sf1 = sff.GetFlyweight();  //蝇量工厂初始化时创建的蝇量对象。
            Flyweight sf2 = sff.GetFlyweight();  //蝇量工厂初始化时创建的蝇量对象。
            Flyweight sf3 = sff.GetFlyweight();  //蝇量工厂中蝇量对象不足时，新建的蝇量对象。
            //在Client中对蝇量对象的外部状态进行操作。
            sf1.Operation("这个字符串代表变化、不共享的“一些”外部状态，123123123");
            sf2.Operation("这个字符串代表变化、不共享的“一些”外部状态，456456456");
            sf3.Operation("这个字符串代表变化、不共享的“一些”外部状态，789789789");

            //将不使用的sf3放回蝇量工厂，以便之后进行复用。
            sff.SetFlyweight(sf3);

            //获取蝇量对象
            Flyweight sf4 = sff.GetFlyweight();  //获取到了上一步放回的sf3, 达到复用的目的。

            //------------------------------------------NRatel割------------------------------------------------

            //创建不可共享内部状态的蝇量工厂，以一定数量初始化。
            FlyweightFactory usff = new UnsharedFlyweightFactory(0);

            //获取蝇量对象
            Flyweight usf1 = sff.GetFlyweight();  //蝇量工厂中蝇量对象不足时，新建的蝇量对象。
            //在Client中对蝇量对象的外部状态进行操作。
            usf1.Operation("这个字符串代表变化、不共享的“一些”外部状态，789789789");

            //将不使用的usf1放回蝇量工厂，以便之后进行复用。
            usff.SetFlyweight(usf1);

            //获取蝇量对象
            Flyweight usf2 = sff.GetFlyweight();  //获取到了上一步放回的usf1, 达到复用的目的。
        }
    }
}
