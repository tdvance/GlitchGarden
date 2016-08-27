using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Navigation : MonoBehaviour
{
    public float minX = 2f;
    public float maxX = 14f;
    public float minY = 0f;
    public float maxY = 9f;
    public float buttonHeight = 0.8f;
    public float buttonWidth = 0.8f;

    public GameObject buttonPrefab;
    public GameObject navigation;
    public SceneScript sceneScript = null;

    [Tooltip("Navigational Buttons needed in this scene, from left to right")]
    public NavButton[] buttons = new NavButton[]
    {
        new NavButton("Back"),
        new NavButton("Forward"),
        new NavButton("Play"),
        new NavButton("Options"),
        new NavButton("Title"),
        new NavButton("Win"),
        new NavButton("Lose"),
        new NavButton("Score"),
    };

    // Use this for initialization
    void Start()
    {
        if (sceneScript == null)
        {
            sceneScript = FindObjectOfType<SceneScript>();
            Debug.Log("Found scene script: " + sceneScript);
        }

        int numButtons = buttons.Length;
        float firstButton = minX + buttonWidth / 2f;
        float lastButton = maxX - buttonWidth / 2f;

        for (int i = 0; i < numButtons; i++)
        {
           
            float t = numButtons == 1 ? 0.5f : (float)i / (numButtons - 1f);
            float x = (1f - t) * firstButton + t * lastButton;
            float y = minY + buttonHeight / 2f;
            GameObject b = Instantiate(buttonPrefab, new Vector3(x, y),
           Quaternion.identity) as GameObject;
            b.transform.SetParent(navigation.transform);
            Button button = b.GetComponent<Button>();
            if (buttons[i].enabled)
            {

                switch (buttons[i].name)
                {
                    case "Back":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Back));
                        break;
                    case "Forward":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Forward));
                        break;
                    case "Play":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Play));
                        break;
                    case "Options":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Options));
                        break;
                    case "Title":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Title));
                        break;
                    case "Win":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Win));
                        break;
                    case "Lose":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Lose));
                        break;
                    case "Score":
                        button.onClick.AddListener(new UnityEngine.Events.UnityAction(Score));
                        break;
                    default:
                        Debug.LogError("Unknown button type: " + buttons[i].name + " at index: " + i);
                        break;
                }
            }else
            {
                button.interactable = false;
            }
            button.image.sprite = buttons[i].sprite;
        }
    }

    void Back()
    {
        Debug.Log("Back");
        sceneScript.LoadPreviousScene();
    }

    void Forward()
    {
        Debug.Log("Forward");
        sceneScript.LoadNextScene();
    }

    void Play()
    {
        Debug.Log("Play");
        sceneScript.LoadGameScene();
    }

    void Options()
    {
        Debug.Log("Options");
        sceneScript.LoadOptionsScene();
    }

    void Title()
    {
        Debug.Log("Title");
        sceneScript.LoadTitleScene();
    }

    void Win()
    {
        Debug.Log("Win");
        sceneScript.LoadWinScene();
    }

    void Lose()
    {
        Debug.Log("Lose");
        sceneScript.LoadLoseScene();
    }
    void Score()
    {
        Debug.Log("Score");
        ScoreManager.instance.addPoints(10);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
