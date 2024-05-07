using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create a menu to select from in
[CreateAssetMenu(menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    //Make a list of listeners for Game Events
    public List<GameEventListener> listeners = new List<GameEventListener>();

    //Call "raise" - check through all enabled listeners, and allow the UnityEvent response (see SCR_GameEventListeners)
    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised();
        }
    }

    //If the object is not yet on the GameEventListener script list, add it to become a 'listening' object
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    //If the object is on the GameEventListener script list, remove (prevent double ups)
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}