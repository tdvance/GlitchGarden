using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

    public GameObject musicVolumeSlider;
    public GameObject sfxVolumeSlider;

    // Use this for initialization
    void Start() {
        if (musicVolumeSlider) {
            musicVolumeSlider.GetComponent<Slider>().value = GameOptions.instance.musicVolume;
        }
        if (sfxVolumeSlider) {
            sfxVolumeSlider.GetComponent<Slider>().value = GameOptions.instance.sfxVolume;
        }
    }

    public void UpdateMusic() {
        GameOptions.instance.SetMusicVolume(musicVolumeSlider.GetComponent<Slider>().value);

    }

    public void UpdateSFX() {
        GameOptions.instance.SetSFXVolume(sfxVolumeSlider.GetComponent<Slider>().value);
    }

}
