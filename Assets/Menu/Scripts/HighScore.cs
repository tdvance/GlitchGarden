using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : SceneScript {
    public Text HighScoreText;

    // Use this for initialization
    override public void AfterStart () {
        HighScoreText.text = HighScores.instance.GetHighScoreText();
	}
	
	// Update is called once per frame
	override public void AfterUpdate () {
	
	}
}
