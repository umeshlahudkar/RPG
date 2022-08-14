using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic singleton class
public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
        } 
        else
        {
            Destroy(this);
        }
    }
}
