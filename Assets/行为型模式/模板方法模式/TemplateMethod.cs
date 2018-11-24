using UnityEngine;

namespace TemplateMethod
{
    public abstract class AbstractClass
    {
        //模板方法，将不变的行为(一般是步骤)总结出来。（子类不要重写这个方法）
        public void TemplateMethod()
        {
            DoStep1();
            DoStep2();
            DoStep3();
            Hook();
            DoStep4();
        }

        //其中，某些步骤是不变的，某些步骤是可变的。
        //不变的，直接给出实现，
        //可变的，在子类中确定具体的执行内容。
        private void DoStep1() { Debug.Log("DoStep1"); }            //不变的。
        public abstract void DoStep2();                             //可变的，实现延迟至子类。
        public abstract void DoStep3();                             //可变的，实现延迟至子类。

        //钩子方法。固定出现在某个地方，一般不是重要的关键步骤，父类可选择有实现或无实现，子类可选择重写或不重写。
        protected virtual void Hook() { Debug.Log("DoAfter3AndBefor4_XXX"); }  

        private void DoStep4() { Debug.Log("DoStep4"); }            //不变的。
    }

    //具体类A
    public class ConcreteClassA : AbstractClass
    {
        public override void DoStep2()
        {
            Debug.Log("DoStep2_A");
        }

        public override void DoStep3()
        {
            Debug.Log("DoStep3_A");
        }
    }
    
    //具体类B
    public class ConcreteClassB : AbstractClass
    {
        public override void DoStep2()
        {
            Debug.Log("DoStep2_B");
        }

        public override void DoStep3()
        {
            Debug.Log("DoStep3_B");
        }

        sealed protected override void Hook()          //可看情况，选择设置为密封方法
        {
            Debug.Log("DoAfter3AndBefor4_B");
        }
    }
    
    public class Client
    {
        static public void Main()
        {
            new ConcreteClassA().TemplateMethod();  //输出 DoStep1 DoStep2_A DoStep3_A DoAfter3AndBefor4_XXX DoStep4 
            new ConcreteClassB().TemplateMethod();  //输出 DoStep1 DoStep2_B DoStep3_B DoAfter3AndBefor4_B DoStep4 
        }
    }
}
