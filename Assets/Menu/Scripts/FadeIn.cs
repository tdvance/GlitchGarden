using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    public float fadeInTime = 3.0f;

    private Image fadePanel;
    private Color currentColor = Color.black;

	// Use this for initialization
	void Start () {
        fadePanel = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Time.timeSinceLevelLoad < fadeInTime)
        {
            float alpha = 1.0f - Time.timeSinceLevelLoad / fadeInTime;
            currentColor = new Color(0, 0, 0, alpha);
            fadePanel.color = currentColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
	}
}
