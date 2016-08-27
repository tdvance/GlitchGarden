using System;
using System.Collections.Generic;
using UnityEngine;
/*
 * For effects that occur over time.
 * 
 * Access this through the TimedEventManager singleton.
 */
public class TimedEvent
{
    private float duration;
    private float timeToLive;
    private Func<float, bool> doEvent;
    private Func<bool> endEvent;

    public TimedEvent(float duration, Func<float, bool> doEvent, Func<bool> endEvent)
    {
        this.doEvent = doEvent;
        this.endEvent = endEvent;
        this.duration = duration;
        timeToLive = duration;
    }

    public float getTimeRemaining()
    {
        return timeToLive;
    }

    public bool update(float deltaTime)
    {
        if (timeToLive > 0)
        {
            timeToLive -= deltaTime;
            if (timeToLive <= 0)
            {
                timeToLive = 0;
                bool result = endEvent();
                return result;
            }
            else
            {
                return doEvent((1.0f - duration + timeToLive) / duration);
            }
        }
        return false;
    }


}