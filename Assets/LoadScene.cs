using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public string sceneName;

    public void OnPressed()
    {
        SceneManager.LoadScene(sceneName);
    }


}
