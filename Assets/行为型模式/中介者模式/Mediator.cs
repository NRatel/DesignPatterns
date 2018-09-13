using System.Collections.Generic;

namespace Mediator
{
    //辅助测试参数类
    public class ARes { }
    public class AProduct { }
    public class BRes { }
    public class BProduct { }
    public class CRes { }
    public class CProduct { }
    public class FinalProduct { }

    //抽象中介者类
    public abstract class Mediator
    {
        //对具体同事A提供的接口，需要什么、产出什么
        public abstract ARes GetARes();
        public abstract void OutputAProduct(AProduct aProduct);

        //对具体同事A提供的接口，需要什么、产出什么
        public abstract BRes GetBRes();
        public abstract void OutputBProduct(BProduct bProduct);

        //对具体同事A提供的接口，需要什么、产出什么
        public abstract CRes GetCRes();
        public abstract void OutputCProduct(CProduct cProduct);

        public abstract FinalProduct GetFinalProduct();
    }

    //抽象同事类
    public abstract class Colleague
    {
        //持有中介者，只和中介者交互
        protected Mediator mediator;
        public Colleague(Mediator mediator)
        {
            this.mediator = mediator;
        }

        //抽象接口，做自己的事，在这个方法中，从中介者拿到所需资源，生产，然后将产品交给中介者。
        public abstract void DoSelfDuty();
    }

    //具体同事A
    public class ConcreteColleagueA : Colleague
    {
        //构造函数，至少需要传入中介者
        public ConcreteColleagueA(Mediator mediator) : base(mediator) { }

        public override void DoSelfDuty()
        {
            //从中介者拿到所需资源
            ARes aRes = this.mediator.GetARes();
            //生产
            AProduct aProduct = ProcessA(aRes);
            //将产品交给中介者
            this.mediator.OutputAProduct(aProduct);
        }

        private AProduct ProcessA(ARes aRes)
        {
            //这里简略了资源变为产品的加工过程
            return new AProduct();
        }
    }

    //具体同事B
    public class ConcreteColleagueB : Colleague
    {
        public ConcreteColleagueB(Mediator mediator) : base(mediator) { }

        public override void DoSelfDuty()
        {
            BRes bRes = this.mediator.GetBRes();
            BProduct bProduct = ProcessB(bRes);
            this.mediator.OutputBProduct(bProduct);
        }

        private BProduct ProcessB(BRes bRes)
        {
            return new BProduct();
        }
    }

    //具体同事C
    public class ConcreteColleagueC : Colleague
    {
        public ConcreteColleagueC(Mediator mediator) : base(mediator) { }

        public override void DoSelfDuty()
        {
            CRes cRes = this.mediator.GetCRes();
            CProduct cProduct = ProcessC(cRes);
            this.mediator.OutputCProduct(cProduct);
        }

        private CProduct ProcessC(CRes cRes)
        {
            return new CProduct();
        }
    }

    //具体中介者类, 负责具体协调逻辑。
    public class ConcreteMediator : Mediator
    {
        //示例，产品或半成品仓库，缓存具体同事ABC的产出。ABC所需的资源也可从这里拿，最终产品所需的资源也可以从这里拿。
        protected List<AProduct> aProductList = new List<AProduct>();
        protected List<BProduct> bProductList = new List<BProduct>();
        protected List<CProduct> cProductList = new List<CProduct>();

        public override ARes GetARes()
        {
            //这里简略ARes的产生过程，这里可以决定A所需的资源由什么组成
            return new ARes();
        }

        public override BRes GetBRes()
        {
            //这里简略BRes的产生过程，这里可以决定B所需的资源由什么组成
            return new BRes();
        }

        public override CRes GetCRes()
        {
            //这里简略CRes的产生过程，这里可以决定C所需的资源由什么组成
            return new CRes();

            //例如: CRes可能由AProduct和BProduct两种半成品组成, 需要同时在CRes中提供对应的构造函数
            //return new CRes(aProductSet[0], bProductSet[0]);
        }

        //A产出的产品放入缓存仓库
        public override void OutputAProduct(AProduct aProduct)
        {
            aProductList.Add(aProduct);
        }

        //B产出的产品放入缓存仓库
        public override void OutputBProduct(BProduct bProduct)
        {
            bProductList.Add(bProduct);
        }

        //C产出的产品放入缓存仓库
        public override void OutputCProduct(CProduct cProduct)
        {
            cProductList.Add(cProduct);
        }

        //最终产品,提供给客户端的接口
        public override FinalProduct GetFinalProduct()
        {
            //这里简略FinalProduct的产生过程。
            //这里可以决定最终产品所需的资源由什么组成。
            //可能ABC其中的某个产品就是最终产品，也可能由ABC的半成品组成，但是这个组成过程也可以扩展一个D并且放到D中去。最终让某个具体同事类的产品作为最终产品。
            return new FinalProduct();
        }
    }

    public class Client
    {
        static public void Main()
        {
            //构建具体类
            ConcreteMediator concreteMediator = new ConcreteMediator();
            ConcreteColleagueA concreteColleagueA = new ConcreteColleagueA(concreteMediator);
            ConcreteColleagueB concreteColleagueB = new ConcreteColleagueB(concreteMediator);
            ConcreteColleagueC concreteColleagueC = new ConcreteColleagueC(concreteMediator);

            //各司其职
            concreteColleagueA.DoSelfDuty();
            concreteColleagueB.DoSelfDuty();
            concreteColleagueC.DoSelfDuty();

            //得到最终产品
            FinalProduct finalProduct = concreteMediator.GetFinalProduct();
        }
    }
}