using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class OptionsManager : MonoBehaviour {

    string _name;
    float _volume;
    float _difficulty;
    


    public string player_name
    {
        get
        {
            return _name;
        }

        set
        {
            _name = value;
            PlayerPrefs.SetString("name", value);
        }
    }

    public float volume
    {
        get
        {
            return _volume;
        }

        set
        {
            _volume = value;
            MusicManager.instance.SetVolume(value);
            PlayerPrefs.SetFloat("volume", value);
        }
    }

    public float difficulty
    {
        get
        {
            return _difficulty;
        }

        set
        {
            _difficulty = value;
            PlayerPrefs.SetFloat("difficulty", value);
        }
    }

    public string defaultName = Environment.UserName;
    public float defaultVolume = 0.8f;
    public float defaultDifficulty = 2f;

    public void Reset()
    {
        player_name = defaultName;
        volume = defaultVolume;
        difficulty = defaultDifficulty;
    }

    // Use this for initialization
    void Start () {
        //set values of options from saved; 
        if (PlayerPrefs.HasKey("name"))
        {
            _name = PlayerPrefs.GetString("name");
        }else
        {
            name = defaultName;//if no saved value, use default and save it.
        }

        if (PlayerPrefs.HasKey("volume"))
        {
            _volume = PlayerPrefs.GetFloat("volume");
        }else
        {
            volume = defaultVolume;//if no saved value, use default and save it.
        }

        if (PlayerPrefs.HasKey("difficulty"))
        {
            _difficulty = PlayerPrefs.GetFloat("difficulty");
        }
        else
        {
            difficulty = defaultDifficulty;//if no saved value, use default and save it.
        }

    }

    // Update is called once per frame
    void Update () {
	
	}

    #region Singleton
    private static OptionsManager _instance;

    public static OptionsManager instance
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
