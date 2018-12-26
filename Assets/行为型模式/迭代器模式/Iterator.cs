using System.Collections.Generic;
using UnityEngine;

namespace Iterator
{
    public interface Iterator
    {
        object Current();
        bool Next();
    }
    
    public interface Aggregate
    {
        Iterator GetIterator();
    }

    public class ConcreateIterator : Iterator
    {
        private ConcreateAggregate integerList;
        private int index;

        public ConcreateIterator(ConcreateAggregate integerList)
        {
            this.integerList = integerList;
            this.index = 0;
        }

        public object Current()
        {
            return integerList.Get(index);
        }

        public bool Next()
        {
            index += 1;
            if (index < integerList.Count)
            {
                return true;
            }
            //重置
            index = 0;
            return false;
        }
    }

    //自己实现简单的string类型的list示例
    public class ConcreateAggregate : Aggregate
    {
        private string[] array;
        private int count;

        ConcreateIterator concreateIterator;

        public ConcreateAggregate()
        {
            count = 0;
            array = new string[1];
        }

        public Iterator GetIterator()
        {
            if (concreateIterator == null)
            {
                concreateIterator = new ConcreateIterator(this);
            }
            return concreateIterator;
        }

        public int Count
        {
            get
            {
                return count;
            }
        }

        public void Add(string element)
        {
            if (count == array.Length)
            {
                string[] newArray = new string[count * 2];
                array.CopyTo(newArray, 0);
                array = newArray;
            }
            array[count] = element;
            count += 1;
        }

        public string Get(int index)
        {
            return array[index];
        }
    }

    public class Client
    {
        static public void Main()
        {
            ConcreateAggregate list = new ConcreateAggregate();
            list.Add("N");
            list.Add("R");
            list.Add("a");
            list.Add("t");
            list.Add("e");
            list.Add("l");

            Iterator iterator = list.GetIterator();
            do
            {
                Debug.Log(iterator.Current());
            }
            while (iterator.Next());
        }
    }
}
