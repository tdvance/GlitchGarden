using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed, damage;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject obj = collider.gameObject;
        if (!obj.GetComponent<Attacker>())
        {
            return; //do no more if not an attacker
        }
        Debug.Log("Projectile " + name + " hit attacker " + obj);
        obj.GetComponent<Health>().damage(damage);
        Destroy(gameObject);
    }

}
