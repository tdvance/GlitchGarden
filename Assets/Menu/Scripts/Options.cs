using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : SceneScript {

    public InputField nameField;

    public Slider volumeControl;

    public Slider difficultyControl;

	override public void AfterStart () {
        Debug.Log("Initializing controls");
        nameField.text = OptionsManager.instance.player_name;
        nameField.placeholder.GetComponent<Text>().text = nameField.text;
        volumeControl.value = OptionsManager.instance.volume;
        difficultyControl.value = OptionsManager.instance.difficulty;

    }

    override public void AfterUpdate () {
	
	}

    public void SetName()
    {
        Debug.Log("Set Name");
        OptionsManager.instance.player_name = nameField.text;
    }

    public void SetVolume()
    {
        Debug.Log("Set Volume");
        OptionsManager.instance.volume = volumeControl.value;

    }

    public void SetDifficulty()
    {
        Debug.Log("Set Difficulty");
        OptionsManager.instance.difficulty = difficultyControl.value;

    }

    public void Reset()
    {
        Debug.Log("Reset");
        OptionsManager.instance.Reset();
        AfterStart();//adjust controls to match
    }
}
