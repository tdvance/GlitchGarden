using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour, IObserverSubject
{

    int _score;


    public int score
    {
        get
        {
            return _score;
        }
    }

    public void addPoints(int howmany)
    {
        _score += howmany;
        Notify("addPoints");
    }

    // Use this for initialization
    void Start () {
        Reset();
	}
	
    public void Reset()
    {
        _score = 0;
    }

	// Update is called once per frame
	void Update () {
	
	}

    #region Observer

    private HashSet<IObserver> observers = new HashSet<IObserver>();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
        Debug.Log("Observer " + observer + "attached to " + name);
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
        Debug.Log("Observer " + observer + "detached from " + name);
    }

    public void Notify(object message)
    {
        foreach(IObserver observer in observers)
        {
            observer.UpdateObserver(message);
        }
    }
    #endregion

    #region Singleton
    private static ScoreManager _instance;

    public static ScoreManager instance
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
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            throw new System.Exception("Duplicate singleton instantiated");
        }
    }

    #endregion
}
