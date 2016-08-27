using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Assets.Menu.Scripts;


public class Game : SceneScript {

    public Text score;

	override public void AfterStart () {
        ScoreManager.instance.Reset();
	}

    override public void AfterUpdate () {
        score.text = "Score:" + string.Format("{0,10}", ScoreManager.instance.score);
	}

    public void GameOver()
    {
        HighScores.instance.saveHighScores();
    }

    override public void BeforeLeavingScene(NavType which)
    {
        switch (which)
        {
            case NavType.Title:
            case NavType.Win:
            case NavType.Lose:
                GameOver();
                break;
            default:
                break;
        }
    }
}
