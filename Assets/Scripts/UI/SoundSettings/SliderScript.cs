using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI txt;

    void Start()
    {
    }

    
    void Update()
    {
        txt.text = slider.value.ToString();
    }
}
