using UnityEngine;
using System.Collections;
using System;

public class HighScores : MonoBehaviour
{
    private HighScore[] highScores = new HighScore[10];

    [Serializable]
    public class HighScore : IComparable
    {
        public string name;
        public int score;

        public int CompareTo(object obj)
        {
            HighScore h = obj as HighScore;
            return score.CompareTo(h.score);
        }

    };

    void AfterAwake()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = new HighScore();
        }
        ClearHighScores();
        loadHighScores();
    }

    public string GetHighScoreText()
    {
        string result = "";

        for (int i = 0; i < highScores.Length; i++)
        {
            result += string.Format("{0,5}", i + 1) + ".  " + string.Format("{0,10}", highScores[i].score)
                + "   " + highScores[i].name + "\n";
        }
        return result;
    }

    public void ClearHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i].score = 0;
            highScores[i].name = "Nobody";
        }
        saveHighScores();
    }

    public void saveHighScores()
    {
        mergeHighScore(OptionsManager.instance.player_name, ScoreManager.instance.score);
        for (int i = 0; i < highScores.Length; i++)
        {
            PlayerPrefs.SetString("high_score_name_" + i, highScores[i].name);
            PlayerPrefs.SetInt("high_score_score_" + i, highScores[i].score);
        }
    }

    public void loadHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            if (PlayerPrefs.HasKey("high_score_name_" + i) && PlayerPrefs.HasKey("high_score_score_" + i))
            {
                string n = PlayerPrefs.GetString("high_score_name_" + i);
                int s = PlayerPrefs.GetInt("high_score_score_" + i);
                mergeHighScore(n, s);
            }

        }
    }

    private void mergeHighScore(string name, int score)
    {
        if (highScores[highScores.Length - 1].score < score)
        {
            highScores[highScores.Length - 1].name = name;
            highScores[highScores.Length - 1].score = score;
            for (int i = highScores.Length - 2; i >= 0; i--)
            {
                if (highScores[i].score < score)
                {
                    highScores[i + 1].name = highScores[i].name;
                    highScores[i].name = name;
                    highScores[i + 1].score = highScores[i].score;
                    highScores[i].score = score;
                }
                else
                {
                    break;
                }
            }
        }
    }
   

    #region Singleton
    private static HighScores _instance;

    public static HighScores instance
    {
        get
        {
            if (_instance == null)
            {

                Debug.Log("Neither ~Bootstrap nor ~Init has been loaded");

            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Singleton " + this.GetType() + " instantiated");
            AfterAwake();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            throw new System.Exception("Duplicate singleton instantiated");
        }
    }
    #endregion

}
