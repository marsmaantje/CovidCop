using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{


    public void OnPressed()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
