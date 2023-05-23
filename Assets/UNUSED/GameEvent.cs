using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Game Event", menuName = "Events/EventObject", order = 1)]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners;

    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void Raise()
    {
        foreach (GameEventListener listener in listeners)
            listener.Invoke();
    }
}
