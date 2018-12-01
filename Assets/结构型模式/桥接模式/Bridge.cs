using UnityEngine;

namespace Bridge
{
    //抽象实现者
    public abstract class Implementor
    {
        public abstract void DoSthImp();
    }

    //具体实现者A
    public class ConcreateImplementorA : Implementor
    {
        public override void DoSthImp()
        {
            Debug.Log("做A事");
        }
    }

    //具体实现者B
    public class ConcreateImplementorB : Implementor
    {
        public override void DoSthImp()
        {
            Debug.Log("做B事");
        }
    }

    //抽象物体
    public abstract class Abstraction
    {
        private Implementor implementor;
        public Abstraction(Implementor implementor)
        {
            this.implementor = implementor;
        }

        public void DoSth()
        {
            this.implementor.DoSthImp();
        }
    }

    //精确抽象物体X
    public class RefinedAbstractionX : Abstraction
    {
        public RefinedAbstractionX(Implementor implementor) : base(implementor)
        {
            Debug.Log(" RefinedAbstractionX ");
        }
    }

    //精确抽象物体Y
    public class RefinedAbstractionY : Abstraction
    {
        public RefinedAbstractionY(Implementor implementor) : base(implementor)
        {
            Debug.Log(" RefinedAbstractionY ");
        }
    }

    public class Client
    {
        static public void Main()
        {
            //为精确抽象物体X指定一个实现，以形成一个真实物体。
            //精确抽象物体和实现可以自由组合。
            RefinedAbstractionX rax1 = new RefinedAbstractionX(new ConcreateImplementorA());
            rax1.DoSth();

            RefinedAbstractionX rax2 = new RefinedAbstractionX(new ConcreateImplementorB());
            rax2.DoSth();

            RefinedAbstractionY ray1 = new RefinedAbstractionY(new ConcreateImplementorA());
            ray1.DoSth();

            RefinedAbstractionY ray2 = new RefinedAbstractionY(new ConcreateImplementorB());
            ray2.DoSth();
        }
    }
}