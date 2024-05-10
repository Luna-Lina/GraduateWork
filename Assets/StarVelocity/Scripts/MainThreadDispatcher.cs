using System;
using System.Collections.Concurrent;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
    //private static MainThreadDispatcher _instance;

    //public static MainThreadDispatcher Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = new GameObject("MainThreadDispatcher").AddComponent<MainThreadDispatcher>();
    //            DontDestroyOnLoad(_instance.gameObject);
    //        }

    //        return _instance;
    //    }
    //}

    //private void Awake()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void Update()
    {
        while (!_actions.IsEmpty)
        {
            if (_actions.TryDequeue(out Action action))
            {
                action();
            }
            else
            {
                break;
            }
        }
    }

    public void Dispatch(Action action)
    {
        _actions.Enqueue(action);
    }
}
