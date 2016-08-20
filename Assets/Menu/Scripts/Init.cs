using UnityEngine;
using System.Collections;



public class Init : MonoBehaviour {

    public string titleScreen = "TitleScreen";


    // Use this for initialization
    void Start () {
        Invoke("LoadTitleScreen", 2.7f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadTitleScreen()
    {
        LevelManager.instance.LoadLevel(titleScreen);

    }
}
