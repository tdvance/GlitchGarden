using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Quit : MonoBehaviour {


    public void Submit() {
        Debug.Log("Button " + name + " pressed");
        if (LevelManager.instance) {
            if (LevelManager.inOptionsSceneDuringGame) {
                LevelManager.inOptionsSceneDuringGame = false;
                SceneManager.UnloadScene("OptionsInGame");
                Time.timeScale = LevelManager.saveTimeScale;
                ScoreManager.instance.SubmitScore(ScoreDisplay.instance.score);
                LevelManager.instance.StartMainCycleImmediately();
            } else {
                LevelManager.instance.Quit();
            }
        } else {
            Debug.LogWarning("Missing singleton: LevelManager");
        }
    }

}
