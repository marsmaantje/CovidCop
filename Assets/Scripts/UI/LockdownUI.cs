using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockdownUI : MonoBehaviour
{
    [SerializeField] RectMask2D mask;

    [Range(0, 1)]
    public float lockdown;

    Vector4 modifyMask;

    void Start()
    {
        modifyMask = new Vector4(0, 0, 0, 0);
    }


    void Update()
    {
        lockdown = Mathf.Clamp01((PlayerManager.instance.playerAbilities.nextLockdownTime - Time.time) / PlayerManager.instance.playerAbilities.lockdownCooldown);

        mask.padding = modifyMask;

        modifyMask.y = lockdown * 150;
    }
}
