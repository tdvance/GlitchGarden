using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour {


   public void Submit() {
        Debug.Log("Button " + name + " pressed");
        if (LevelManager.instance) {
            LevelManager.instance.ReturnFromOptions();
        } else {
            Debug.LogWarning("Missing singleton: LevelManager");
        }
    }

}
