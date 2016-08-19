using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlueScreen : GenericMenu
{
    public string errorMessage = "Stub: error message goes here";
    new void Start()
    {
        base.Start();
        subtitle.text = errorMessage;
    }
    void Update()
    {
        // do nothing
    }
}
