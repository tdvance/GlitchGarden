using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static bool inOptionsSceneDuringGame = false;

    public string[] mainCycle;
    public string gameScene;
    public string optionsScene;


    public float initialDelay = 2f;
    public float cycleDelay = 10f;
    public float startGameDelay = 0.5f;

    public bool startOnAwake = true;

    int cycleIndex = 0;
    string currentLevelName = "_Init";
    bool onMainCycle = false;

    void Start() {
        if (startOnAwake) {
            StartMainCycle();
        }
    }

    public void StartGame() {
        FlexibleMusicManager.instance.Pause();
        LoadLevel(gameScene, startGameDelay);
    }

    public void StartMainCycle(float delay) {
        Invoke("StartMainCycle", delay);
    }

    public void StartMainCycle() {
        CancelInvoke();
        cycleIndex = -1;
        InvokeRepeating("NextInCycle", initialDelay, cycleDelay);
        onMainCycle = true;
    }

    public void StartMainCycleImmediately() {
        CancelInvoke();
        cycleIndex = -1;
        InvokeRepeating("NextInCycle", 0f, cycleDelay);
        onMainCycle = true;
    }

    public void StopMainCycle() {
        CancelInvoke();
        onMainCycle = false;
    }

    public void ResumeMainCycle() {
        CancelInvoke();
        cycleIndex--;
        InvokeRepeating("NextInCycle", 0, cycleDelay);
        onMainCycle = true;
    }

    public void LoadLevel(string name, float delay = 0f) {
        StopMainCycle();
        currentLevelName = name;
        Invoke("LoadSpecifiedLevel", delay);
    }

    public void Quit() {
        Application.Quit();
    }

    string saveOptionsSceneReturn;
    bool saveOptionsMainCycle;
    int saveOptionsCycleIndex;
    public void OptionsMenu() {
        inOptionsSceneDuringGame = false;
        saveOptionsSceneReturn = currentLevelName;
        saveOptionsMainCycle = onMainCycle;
        saveOptionsCycleIndex = cycleIndex;
        LoadLevel(optionsScene);
    }

    public static float saveTimeScale;
    public void OptionsMenuInGame() {
        inOptionsSceneDuringGame = true;
        saveTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        SceneManager.LoadScene("OptionsInGame", LoadSceneMode.Additive);
        Destroy(GameObject.Find("OptionsCamera"));
    }


    public void ReturnFromOptions() {
        if (inOptionsSceneDuringGame) {
            inOptionsSceneDuringGame = false;
            SceneManager.UnloadScene("OptionsInGame");
            Time.timeScale = saveTimeScale;
            return;
        }
        if (saveOptionsMainCycle) {
            cycleIndex = saveOptionsCycleIndex;
            ResumeMainCycle();
        } else {
            LoadLevel(saveOptionsSceneReturn);
        }

    }

    void NextInCycle() {
        if (FlexibleMusicManager.instance.CurrentTrackNumber() != 1) {
            FlexibleMusicManager.instance.Pause();
            FlexibleMusicManager.instance.SetCurrentTrack(1);
            FlexibleMusicManager.instance.repeat = true;
            FlexibleMusicManager.instance.Play();
        }
        cycleIndex++;
        if (cycleIndex >= mainCycle.Length) {
            cycleIndex = 0;
        }
        LoadNamedLevel(mainCycle[cycleIndex]);
    }

    void LoadNamedLevel(string name) {
        currentLevelName = name;
        LoadSpecifiedLevel();
    }

    void LoadSpecifiedLevel() {
        Debug.Log("Loading scene: " + currentLevelName);
        SceneManager.LoadScene(currentLevelName);
    }

    
    #region Singleton
    private static LevelManager _instance;

    public static LevelManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<LevelManager>();
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
