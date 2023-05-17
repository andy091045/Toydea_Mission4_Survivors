using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HD.Math
{
    class Math
    {
        //Add function
        public int Add(int number1, int number2)
        {
            return number1 + number2;
        }

        public float Add(float number1, float number2)
        {
            return number1 + number2;
        }
    }
}

//useing Pool reference: https://github.com/andy091045/poolTest
namespace HD.Pooling
{
    //Abstract pooling
    public interface IPool<T>
    {
        T GetInstance();
    }

    //ListPool
    class ListPool<T> : IPool<T> where T : class
    {
        Func<T> produce;
        int capacity;
        List<T> objects;
        Func<T, bool> useTest;
        bool expandable;

        public ListPool(Func<T> factoryMethod, int maxSize, Func<T, bool> inUse, bool expandable = true)
        {
            produce = factoryMethod;
            capacity = maxSize;
            objects = new List<T>(maxSize);
            useTest = inUse;
            this.expandable = expandable;
        }

        public T GetInstance()
        {
            var count = objects.Count;
            foreach (var item in objects)
            {
                if (!useTest(item))
                {
                    return item;
                }
            }
            if (count >= capacity && !expandable)
            {
                return null;
            }
            var obj = produce();
            objects.Add(obj);
            return obj;
        }
    }

    //QueuePool
    class QueuePool<T> : IPool<T>
    {
        Func<T> produce;
        int capacity;
        T[] objects;
        int index;

        public QueuePool(Func<T> factoryMethod, int maxSize)
        {
            produce = factoryMethod;
            capacity = maxSize;
            index = -1;
            objects = new T[maxSize];
        }

        public T GetInstance()
        {
            //stuff
            index = (index + 1) % capacity;

            if (objects[index] == null)
            {
                objects[index] = produce();
            }

            return objects[index];
        }
    }
}

namespace HD.Singleton
{
    public class TSingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour
    {

        public static T Instance => GetInstance();
        private static T instance = null;

        private static T GetInstance()
        {
            if (instance == null)
            {
                var type = typeof(T);
                var gameObject = new GameObject(type.Name);
                instance = gameObject.AddComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            return instance;
        }

        void Awake()
        {
            if (instance == null) instance = this as T;
            if (instance == this) DontDestroyOnLoad(this);
            else
            {
                DestroyImmediate(gameObject);
                return;
            }
            init();
        }

        protected virtual void init()
        {
        }

        //protected virtual void OnDestroy() => instance = null;
    }
}

namespace HD.FindObject
{
    public class Find
    {
        //前面放入要找的物件名字，後面放入要指到物件底下類別的已宣告類別
        public void FindObject<T>(string name, out T component) where T : Component
        {
            GameObject target = GameObject.Find(name);
            if (target != null)
            {
                component = target.GetComponent<T>();
            }
            else
            {
                Debug.LogError($"Can't find {name}");
                component = null;
            }
        }
    }
}

namespace HD.FrameworkDesign
{
    public class Event<T> where T : Event<T>
    {
        private static Action mOnEvent;

        public static void Register(Action onEvent)
        {
            mOnEvent += onEvent;
        }

        public static void UnRegister(Action onEvent)
        {
            mOnEvent -= onEvent;
        }

        public static void Trigger()
        {
            mOnEvent?.Invoke();
        }
    }

    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T mValue = default(T);

        public T Value
        {
            get { return mValue; }
            set
            {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
        public Action<T> OnValueChanged;
    }

}
