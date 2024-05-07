using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    //Allow access to Event script
    public GameEvent gameEvent;

    //Attach items to "act" when raised
    public UnityEvent response;

    //When enabled/disabled, register/disable the object as listener (from GameEvent script function)
    void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }

    void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }

    //Call Unity to invoke the set items in inspector when event is broadcasted or "raised"
    public void OnEventRaised()
    {
        response.Invoke();
    }
}