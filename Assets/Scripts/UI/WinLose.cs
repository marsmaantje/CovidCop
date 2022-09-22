using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        } else {
            canvas.enabled = false;
        }
    }

    public void Win() {
        gameOver = true;
        isWin = true;

    }
    public void Lose() {
        gameOver = true;
        isLose = true;
    }

    // If key is pressed
    

}
