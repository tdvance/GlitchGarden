using UnityEngine;
using System.Collections;
using Assets;
using System;
using UnityEngine.SceneManagement;
using Assets.Menu.Scripts;

public class SceneScript : MonoBehaviour, ISceneScript {


    [Tooltip("If true, leave script up instead of going to next screen")]
    public bool debugMode = false;

    [Tooltip("Music for this level")]
    public Music music;

    [Tooltip("Seconds before automatically loading next level; negative means infinite")]
    public float duration = 10.0f;

    [Tooltip("This scene (default is from script name)")]
    public string thisScene = "<Unknown>";

    [Tooltip("Next scene (for autoload and/or Next button) (default is none)")]
    public string nextScene = "None";

    [Tooltip("Previous scene (for back button) (default is scene that loaded this one)")]
    public string previousScene = "Back";

    [Tooltip("Options scene (for options button) (default is scene called 'Options')")]
    public string optionsScene = "Options";

    [Tooltip("Game scene (for play game button) (default is scene called 'Level001')")]
    public string gameScene = "Level001";

    [Tooltip("Title scene (for leave game button) (default is scene called 'Title')")]
    public string titleScene = "Title";

    [Tooltip("Win scene (for win condition) (default is scene called 'Win')")]
    public string winScene = "Win";

    [Tooltip("Lose scene (for lose condition) (default is scene called 'Lose')")]
    public string loseScene = "Lose";
    

    public Music GetMusic()
    {
        return music;
    }

    public LevelManager.SceneInfo GetScene()
    {   
        return LevelManager.instance.FindSceneByName(thisScene);
    }

    public SceneType GetSceneType()
    {
        return SceneType.Init;
    }

    public float GetDuration()
    {
        return duration;
    }

    virtual public void AfterStart()
    {

    }

    virtual public void AfterUpdate()
    {

    }

    

    // Use this for initialization
    void Start()
    {
        
        if (thisScene.Length==0)
        {
            thisScene = name;
        }
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(thisScene);
        thisScene = scene.name;
        if (scene.buildIndex < 0)
        {
            scene = LevelManager.instance.FindSceneByName(name);
            thisScene = scene.name;
        }
        if (thisScene.EndsWith(".unity"))
        {
            thisScene = thisScene.Substring(0, thisScene.Length - 6);
        }
        if (thisScene.EndsWith("Script"))
        {
            thisScene = thisScene.Substring(0, thisScene.Length - 6);
        }

        Debug.Log("Scene started: " + thisScene);
        MusicManager.instance.ChangeMusic(music);
        if (!debugMode && duration >= 0)
        {
            Invoke("LoadNextScene", duration);
        }
        AfterStart();
    }
	
	// Update is called once per frame
	void Update () {
        AfterUpdate();
	}

    void Awake()
    {
        //this ~Bootstrap business is my solution to the problem of loading singletons regardless of which scene one presses "play" on when testing.
        if (!LevelManager.isLoaded)
        {
            SceneManager.LoadScene("~BootStrap", LoadSceneMode.Additive);
            SceneManager.UnloadScene("~BootStrap");
            Debug.Log("Using ~Bootstrap to load singletons");
        }else
        {
            Debug.Log("Singletons already loaded");

        }
    }


    virtual public void BeforeLeavingScene(NavType which)
    {
    }


    public void LoadNextScene()
    {
        BeforeLeavingScene(NavType.Next);
        LevelManager.SceneInfo scene = GetNext();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }else
        {
            Debug.Log("No next scene found");
        }
    }

    public void LoadPreviousScene()
    {
        BeforeLeavingScene(NavType.Previous);
        LevelManager.SceneInfo scene = GetPrevious();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }else
        {
            LevelManager.instance.ChangeScene(LevelManager.instance.GetPreviousScene());
        }

    }

    public void LoadOptionsScene()
    {
        BeforeLeavingScene(NavType.Options);
        LevelManager.SceneInfo scene = GetOptionsScene();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }
        else
        {
            scene = LevelManager.instance.FindSceneByName(name);
            if (scene.buildIndex >= 0)
            {
                LevelManager.instance.ChangeScene(scene);
            }else
            {
                Debug.Log("No options scene found");
            }
        }

    }


    public void LoadGameScene()
    {
        BeforeLeavingScene(NavType.Play);
        LevelManager.SceneInfo scene = GetGameScene();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }
        else
        {
            scene = LevelManager.instance.FindSceneByName(name);
            if (scene.buildIndex >= 0)
            {
                LevelManager.instance.ChangeScene(scene);
            }
            else
            {
                Debug.Log("No game scene found");
            }
        }
    }

    public void LoadTitleScene()
    {
        BeforeLeavingScene(NavType.Title);
        LevelManager.SceneInfo scene = GetTitleScene();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }
        else
        {
            scene = LevelManager.instance.FindSceneByName(name);
            if (scene.buildIndex >= 0)
            {
                LevelManager.instance.ChangeScene(scene);
            }
            else
            {
                Debug.Log("No title scene found");
            }
        }
    }

    public void LoadWinScene()
    {
        BeforeLeavingScene(NavType.Win);
        LevelManager.SceneInfo scene = GetWinScene();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }
        else
        {
            scene = LevelManager.instance.FindSceneByName(name);
            if (scene.buildIndex >= 0)
            {
                LevelManager.instance.ChangeScene(scene);
            }
            else
            {
                Debug.Log("No win scene found");
            }
        }
    }

    public void LoadLoseScene()
    {
        BeforeLeavingScene(NavType.Lose);
        LevelManager.SceneInfo scene = GetLoseScene();
        if (scene.buildIndex >= 0)
        {
            LevelManager.instance.ChangeScene(scene);
        }
        else
        {
            scene = LevelManager.instance.FindSceneByName(name);
            if (scene.buildIndex >= 0)
            {
                LevelManager.instance.ChangeScene(scene);
            }
            else
            {
                Debug.Log("No lose scene found");
            }
        }
    }

    LevelManager.SceneInfo GetGameScene()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(gameScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + gameScene);
        }
        return scene;
    }

    LevelManager.SceneInfo GetTitleScene()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(titleScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + titleScene);
        }
        return scene;
    }

    LevelManager.SceneInfo GetWinScene()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(winScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + winScene);
        }
        return scene;
    }

    LevelManager.SceneInfo GetLoseScene()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(loseScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + loseScene);
        }
        return scene;
    }



    LevelManager.SceneInfo GetNext()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(nextScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + nextScene);
        }
        return scene;
    }

    LevelManager.SceneInfo GetPrevious()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(previousScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + previousScene);
        }
        return scene;
    }

    LevelManager.SceneInfo GetOptionsScene()
    {
        LevelManager.SceneInfo scene = LevelManager.instance.FindSceneByName(optionsScene);
        if (scene.buildIndex < 0)
        {
            Debug.Log("No scene found with name " + optionsScene);
        }
        return scene;
    }

    public override string ToString()
    {
        return name;
    }
}
