using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    public class SceneInfo
    {
        private string _name;
        private int _index;

        public SceneInfo(string name, int index)
        {
            _name = name;
            _index = index;
        }

        public string name
        {
            get
            {
                return _name;
            }
        }

        public int buildIndex
        {
            get
            {
                return _index;
            }
        }

        public override string ToString()
        {
            return "SceneInfo(name=" + name + ", index=" + buildIndex;
        }
    }

    public static bool isLoaded
    {
        get
        {
            return _loaded;
        }
    }

    private SceneInfo previousScene;
    private SceneInfo currentScene;
    private SceneInfo[] scenes;
    private static bool _loaded = false;


    public SceneInfo GetPreviousScene()
    {
        return previousScene;
    }

    public SceneInfo GetCurrentScene()
    {
        return currentScene;
    }

    public void ChangeScene(SceneInfo scene)
    {
        Notify("ChangeScene_"+scene.name);
        previousScene = currentScene;
        currentScene = scene;
        SceneManager.LoadScene(scene.buildIndex);
    }


    public void ChangeScene(Scene scene)
    {
        string n = scene.name;
        if (n.EndsWith(".unity"))
        {
            n = n.Substring(0, n.Length - 6);
        }
        Notify("ChangeScene_"+scene.name);
        if (scene.buildIndex >= 0)
        {
            ChangeScene(scenes[scene.buildIndex]);
        }
        else
        {
            Debug.Log("Invalid scene");
        }
    }

    public SceneInfo FindSceneByName(string sceneName)
    {
        int best = -1;
        int nextbest = -1;
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].name.Equals(sceneName + ".unity"))
            {
                return scenes[i];
            }
            if (scenes[i].name.EndsWith(sceneName + ".unity"))
            {
                if (nextbest < 0)
                {
                    nextbest = i;
                }
            }
            if (scenes[i].name.EndsWith("/" + sceneName + ".unity"))
            {
                if (best < 0)
                {
                    best = i;
                }
            }
        }
        if (best >= 0)
        {
            return scenes[best];
        }
        if (nextbest >= 0)
        {
            return scenes[nextbest];
        }
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i].name.Equals(sceneName + "Script.unity"))
            {
                return scenes[i];
            }
            if (scenes[i].name.EndsWith(sceneName + "Script.unity"))
            {
                if (nextbest < 0)
                {
                    nextbest = i;
                }
            }
            if (scenes[i].name.EndsWith("/" + sceneName + "Script.unity"))
            {
                if (best < 0)
                {
                    best = i;
                }
            }
        }
        if (best >= 0)
        {
            return scenes[best];
        }
        if (nextbest >= 0)
        {
            return scenes[nextbest];
        }

        return new SceneInfo(sceneName, -1);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void AfterAwake()
    {
        scenes = new SceneInfo[UnityEditor.EditorBuildSettings.scenes.Length];
        for (int i = 0; i < UnityEditor.EditorBuildSettings.scenes.Length; i++)
        {
            scenes[i] = new SceneInfo(UnityEditor.EditorBuildSettings.scenes[i].path, i);
        }
        currentScene = scenes[0];
    }

    #region Singleton
    private static LevelManager _instance;

    public static LevelManager instance
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
            _loaded = true;
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

}
