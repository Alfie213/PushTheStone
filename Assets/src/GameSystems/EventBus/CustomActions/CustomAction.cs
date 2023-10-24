using System;
using System.Collections.Generic;

public class CustomAction
{
    private readonly List<Action> callbacks = new List<Action>();

    public void Subscribe(Action callback)
    {
        callbacks.Add(callback);
    }

    public void Unsubscribe(Action callback)
    {
        callbacks.Remove(callback);
    }

    public void Publish()
    {
        foreach (Action callback in callbacks)
        {
            callback.Invoke();
        }
    }
}