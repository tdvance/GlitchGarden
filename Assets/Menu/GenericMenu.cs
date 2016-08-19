using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GenericMenu : MonoBehaviour {
    public float screenDuration = 10f;
    public String nextScreen = "BlueScreen";
    public String gameScreen = "BlueScreen";

    public Text title;
    public Text subtitle;

    // Use this for initialization
    protected void Start () {
        if (screenDuration > 0)
        {
            Invoke("NextScreen", screenDuration);
        }
	}
	
	// Update is called once per frame
	void Update () {
        checkKeyPresses();
        checkButtonClicks();
	}

    private void checkKeyPresses()
    {
        if (Input.anyKeyDown)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        Debug.Log("Start Game: " + gameScreen);
        SceneManager.LoadScene(gameScreen);

    }

    private void checkButtonClicks()
    {
        if (Input.GetMouseButton(0))
        {
            StartGame();
        }
    }

    void NextScreen()
    {
        Debug.Log("Next Screen: " + nextScreen);
        SceneManager.LoadScene(nextScreen);
    }
}
