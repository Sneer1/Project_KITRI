using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    static bool bShutdown = false;
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if (bShutdown == false)
                {
                    T newinstance = GameObject.FindObjectOfType<T>() as T;

                    if (newinstance == null)
                    {
                        newinstance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }

                    InstanceInit(newinstance);
                    Debug.Assert(_instance != null, typeof(T).ToString() + "싱글턴 생성 실패");
                }
            }

            return _instance;
        }
    }

    private static void InstanceInit(Object inst)
    {
        _instance = inst as T;
        _instance.Init();
    }

    public virtual void Init()
    {
        DontDestroyOnLoad(_instance);
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    private void OnApplicationQuit()
    {
        _instance = null;
        bShutdown = true;
    }
}
