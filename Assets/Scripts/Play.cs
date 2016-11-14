using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {


   public void Submit() {
        Debug.Log("Button " + name + " pressed");
        if (LevelManager.instance) {
            LevelManager.instance.StartGame();
        } else {
            Debug.LogWarning("Missing singleton: LevelManager");
        }
    }

}
