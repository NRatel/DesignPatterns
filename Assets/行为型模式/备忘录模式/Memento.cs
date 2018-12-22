using System.Collections.Generic;
using UnityEngine;

namespace Memento
{
    //状态数据类用于辅助说明
    public struct State
    {
        public string name; //状态名
        //...状态的其他数据

        public State(string name/*, ...状态的其他数据*/)
        {
            this.name = name;
        }
    }
    //-------------------------------------

    //备忘录
    public class Memento
    {
        private State state;

        //一般而言，备忘录被创建后，持有的State应该是只读的，所以在构造时初始化，并且不提供SetState()方法
        public Memento(State state)
        {
            this.state = state;
        }

        //白箱备忘录
        //暴露了备忘录中的状态给任何对象
        //public State GetState()
        //{
        //    return state;
        //}

        //黑箱备忘录
        //提供一个方法，反调“以参数形式传入的Originator”的改变状态接口(或者将originator直接放在Memento构造时)。
        //这样, 虽然这个方法是公有的，但只可能被Originator使用, 保证了备忘录的安全性。缺点是, Memento反向依赖了Originator，增加了耦合度。
        public void Restore(Originator originator)
        {
            originator.ChangeState(this.state);
        }

        //另一种黑箱备忘录
        //Memento构造时就传入 originator对象
        //public void Restrore()
        //{
        //    originator.ChangeState(this.state);
        //}
    }

    public class Originator
    {
        private State state;

        //自身状态自然变化
        public void ChangeState(State state)
        {
            this.state = state;
        }

        //获取当前自身状态
        public State GetState()
        {
            return state;
        }

        //创建备忘录，保存当前状态
        public Memento CreateMemento()
        {
            return new Memento(state);
        }

        //使用备忘录恢复状态。
        //public void RestoreFromMemento(Memento Memento)
        //{
        //    this.state = Memento.GetState();
        //}

        public void RestoreFromMemento(Memento Memento)
        {
            Memento.Restore(this);
        }
    }

    public class CareTaker
    {
        private Dictionary<string, Memento> mementoDict = new Dictionary<string, Memento>();

        public void AddMemento(string stateName, Memento state)
        {
            mementoDict.Add(stateName, state);
        }

        public Memento GetMemento(string stateName )
        {
            return mementoDict[stateName];
        }
    }

    public class Client
    {
        public static void Main()
        {
            Originator originator = new Originator();
            CareTaker careTaker = new CareTaker();
            originator.ChangeState(new State("状态一"));
            originator.ChangeState(new State("状态二"));

            //存储当前状态
            string key = originator.GetState().name;
            careTaker.AddMemento(key, originator.CreateMemento());
            
            originator.ChangeState(new State("状态三"));

            //恢复到之前备份的某一状态
            originator.RestoreFromMemento(careTaker.GetMemento(key));

            Debug.Log("目前状态：" + originator.GetState().name);
        }
    }
}


