using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;


/*
 * For events that occur over time, like fade transitions.  Use the createEvent method to create a new one,
 * passing how long the event should take in seconds, a function that accepts a float from 0.0f to 1.0f that is called by 
 * this manager with values starting with a little less than one to a little more than 0 in decreasing order.  The user 
 * writes the code saying what to do at each level (such as adjust volume or image alpha or whatever to fade).  Then when
 * the duration has expired, the third argument, the endEvent method, is called.
 * 
 * Currently the boolean return values from doEvent and endEvent are unused.
 */

public class TimedEventManager : MonoBehaviour
{

    private static HashSet<TimedEvent> allEvents = new HashSet<TimedEvent>();

    private static HashSet<TimedEvent> toRemove = new HashSet<TimedEvent>();


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (TimedEvent e in toRemove)
        {
            allEvents.Remove(e);
        }
        toRemove.Clear();

        foreach (TimedEvent e in allEvents)
        {
            e.update(Time.deltaTime);
            if (e.getTimeRemaining() <= 0)
            {
                removeEvent(e);
            }
        }
    }

    void AfterAwake()
    {
    }

    public void addEvent(TimedEvent e)
    {
        allEvents.Add(e);
    }

    public void removeEvent(TimedEvent e)
    {
        toRemove.Add(e);
    }

    public void createEvent(float duration, Func<float, bool> doEvent, Func<bool> endEvent)
    {
        TimedEvent e = new TimedEvent(duration, doEvent, endEvent);
        addEvent(e);
    }

    #region Singleton
    private static TimedEventManager _instance;

    public static TimedEventManager instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Neither ~Bootstrap nor ~Init has been loaded");

            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Singleton " + this.GetType() + " instantiated");
            AfterAwake();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            throw new System.Exception("Duplicate singleton instantiated");
        }
    }
    #endregion
}
