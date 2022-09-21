using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] AudioMixer Mixer;
    [SerializeField] string VolumeName;

    public void OnChangeSlider(float Value)
    {
        if(Value > 0)
            Mixer.SetFloat(VolumeName, Mathf.Log10(Mathf.Pow(Value, 2)) * 20);
        else //volume is 0
            Mixer.SetFloat(VolumeName, -80);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
