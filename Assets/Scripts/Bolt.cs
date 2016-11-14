using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour {
    [Tooltip("Bolt does this much damage to player or enemy")]
    public float damage = 1f;

    // Use this for initialization
    void Start() {
        GetComponent<AudioSource>().volume = GameOptions.instance.sfxVolume;

        if (tag != "EnemyFire" && tag != "FriendlyFire") {
            Debug.LogWarning("Bolt should either be tagged 'EnemyFire' or 'FriendlyFire'");
        }
    }

    // Update is called once per frame
    void Update() {
        if (transform.position.y > 5 || transform.position.y < -5) {
            Destroy(gameObject); //bolt is offscreen, so destroy it.
        }
    }
}
