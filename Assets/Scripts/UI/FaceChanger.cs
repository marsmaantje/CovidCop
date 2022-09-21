using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceChanger : MonoBehaviour
{

    public Sprite face0;
    public Sprite face1;
    public Sprite face2;
    public SicknessMeter sm;

    Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        if (sm.sickness >= 230)
        {
            img.sprite = face0;
        } else if (sm.sickness >= 100)
        {
            img.sprite = face1;
        } else
        {
            img.sprite = face2;
        }
            
    }
}
