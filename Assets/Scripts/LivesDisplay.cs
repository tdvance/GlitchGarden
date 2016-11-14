using UnityEngine;
using System.Collections;

public class LivesDisplay : MonoBehaviour {
    public GameObject ship1;
    public GameObject ship2;

    public bool RemoveLife() {
        if (ship2) {
            Destroy(ship2);
            return true;
        }
        if (ship1) {
            Destroy(ship1);
            return true;
        }
        return false;
    }

}
