using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Composite
{
    //抽象组件
    public abstract class Component
    {
        protected string name;  //名称

        public int depth = 0;   //自身深度

        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component component);

        public abstract void Remove(Component component);

        public abstract Component GetChild(int index);

        public abstract void RecurveShow();

        //-------------------下面是本设计模式的非主要方法----------------------
        public void Operate()
        {
            Debug.Log(GetDepthStr() + GetName());
        }

        public string GetDepthStr()
        {
            string depthStr = "";
            for (int i = 0; i < this.depth; i++)
            {
                depthStr += "- ";
            }
            return depthStr;
        }

        public string GetName()
        {
            return this.name;
        }

        public abstract void RefreshDepth(int depth);
    }

    //叶节点
    public class Leaf : Component
    {
        public Leaf(string name) : base(name) { }

        public override void Add(Component component)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Component component)
        {
            throw new NotImplementedException();
        }

        public override Component GetChild(int index)
        {
            throw new NotImplementedException();
        }

        public override void RecurveShow()
        {
            this.Operate();
        }

        public override void RefreshDepth(int depth)
        {
            this.depth = depth;
        }
    }

    //枝节点
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();

        public Composite(string name) : base(name) { }

        public override void Add(Component component)
        {
            component.RefreshDepth(this.depth + 1);
            children.Add(component);
        }

        public override void Remove(Component component)
        {
            children.Remove(component);
        }

        public override Component GetChild(int index)
        {
            return children[index];
        }

        public override void RecurveShow()
        {
            this.Operate();
            foreach (Component child in children)
            {
                child.RecurveShow();
            }
        }

        public override void RefreshDepth(int depth)
        {
            this.depth = depth;
            foreach (Component child in children)
            {
                child.RefreshDepth(this.depth + 1);
            }
        }
    }

    public class Client
    {
        static public void Main()
        {
            //创建根节点，并加入两个叶节点
            Composite root = new Composite("root");
            root.Add(new Leaf("LeafA"));
            root.Add(new Leaf("LeafB"));

            //创建带有叶节点的枝节点，最后加入根节点
            Composite compX = new Composite("CompositeX");
            compX.Add(new Leaf("LeafC"));
            compX.Add(new Leaf("LeafD"));
            compX.Add(new Leaf("LeafE"));
            compX.Remove((Leaf)compX.GetChild(1));
            root.Add(compX);

            //创建更深层的带有叶节点的枝节点，最后加入根节点
            Composite compY = new Composite("CompositeY");
            Composite compZ = new Composite("CompositeZ");
            compX.Add(compY);
            compY.Add(compZ);
            compZ.Add(new Leaf("LeafF"));
            
            //递归显示
            root.RecurveShow();
        }
    }
}
