using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 消除 If Else 方式一
/// </summary>
namespace ClearIfElse.Way1.Old
{
    public class Executor
    {
        public void Execute(string parm)
        {
            if (parm == "A")
            {
                DoSthA();
            }
            else if (parm == "B")
            {
                DoSthB();
            }
            else if (parm == "C")
            {
                DoSthC();
            }
        }

        private void DoSthA() { Debug.Log("做A事"); }
        private void DoSthB() { Debug.Log("做B事"); }
        private void DoSthC() { Debug.Log("做C事"); }
    }

    public class Client
    {
        static public void Main()
        {
            Executor executor = new Executor();
            executor.Execute("A");
            executor.Execute("B");
            executor.Execute("C");
        }
    }
}

namespace ClearIfElse.Way1.New
{
    public class Executor
    {
        //创建集合收录参数和操作
        public Dictionary<string, Action> operateDict;
        public Executor()
        {
            operateDict = new Dictionary<string, Action>();
            operateDict.Add("A", DoSthA);
            operateDict.Add("B", DoSthB);
            operateDict.Add("C", DoSthC);
        }

        public void Execute(string parm)
        {
            operateDict[parm]();
        }

        private void DoSthA() { Debug.Log("做A事"); }
        private void DoSthB() { Debug.Log("做B事"); }
        private void DoSthC() { Debug.Log("做C事"); }
    }

    public class Client
    {
        static public void Main()
        {
            Executor executor = new Executor();
            executor.Execute("A");
            executor.Execute("B");
            executor.Execute("C");
        }
    }
}


/// <summary>
/// 消除 If Else 方式二
/// </summary>

//消除前
namespace ClearIfElse.Way2.Old
{
    public abstract class Parm { }
    public class A : Parm { }
    public class B : Parm { }
    public class C : Parm { }

    public class Executor
    {
        public void Execute(Parm parm)
        {
            if (parm is A)
            {
                DoSthA();
            }
            else if (parm is B)
            {
                DoSthB();
            }
            else if (parm is C)
            {
                DoSthC();
            }
        }

        private void DoSthA() { Debug.Log("做A事"); }
        private void DoSthB() { Debug.Log("做B事"); }
        private void DoSthC() { Debug.Log("做C事"); }
    }

    public class Client
    {
        static public void Main()
        {
            Executor executor = new Executor();
            executor.Execute(new A());
            executor.Execute(new B());
            executor.Execute(new C());
        }
    }
}

//消除后
namespace ClearIfElse.Way2.New
{
    public abstract class Parm
    {
        public abstract void DoSth();
    }

    public class A : Parm
    {
        public override void DoSth() { Debug.Log("做A事"); }
    }

    public class B : Parm
    {
        public override void DoSth() { Debug.Log("做B事"); }
    }

    public class C : Parm
    {
        public override void DoSth() { Debug.Log("做C事"); }
    }

    public class Executor
    {
        public void Execute(Parm parm)
        {
            parm.DoSth();
        }
    }

    public class Client
    {
        static public void Main()
        {
            Executor executor = new Executor();
            executor.Execute(new A());
            executor.Execute(new B());
            executor.Execute(new C());
        }
    }
}
