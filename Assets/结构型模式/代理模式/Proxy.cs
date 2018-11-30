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
            //此处定义可访问条件，如只有亲友可访问
            string who = "zhangsan";
            bool canVisit = (who == "kinsfolk" || who == "friend");
            if (canVisit)
            {
                //最终调用真实主题的业务方法
                this.subject.DoSth();
            }
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