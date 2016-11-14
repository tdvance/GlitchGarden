using UnityEngine;
using System.Collections;

public class GameInit : MonoBehaviour {
    [Tooltip("For debugging, to spawn _Init scene singletons if starting from this scene")]
    public GameObject initScriptsPrefab;

    public int gameMusicTrack = 1;

    // Use this for initialization
    void Start() {
        if (FlexibleMusicManager.instance) {
            FlexibleMusicManager.instance.SetCurrentTrack(gameMusicTrack);
            FlexibleMusicManager.instance.Play();
        } else {
            Debug.LogWarning("Missing singleton: FlexibleMusicManager, attempting to make one");
            Instantiate(initScriptsPrefab);
        }


        ScoreDisplay.instance.score = 0;
    }

    // Update is called once per frame
    void Update() {

    }
}
