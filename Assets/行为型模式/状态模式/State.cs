using UnityEngine;

namespace State
{
    //抽象状态
    public abstract class State
    {
        public abstract void Handle(Context context);
    }

    //具体状态A
    public class ConcreteStateA : State
    {
        public override void Handle(Context context)
        {
            Debug.Log("状态为A时的处理方法");
        }
    }

    //具体状态B
    public class ConcreteStateB : State
    {
        public override void Handle(Context context)
        {
            Debug.Log("状态为B时的处理方法");
        }
    }

    //环境/上下文
    public class Context
    {
        private State currentSate;

        public Context(State initSate)
        {
            currentSate = initSate;
        }

        public void Request()
        {
            this.currentSate.Handle(this);
        }

        public void SwitchSate(State newSate)
        {
            this.currentSate = newSate;
        }
    }

    public class Client
    {
        static public void Main()
        {
            State stateA = new ConcreteStateA();
            State stateB = new ConcreteStateB();
            
            Context context = new Context(stateA);
            context.Request();  //表现状态A的行为
            context.SwitchSate(stateB);
            context.Request();  //表现状态B的行为
        }
    }
}
