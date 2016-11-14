using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {
    public GameObject[] enemyPrefabs;
    public float width = 11;
    public float height = 5;
    public float xMin = -9f;
    public float xMax = 9f;
    public float speed = 5f;
    public float spawnDelay = 1f;

    public AudioClip levelStart;

    float dx = 1f;
    float spawnTime = 0f;
    bool spawning = false;

    static int currentNumShips = 0;

    public static int GetNumShips() {
        return currentNumShips;
    }

    // Use this for initialization
    void Start() {
        spawning = true;
        SpawnFormation();
    }

    // Update is called once per frame
    void Update() {
        MoveFormation();
        spawning = CheckEnemies();
        if (spawning) {
            currentNumShips = 9;
            spawning = SpawnWithDelay();
        }
    }

    bool SpawnWithDelay() {

        spawnTime += Time.deltaTime;
        if (spawnTime > spawnDelay) {
            spawnTime = 0;
            if (!SpawnEnemy()) {
                return false;
            }
        }
             
        return true;
    }

    bool CheckEnemies() {
        int count = 0;
        foreach (Transform position in transform) {
            count += position.transform.childCount;
        }
        currentNumShips = count;
        if (count == 0 && !spawning) {
            ScoreDisplay.instance.score += 50;
            ScoreDisplay.instance.Highlight();
            ScoreManager.instance.SubmitScore(ScoreDisplay.instance.score);
            SpawnFormation();
            return true;
        }
        return spawning;
    }

    void MoveFormation() {
        transform.Translate(Vector3.right * dx * Time.deltaTime * speed);
        if (transform.position.x - width / 2f < xMin) {
            transform.position = new Vector2(xMin + width / 2f, transform.position.y);
            dx = 1f;
        } else if (transform.position.x + width / 2f > xMax) {
            transform.position = new Vector2(xMax - width / 2f, transform.position.y);
            dx = -1f;
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    void SpawnFormation() {
        GetComponent<AudioSource>().clip = levelStart;
        GetComponent<AudioSource>().volume = GameOptions.instance.sfxVolume;
        GetComponent<AudioSource>().Play();
        foreach (Transform child in transform) {
            child.GetComponent<Position>().spawned = false;
        }
        spawnTime = 0f;
    }

    Transform NextFreePosition() {
        foreach (Transform position in transform) {
            if (!position.GetComponent<Position>().spawned) {
                position.GetComponent<Position>().spawned = true;
                return position;
            }
        }
        return null;
    }

    GameObject SpawnEnemy() {
        Transform parent = NextFreePosition();
        if (!parent) {
            return null;
        }
        int which = parent.GetComponent<Position>().whichEnemy;
        GameObject enemy = Instantiate(enemyPrefabs[which], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        enemy.transform.SetParent(parent, false);

        return enemy;
    }
}
