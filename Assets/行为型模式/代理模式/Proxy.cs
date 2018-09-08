namespace Proxy
{
    //抽象主题
    public abstract class Subject
    {
        //定义和声明真实主题要被代理的业务方法接口
        public abstract void DoSth();
    }

    //真实主题
    public class RealSubject : Subject
    {
        //实现业务方法接口，真实要做的事情。
        public override void DoSth(){}
    }

    //代理者
    public class Proxy : Subject
    {
        //内部依赖真实主题
        private Subject subject = new RealSubject();

        //代理者拥有与真实主题(被代理者)
        public override void DoSth()
        {
            //额外的预先操作
            this.Before();
            //最终调用真实主题的业务方法
            this.subject.DoSth();
            //额外的后续操作
            this.After();
        }

        private void Before() { }
        private void After() { }
    }

    public class Client
    {
        static public void Main()
        {
            Proxy proxy = new Proxy();
            proxy.DoSth();
        }
    }
}

//真实主题不唯一， 但代理过程相同时
namespace Proxy.ExpandRealSubject
{
    public abstract class Subject
    {
        public abstract void DoSth();
    }

    //真实主题A
    public class RealSubjectA : Subject
    {
        public override void DoSth() { }
    }

    //真实主题B
    public class RealSubjectB : Subject
    {
        public override void DoSth() { }
    }
    
    public class Proxy : Subject
    {
        //代理过程相同，真实主题（被代理者）不同时，可在构造代理者时确定代理哪个真实主题。
        private Subject subject = null;
        public Proxy(Subject subject)
        {
            this.subject = subject;
        }
        
        public override void DoSth()
        {
            this.Before();
            this.subject.DoSth();
            this.After();
        }

        public void Before() { }
        public void After() { }
    }

    public class Client
    {
        static public void Main()
        {
            RealSubjectA realSubjectA = new RealSubjectA();
            RealSubjectB realSubjectB = new RealSubjectB();
            //相同的代理过程，分别代理真实主题A和真实主题B
            new Proxy(realSubjectA).DoSth();
            new Proxy(realSubjectB).DoSth();
        }
    }
}

//真实主题唯一， 但代理过程不同时
namespace Proxy.ExpandProxy
{
    public abstract class Subject
    {
        public abstract void DoSth();
    }

    public class RealSubject : Subject
    {
        public override void DoSth() { }
    }

    //代理X
    public class ProxyX : Subject
    {
        private Subject subject = new RealSubject();

        public override void DoSth()
        {
            this.BeforeX();
            this.subject.DoSth();
            this.AfterX();
        }

        private void BeforeX() { }
        private void AfterX() { }
    }

    //代理Y
    public class ProxyY : Subject
    {
        private Subject subject = new RealSubject();

        public override void DoSth()
        {
            this.BeforeB();
            this.subject.DoSth();
            this.AfterB();
        }

        private void BeforeB() { }
        private void AfterB() { }
    }

    public class Client
    {
        static public void Main()
        {
            //真实主题保持不变。
            RealSubject realSubject = new RealSubject();
            //A代理
            ProxyX proxyX = new ProxyX();
            proxyX.DoSth();
            //B代理
            ProxyY proxyY = new ProxyY();
            proxyY.DoSth();
            //扩展其他代理需求
            //...
        }
    }
}

//真实主题唯一， 代理过程也不同时
namespace Proxy.ExpandBoth
{
    public abstract class Subject
    {
        public abstract void DoSth();
    }

    //真实主题A
    public class RealSubjectA : Subject
    {
        public override void DoSth() { }
    }

    //真实主题B
    public class RealSubjectB : Subject
    {
        public override void DoSth() { }
    }

    //代理X
    public class ProxyX : Subject
    {
        //在构造代理者时确定代理哪个真实主题
        private Subject subject = null;
        public ProxyX(Subject subject)
        {
            this.subject = subject;
        }

        public override void DoSth()
        {
            this.BeforeX();
            this.subject.DoSth();
            this.AfterX();
        }

        private void BeforeX() { }
        private void AfterX() { }
    }

    //代理Y
    public class ProxyY : Subject
    {
        //在构造代理者时确定代理哪个真实主题
        private Subject subject = null;
        public ProxyY(Subject subject)
        {
            this.subject = subject;
        }

        public override void DoSth()
        {
            this.BeforeB();
            this.subject.DoSth();
            this.AfterB();
        }

        private void BeforeB() { }
        private void AfterB() { }
    }

    public class Client
    {
        static public void Main()
        {
            RealSubjectA realSubjectA = new RealSubjectA();
            RealSubjectB realSubjectB = new RealSubjectB();

            //X代理真实主题A
            ProxyX proxyXA = new ProxyX(realSubjectA);
            proxyXA.DoSth();
            //X代理真实主题B
            ProxyX proxyXB = new ProxyX(realSubjectB);
            proxyXB.DoSth();
            //Y代理真实主题A
            ProxyY proxyYA = new ProxyY(realSubjectA);
            proxyYA.DoSth();
            //Y代理真实主题B
            ProxyY proxyYB = new ProxyY(realSubjectB);
            proxyYB.DoSth();
        }
    }
}
