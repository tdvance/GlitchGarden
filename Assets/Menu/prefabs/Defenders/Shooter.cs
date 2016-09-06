using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    public GameObject projectile;
    public GameObject projectileParent;

    private Transform gun;

	// Use this for initialization
	void Start () {
        gun = gameObject.transform.GetChild(1);
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
