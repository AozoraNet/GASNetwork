using System;
using UnityEngine;
using System.Reflection;

namespace GAS.Demo
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SingletonAttribute : Attribute
    {
        public bool DontDestroyOnLoad { get; }

        public SingletonAttribute(bool dontDestroyOnLoad)
        {
            DontDestroyOnLoad = dontDestroyOnLoad;
        }
    }

    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateSingleton<T>();
                }

                return instance;
            }
        }

        public static T CreateSingleton<T>() where T : Singleton<T>
        {
            T retInstance = FindObjectOfType<T>();
            if (retInstance == null)
            {
                return null;
                //retInstance = new GameObject(typeof(T).Name.ToString()).AddComponent<T>();
            }

            MemberInfo mi = typeof(T);
            var attributes = mi.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                if (attribute is SingletonAttribute attr)
                {
                    if (attr.DontDestroyOnLoad)
                    {
                        DontDestroyOnLoad(retInstance);
                    }
                }
            }

            return retInstance;
        }
    }
}