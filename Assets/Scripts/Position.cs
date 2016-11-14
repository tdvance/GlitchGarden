using UnityEngine;
using System.Collections;

public class Position : MonoBehaviour {
    public int whichEnemy;

    public bool spawned = false;

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}
