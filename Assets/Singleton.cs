using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance {
        get { return instance; }
    }

    public static bool IsInitialized {
        get { return instance != null; }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = (T)this;
        } else {
            Debug.LogError("[" + typeof(T).FullName + "]: Trying to instantiate multiple Singletons.");
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }
}
