using UnityEngine;

namespace Facade
{
    //外观类
    public class Facade
    {
        SubSystem.ModuleA moduleA = new SubSystem.ModuleA();
        SubSystem.ModuleB moduleB = new SubSystem.ModuleB();
        SubSystem.ModuleC moduleC = new SubSystem.ModuleC();

        public void DoSth()
        {
            moduleA.DoSthA();
            moduleB.DoSthB();
            moduleC.DoSthC();
        }
    }

    //子系统，由多个由关联的模块组成而成的一个系统
    public class SubSystem
    {
        public class ModuleA
        {
            public void DoSthA()
            {
                Debug.Log("做A事情");
            }
        }

        public class ModuleB
        {
            public void DoSthB()
            {
                Debug.Log("做B事情");
            }
        }

        public class ModuleC
        {
            public void DoSthC()
            {
                Debug.Log("做C事情");
            }

        }
    }

    public class Client
    {
        static public void Main()
        {
            //new SubSystem.ModuleA().DoSthA();
            //new SubSystem.ModuleB().DoSthB();
            //new SubSystem.ModuleC().DoSthC();

            //使用同一的接口，避免访问子系统的细节模块
            new Facade().DoSth();
        }
    }
}