using UnityEngine;
using System.Collections;

public class GameOptions : MonoBehaviour {
    public float sfxVolume = 0.5f;
    public float musicVolume = 0.25f;


    string musicKey = "Music Volume";
    string sfxKey = "SFX Volume";

    public void SetMusicVolume(float value) {
        musicVolume = value;
        PlayerPrefs.SetFloat(musicKey, value);
        FlexibleMusicManager.instance.volume = value;
    }

    public void SetSFXVolume(float value) {
        sfxVolume = value;
        PlayerPrefs.SetFloat(sfxKey, value);
    }


    // Use this for initialization
    void Start() {
        if (PlayerPrefs.HasKey(musicKey)) {
            musicVolume = PlayerPrefs.GetFloat(musicKey);
        }
        FlexibleMusicManager.instance.volume = musicVolume;
        if (PlayerPrefs.HasKey(sfxKey)) {
            sfxVolume = PlayerPrefs.GetFloat(sfxKey);
        }
    }



    #region Singleton
    private static GameOptions _instance;

    public static GameOptions instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<GameOptions>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance != null && _instance != this) {
            Debug.LogError("Duplicate singleton " + this.gameObject + " created; destroying it now");
            Destroy(this.gameObject);
        }

        if (_instance != this) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    #endregion

}
