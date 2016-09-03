using UnityEngine;
using System.Collections;

public class Lizard : MonoBehaviour {
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
        Debug.Log("Lizard.cs Trigger Enter collider = " + collider);
       
            animator.SetBool("isAttacking", true);
            attacker.Attack(obj);
        
    }

    // Update is called once per frame
    void Update () {
	
	}
}
