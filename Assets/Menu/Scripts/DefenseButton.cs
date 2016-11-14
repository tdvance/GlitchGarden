using UnityEngine;
using System.Collections;

public class DefenseButton : MonoBehaviour
{
    private DefenseButton[] buttons;
    public GameObject defenderPrefab;
    public static GameObject selectedDefender;
    

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        buttons = GameObject.FindObjectsOfType<DefenseButton>();
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        foreach(DefenseButton button in buttons)
        {
            button.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        selectedDefender = defenderPrefab;
    }
}
