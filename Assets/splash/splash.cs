using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class splash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Splash");
        Invoke("LoadStartScene", 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void LoadStartScene()
    {
        SceneManager.LoadScene("Start");
    }
}
