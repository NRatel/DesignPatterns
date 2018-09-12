using System;
using UnityEngine;

namespace Prototype
{
    //辅助测试成员类
    public class Extra
    {
        public int id;
        public string name;
        public Extra(int id, string name)
        {
            this.id = id; this.name = name;
        }
    }

    //抽象原型，提供抽象的拷贝接口方法
    public abstract class Prototype
    {
        public abstract Prototype ShallowClone();
        public abstract Prototype DeepClone();
    }

    //具体原型，实现拷贝方法
    public class ConcretePrototype : Prototype
    {
        public int id = 1;
        public string name = "A";
        public Extra extra = new Extra(1, "A");

        public override Prototype ShallowClone()
        {
            return (ConcretePrototype)this.MemberwiseClone();
        }

        public override Prototype DeepClone()
        {
            //先浅拷贝
            ConcretePrototype p = (ConcretePrototype)this.MemberwiseClone();
            //再处理引用类型成员新建处理。 以当前成员新new出来，或 在该引用类型中也实现深拷贝方法，然后调用。
            p.extra = new Extra(this.id, this.name);
            //这句其实可以不需要,因为虽然String不是基础成员，但因为其为静态常量，所以具有基础成员的性质。
            p.name = String.Copy(this.name);
            return p;
        }
    }

    public class Client
    {
        static public void Main()
        {
            ShallowCloneTest();
            Debug.Log("---------------------------------");
            DeepCloneTest();
        }

        static private void ShallowCloneTest()
        {
            //创建原型 a, 并浅拷贝为 b。
            ConcretePrototype a = new ConcretePrototype();
            ConcretePrototype b = (ConcretePrototype)a.ShallowClone();

            Debug.Log(a.id + ", " + a.name + ", " + a.extra.id + ", " + a.extra.name);   //1, A, 1, A
            Debug.Log(b.id + ", " + b.name + ", " + b.extra.id + ", " + b.extra.name);   //1, A, 1, A

            //尝试修改b
            b.id = 2;
            b.name = "B";
            b.extra.id = 2;
            b.extra.name = "B";

            //B正常，全部被修改
            //A的基础类型成员(string不是基础成员，但具有基础成员的性质)没有被修改(正常)。但引用类型成员被修改(因为浅拷贝的缘故)
            Debug.Log(a.id + ", " + a.name + ", " + a.extra.id + ", " + a.extra.name);   //1, A, 2, B
            Debug.Log(b.id + ", " + b.name + ", " + b.extra.id + ", " + b.extra.name);   //2, B, 2, B
        }

        static private void DeepCloneTest()
        {
            //创建原型 a, 并浅拷贝为 b。
            ConcretePrototype a = new ConcretePrototype();
            ConcretePrototype b = (ConcretePrototype)a.DeepClone();

            Debug.Log(a.id + ", " + a.name + ", " + a.extra.id + ", " + a.extra.name);   //1, A, 1, A
            Debug.Log(b.id + ", " + b.name + ", " + b.extra.id + ", " + b.extra.name);   //1, A, 1, A

            //尝试修改b
            b.id = 2;
            b.name = "B";
            b.extra.id = 2;
            b.extra.name = "B";

            //B正常，全部被修改。
            //A正常，没有因为B的修改而被修改。
            Debug.Log(a.id + ", " + a.name + ", " + a.extra.id + ", " + a.extra.name);   //1, A, 1, A
            Debug.Log(b.id + ", " + b.name + ", " + b.extra.id + ", " + b.extra.name);   //2, B, 2, B
        }
    }
}
