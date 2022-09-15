using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SicknessMeter : MonoBehaviour
{

    RectMask2D mask;

    [Range(0, 330)]
    public int sickness;

    Vector4 modifyMask;

    void Start()
    {
        mask = GetComponent<RectMask2D>();
        modifyMask = new Vector4(0, 0, 0, 0);
        //RectMask2D uses Vector4 to mask from the 4 different sides
    }


    void Update()
    {
        mask.padding = modifyMask;

        modifyMask.z = sickness;
    }
}
