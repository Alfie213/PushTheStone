using System;
using System.Collections.Generic;

public class CustomAction<T, U>
{
    private readonly List<Action<T, U>> callbacks = new List<Action<T, U>>();
    
    public void Subscribe(Action<T, U> callback)
    {
        callbacks.Add(callback);
    }

    public void Unsubscribe(Action<T, U> callback)
    {
        callbacks.Remove(callback);
    }

    public void Publish(T arg1, U arg2)
    {
        foreach (Action<T, U> callback in callbacks)
        {
            callback.Invoke(arg1, arg2);
        }
    }
}