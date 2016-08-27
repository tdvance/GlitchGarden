using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour, IObserverSubject
{
    private Music _currentMusic;
    private AudioSource audioSource;

    private Music nextMusic;
    private float startVolume;

    [Tooltip("Seconds to fade out before new music, 0 is no fade")]
    public float durationForFading = 1f;

    public Music currentMusic
    {
        get
        {
            return _currentMusic;
        }
    }

    void AfterAwake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            throw new System.Exception("No audiosource component found.");

        }
    }

    private bool fadeEvent(float level)
    {
        audioSource.volume = level * startVolume;
        return true;
    }

    private bool endFadeEvent()
    {
        float v = startVolume;
        playNewMusic(nextMusic);
        audioSource.volume = v;
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Start()
    {
        SetVolume(OptionsManager.instance.volume);

    }

    public void ChangeMusic(Music music)
    {
        Debug.Log("Change music requested from " + _currentMusic + " to " + music);
        if (!music.clip)
        {
            return;
        }
        if (_currentMusic != null && _currentMusic.clip.name == music.clip.name && !music.restartWithLevel && music.loops)
        {
            return;
        }
        if (durationForFading > 0)
        {
            fadeToNewMusic(music, durationForFading);
        }
        else
        {
            playNewMusic(music);
        }
    }

    private void playNewMusic(Music music)
    {
        _currentMusic = music;
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); //TODO fade
            NotifyEnd();
        }
        audioSource.clip = music.clip;
        audioSource.loop = music.loops;
        audioSource.Play();
        Debug.Log("Change music to " + music);
        NotifyStart();
        CancelInvoke("NotifyLoop");
        if (music.loops)
        {
            Invoke("NotifyLoop", music.clip.length);
        }
    }


    private void fadeToNewMusic(Music music, float duration)
    {
        if (!audioSource.isPlaying)
        {
            playNewMusic(music);
        }
        else
        {
            nextMusic = music;
            startVolume = audioSource.volume;
            TimedEventManager.instance.createEvent(duration, fadeEvent, endFadeEvent);
        }
    }

    private void NotifyStart()
    {
        Notify("Start");
        CancelInvoke("NotifyLoop");
    }
    private void NotifyLoop()
    {
        if (audioSource.isPlaying)
        {
            Notify("Loop");
            Invoke("NotifyLoop", audioSource.clip.length);
        }
    }

    private void NotifyEnd()
    {
        Notify("End");
        CancelInvoke("NotifyLoop");
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
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
        foreach (IObserver observer in observers)
        {
            observer.UpdateObserver(message);
        }
    }
    #endregion

    #region Singleton
    private static MusicManager _instance;

    public static MusicManager instance
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
