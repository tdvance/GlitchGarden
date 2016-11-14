using UnityEngine;
using System.Collections;

public class Options : MonoBehaviour {


    public void Submit() {
        Debug.Log("Button " + name + " pressed");
        if (LevelManager.instance) {
            LevelManager.instance.OptionsMenu();
        } else {
            Debug.LogWarning("Missing singleton: LevelManager");
        }
    }

    public void SubmitFromGame() {
        Debug.Log("Button " + name + " pressed while in game");
        if (LevelManager.instance) {
            LevelManager.instance.OptionsMenuInGame();
        } else {
            Debug.LogWarning("Missing singleton: LevelManager");
        }
    }

}
