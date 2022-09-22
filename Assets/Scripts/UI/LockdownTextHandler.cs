using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockdownTextHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI lockdownText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.instance.playerAbilities.nextLockdownTime > Time.time)
        {
            lockdownText.SetText(string.Format("{0:0}", PlayerManager.instance.playerAbilities.nextLockdownTime - Time.time));
        }
        else
        {
            lockdownText.SetText("");
        }
    }
}
