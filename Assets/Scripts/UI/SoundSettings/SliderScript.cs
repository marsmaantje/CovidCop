using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderScript : MonoBehaviour
{
    [SerializeField]public Slider slider;
    public TextMeshProUGUI txt;

    void Start()
    {
    }

    
    void Update()
    {
        txt.text = slider.value.ToString();
    }
}
