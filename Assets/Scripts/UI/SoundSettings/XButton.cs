using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XButton : MonoBehaviour
{
    public Canvas soundSettings;

    public void OnPressed()
    {
        soundSettings.enabled = false;
    }
}
