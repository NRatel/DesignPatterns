using UnityEngine;

namespace Adapter
{
    //-------------辅助说明类-------------
    //目标接口需要的参数
    public class TargetParameter { }
    //目标接口需要的返回值
    public class TargetReturnValue { }
    //被适配者现有接口需要的参数
    public class AdapteeParameter { }
    //被适配者现有接口需要的返回值
    public class AdapteeReturnValue { }
    //-------------辅助说明类-------------

    //目标， 定义需要的接口（需要哪些参数，需要返回什么值）
    public abstract class Target
    {
        public abstract TargetReturnValue Request(TargetParameter parameter);
    }

    //被适配者, 拥有与目标不一致的接口（参数不一致或返回值不一致，或两者都不一致）
    public class Adaptee
    {
        public AdapteeReturnValue SpecificalRequest(AdapteeParameter parameter)
        {
            Debug.Log("被适配者执行！");
            return new AdapteeReturnValue();
        }
    }

    //适配器
    public class Adapter : Target
    {
        private Adaptee adptee = new Adaptee();

        public override TargetReturnValue Request(TargetParameter parameter)
        {
            AdapteeParameter adapteeParameter = this.AdaptParameters(parameter);
            AdapteeReturnValue adapteeReturnValue = adptee.SpecificalRequest(adapteeParameter);
            TargetReturnValue targetReturnValue = this.AdaptReturnValues(adapteeReturnValue);
            return targetReturnValue;
        }

        //主要步骤1：将目标接口接收到的参数，转为被适配者需要的参数
        private AdapteeParameter AdaptParameters(TargetParameter parameter)
        {
            Debug.Log("适配/转换 参数");
            return new AdapteeParameter();
        }

        //主要步骤2：将被适配者的返回值，转为目标接口需要的参数
        private TargetReturnValue AdaptReturnValues(AdapteeReturnValue returnValue)
        {
            Debug.Log("适配/转换 返回值");
            return new TargetReturnValue();
        }
    }

    public class CLient
    {
        static public void Main()
        {
            //原来使用被适配者
            AdapteeReturnValue adapteeReturnValue = new Adaptee().SpecificalRequest(new AdapteeParameter());

            //使用适配器使用被适配者
            TargetReturnValue targetReturnValue = new Adapter().Request(new TargetParameter());
        }
    }
}