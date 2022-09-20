using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame: MonoBehaviour
{
    public string sceneName;

    public void OnPressed()
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))   
        {
            if ((!Input.GetMouseButtonDown(0))) {
                SceneManager.LoadScene(sceneName);
            }
            
        }

    }


}
