using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static T m_instance = null;
    public static T Instance
    {
        get
        {
            if (Singleton<T>.m_instance == null)
            {
                Singleton<T>.m_instance = new T();
            }

            return m_instance;
        }
    }
}
