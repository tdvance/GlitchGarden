using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Attacker))]
public class Fox : MonoBehaviour {
    private Attacker attacker;
    private Animator animator;

	// Use this for initialization
	void Start () {
        attacker = GetComponent<Attacker>();
        animator = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {

        GameObject obj = collider.gameObject;
        if (!obj.GetComponent<Defender>())
        {
            return; //do no more if not a defender
        }
        Debug.Log("Fox.cs Trigger Enter collider = " + collider);
        if (obj.GetComponent<Stone>())
        {
            animator.SetTrigger("Jump Trigger");
        }else
        {
            animator.SetBool("isAttacking", true);
            attacker.Attack(obj);
        }
    }

    // Update is called once per frame
    void Update () {
	}
}
