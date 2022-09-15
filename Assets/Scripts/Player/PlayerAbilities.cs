using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{


    

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLockdown(InputAction.CallbackContext context)
    {

        

        if (context.performed)
        {
            NPCManager.instance.NPCList.ForEach(npc => {
                // move npc to nearest house
                npc.movement.DoMove(HouseManager.instance.houses[0].transform.position);
                Debug.Log("Lockdown! Moving NPC to: " + HouseManager.instance.houses[0].transform.position + " from: " + npc.transform.position);
            });
        }
    }
}
