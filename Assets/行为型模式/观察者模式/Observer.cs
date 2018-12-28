using System.Collections.Generic;
using UnityEngine;

namespace Observer
{
    //抽象主题
    public abstract class Subject
    {
        public List<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public abstract void Notify();
    }

    //抽象观察者
    public abstract class Observer
    {
        public abstract void Update(string newData);
    }

    //具体主题
    public class ConcreteSubject : Subject
    {
        private string data;
        public ConcreteSubject(string initData)
        {
            this.data = initData;
        }

        //主动通知
        public override void Notify()
        {
            foreach (Observer o in observers)
            {
                o.Update(data);
            }
        }

        //改变数据并通知（被动通知）
        public void ChangeData(string newData)
        {
            if (newData != data)
            {
                data = newData;
                Notify();
            }
        }
    }

    //具体观察者X
    public class ConcreteObserverX : Observer
    {
        private string data;
        public ConcreteObserverX(string initData) { data = initData; }

        public override void Update(string newData)
        {
            Debug.Log("ConcreteObserverX 数据更新：" + data + " => " + newData);
            data = newData;
        }
    }

    //具体观察者Y
    public class ConcreteObserverY : Observer
    {
        private string data;
        public ConcreteObserverY(string initData) { data = initData; }

        public override void Update(string newData)
        {
            Debug.Log("ConcreteObserverY 数据更新：" + data + " => " + newData);
            data = newData;
        }
    }

    //具体观察者Z
    public class ConcreteObserverZ : Observer
    {
        private string data;
        public ConcreteObserverZ(string initData) { data = initData; }

        public override void Update(string newData)
        {
            Debug.Log("ConcreteObserverZ 数据更新：" + data + " => " + newData);
            data = newData;
        }
    }

    public class Client
    {
        static public void Main()
        {
            ConcreteSubject subject = new ConcreteSubject("A");

            subject.Attach(new ConcreteObserverX("A"));
            subject.Attach(new ConcreteObserverY("A"));
            subject.Attach(new ConcreteObserverZ("A"));

            //改变状态
            subject.ChangeData("B");
        }
    }
}
