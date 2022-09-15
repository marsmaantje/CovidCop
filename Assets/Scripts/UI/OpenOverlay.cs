using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenOverlay: MonoBehaviour
{
    public Canvas soundSettings;

    void Start()
    {
        soundSettings.enabled = false;
    }

    public void OnPressed()
    {
        soundSettings.enabled = true;
    }
}
