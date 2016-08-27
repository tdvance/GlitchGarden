using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class NavButton
{
    public NavButton(string name)
    {
        this.name = name;
    }

    [Tooltip("Navigational button")]
    public string name;

    [Tooltip("Image for navigational button")]
    public Sprite sprite;

    [Tooltip("Appears on this scene")]
    public bool enabled = true;
}