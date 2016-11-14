using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    public GameObject projectile;
    private GameObject projectileParent;

    private Transform gun;

	// Use this for initialization
	void Start () {
        gun = gameObject.transform.GetChild(1);

        GameObject[] projectiles = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
        if (projectileParent == null)
        {
            foreach (GameObject p in projectiles)
            {
                if (p.name == "Projectiles")
                {
                    projectileParent = p;
                }
            }
        }
        if (projectileParent == null)
        {
            projectileParent = new GameObject("Projectiles");

        }

    }

    // Update is called once per frame
    void Update () {
	
	}

    private void Fire()
    {
        GameObject newProjectile = Instantiate(projectile);
        newProjectile.transform.position = gun.position;
        newProjectile.transform.SetParent(projectileParent.transform);
    }

}
