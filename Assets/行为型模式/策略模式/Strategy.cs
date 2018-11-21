using UnityEngine;

namespace Strategy
{
    //抽象策略
    public abstract class Strategy
    {
        public abstract void AlgorithmInterface();
    }

    //具体策略A，实现A算法
    public class ConcretesStrategyA : Strategy
    {
        public override void AlgorithmInterface()
        {
            Debug.Log("算法A");
        }
    }

    //具体策略A，实现B算法
    public class ConcretesStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Debug.Log("算法B");
        }
    }
    
    //环境/上下文，持有具体策略，并可灵活切换；对外，为高层模块提供一个接口方法，方法中对策略进行封装或额外的组织，避免高层模块对策略的直接调用。
    public class Context
    {
        Strategy strategy;
        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public void DoSth()
        {
            //额外的预先操作
            this.Before();
            //最终调用真实主题的业务方法
            this.strategy.AlgorithmInterface();
            //额外的后续操作
            this.After();
        }

        private void Before() { }
        private void After() { }
    }

    //客户
    public class Client
    {
        static public void Main()
        {
            Context contextA = new Context(new ConcretesStrategyA());
            Context contextB = new Context(new ConcretesStrategyB());

            contextA.DoSth();
            contextB.DoSth();
        }
    }
}
