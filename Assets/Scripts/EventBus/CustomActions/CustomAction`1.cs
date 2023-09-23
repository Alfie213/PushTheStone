using System;
using System.Collections.Generic;

public class CustomAction<T>
{
    private readonly List<Action<T>> callbacks = new List<Action<T>>();
    
    public void Subscribe(Action<T> callback)
    {
        callbacks.Add(callback);
    }

    public void Unsubscribe(Action<T> callback)
    {
        callbacks.Remove(callback);
    }

    public void Publish(T arg)
    {
        foreach (Action<T> callback in callbacks)
        {
            callback.Invoke(arg);
        }
    }
}