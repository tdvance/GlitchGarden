using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject scoreDisplay;
    public GameObject highScoreDisplay;
    public int menuMusicTrack = 1;


    // Use this for initialization
    void Start() {
        if (ScoreManager.instance) {
            scoreDisplay.GetComponent<ScoreDisplay>().prefixText = "Score: ";
            scoreDisplay.GetComponent<ScoreDisplay>().score = ScoreManager.instance.score;
            highScoreDisplay.GetComponent<ScoreDisplay>().prefixText = "High Score: ";
            highScoreDisplay.GetComponent<ScoreDisplay>().score = ScoreManager.instance.highScore;
        } else {
            Debug.LogWarning("Missing singleton: ScoreManager");
        }
        if (FlexibleMusicManager.instance) {
            if (FlexibleMusicManager.instance.CurrentTrackNumber() != menuMusicTrack) {
                FlexibleMusicManager.instance.SetCurrentTrack(menuMusicTrack);
                FlexibleMusicManager.instance.Play();
            }
        } else {
            Debug.LogWarning("Missing singleton: FlexibleMusicManager");
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
