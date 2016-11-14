using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    int _score;
    int _highScore;

    string highScoreKey = "High Score";

    public int score {
        get {
            return _score;
        }
    }


    public int highScore {
        get {
            return _highScore;
        }
    }

    public void SubmitScore(int score) {
        this._score = score;
        if (score > _highScore) {
            _highScore = score;
            PlayerPrefs.SetInt(highScoreKey, score);
        }
    }

    void Start() {
        if (PlayerPrefs.HasKey(highScoreKey)) {
            _highScore = PlayerPrefs.GetInt(highScoreKey);
        } else {
            _highScore = 0;
        }
    }

    #region Singleton
    private static ScoreManager _instance;

    public static ScoreManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<ScoreManager>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance != null && _instance != this) {
            Debug.LogError("Duplicate singleton " + this.gameObject + " created; destroying it now");
            Destroy(this.gameObject);
        }

        if (_instance != this) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }
    #endregion

}
