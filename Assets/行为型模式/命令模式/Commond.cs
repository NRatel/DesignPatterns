namespace Commond
{
    //命令实现者，做自己的事，不关心命令
    public class Implementor
    {
        public void DoSthX() { }
        public void DoSthY() { }
        public void DoSthZ() { }
    }

    //抽象命令
    public abstract class Command
    {
        public abstract void Execute();
    }

    //具体命令A
    public class ConcreteCommandA : Command
    {
        //持有实现者
        private Implementor implementor;
        public ConcreteCommandA(Implementor implementor)
        {
            this.implementor = implementor;
        }

        public override void Execute()
        {
            //组织实现者的功能，实现具体命令
            //例如这里要使用实现者的XY功能方法
            this.implementor.DoSthX();
            this.implementor.DoSthY();
        }
    }

    //具体命令B
    public class ConcreteCommandB : Command
    {
        private Implementor implementor;
        public ConcreteCommandB(Implementor implementor)
        {
            this.implementor = implementor;
        }

        public override void Execute()
        {
            //组织实现者的功能，实现具体命令
            //例如这里要使用实现者的Z功能方法
            this.implementor.DoSthZ();
        }
    }

    //命令调用者
    public class Invoker
    {
        //组合持有命令，这里简化为参数方式传入
        public void Invoke(Command command)
        {
            command.Execute();
        }
    }

    //客户，命令装配
    public class Client
    {
        public Client()
        {
            //构造命令实现者
            Implementor implementor = new Implementor();
            //装配具体命令, 将命令实现者与具体命令关联
            ConcreteCommandA concreteCommandA = new ConcreteCommandA(implementor);
            ConcreteCommandB concreteCommandB = new ConcreteCommandB(implementor);
            //构造命令调用者
            Invoker invoker = new Invoker();
            //命令作为参数，提供给命令调用者
            invoker.Invoke(concreteCommandA);
            invoker.Invoke(concreteCommandB);
        }
    }
}