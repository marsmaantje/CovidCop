using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{

    [SerializeField] private bool isPause = false;
    [SerializeField] GameObject pauseScreen;

    // Start is called before the first frame update

    
    public void OnPause(InputAction.CallbackContext callbackContext) {
        if (callbackContext.performed) {
            isPause = !isPause;
            if (isPause) {
                pauseScreen.SetActive(true);
                Time.timeScale = 0;
            } else {
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    public void PauseGame()
    {
        isPause = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void PlayGame()
    {
        isPause = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }
}
