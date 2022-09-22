using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    [Header("Images:")]
    [SerializeField] Image win;
    [SerializeField] Image lose;
    [Space(10)]
    [Header("Bools:")]
    public bool gameOver;
    public bool isWin;
    public bool isLose;

    public string sceneName;

    Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    void Update()
    {
        if (gameOver)
        {


            Time.timeScale = 0.01f;
            canvas.enabled = true;
            win.enabled = isWin;
            lose.enabled = isLose;

            if (Input.anyKeyDown)
            //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))   
            {
                if ((!Input.GetMouseButtonDown(0)))
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(sceneName);
                }

            }
        }
        else
        {
            canvas.enabled = false;
        }
    }

    public void Win()
    {
        gameOver = true;
        isWin = true;

    }
    public void Lose()
    {
        gameOver = true;
        isLose = true;
    }


    public void OnPressed()
    {
        SceneManager.LoadScene(sceneName);
    }




}
