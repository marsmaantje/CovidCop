using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockdownImageHandler : MonoBehaviour
{
    [SerializeField] private Image lockdownImage;
    [SerializeField] Sprite cooldownSprite, normalSprite;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.playerAbilities.nextLockdownTime > Time.time)
        {
            lockdownImage.sprite = cooldownSprite;
        }
        else
        {
            lockdownImage.sprite = normalSprite;
        }

    }


}
