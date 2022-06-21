using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

public class ExecuteOnMainThread : MonoBehaviour
{

    public static readonly ConcurrentQueue<Action> RunOnMainThread = new ConcurrentQueue<Action>();

    void Update()
    {
        if (!RunOnMainThread.IsEmpty)
        {
            while (RunOnMainThread.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }
    }
}