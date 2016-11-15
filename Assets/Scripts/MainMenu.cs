using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject scoreDisplay;
    public GameObject highScoreDisplay;
    public GameObject fadePanel;

    public int menuMusicTrack = 1;
    public float fadeTime = 1.5f;
    float startTime = 0f;


    // Use this for initialization
    void Start() {
        startTime = Time.time;
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
        if (Time.time - startTime < fadeTime) {
            float amount = (Time.time-startTime)/fadeTime;
            fadePanel.GetComponent<Image>().color = new Color(0,0,0, 1f-amount);
        }else {
            Destroy(fadePanel);
        }
    }
}
