using System.Collections;
using System.Collections.Generic;
using Bases;
using UnityEngine;

public class Singleton<T> : BaseMonoBehaviour where T : BaseMonoBehaviour {

    private static T _instance = null;
    private static object l = new object();
    public static T instance {
        get {
            if (applicationIsQuitting) {
                return null;
            }
            lock (l) {
                if (_instance == null) {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof(T)).Length > 1) {
                        return _instance;
                    }
                    if (_instance == null) {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T);
                        DontDestroyOnLoad(singleton);
                    } 
                }
                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    new public void OnDestroy() {
        //base.OnDestroy ();
        applicationIsQuitting = true;
    }
}
