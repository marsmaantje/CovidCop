using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockdownUI : MonoBehaviour
{
    RectMask2D mask;

    [Range(0, 1)]
    public float lockdown;

    Vector4 modifyMask;

    void Start()
    {
        mask = GetComponent<RectMask2D>();
        modifyMask = new Vector4(0, 0, 0, 0);
    }


    void Update()
    {
        mask.padding = modifyMask;

        modifyMask.w = lockdown * 150;
    }
}
