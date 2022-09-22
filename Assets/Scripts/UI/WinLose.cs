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
            canvas.enabled = true;
            win.enabled = isWin;
            lose.enabled = isLose;
        } else {
            canvas.enabled = false;
        }
    }
}
