using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI txt;

    
    void Update()
    {
        txt.SetText(string.Format("{0:0}", slider.value * 100));
    }
}
