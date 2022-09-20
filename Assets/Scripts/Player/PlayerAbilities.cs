using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{

    private bool isLockdown = false;


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

        NPCManager.instance.NPCList.ForEach(npc =>
        {
            npc.SetState(NPCBehavior.NPCState.Lockdown);
            // move npc to nearest house
        });

        HouseManager.instance.houses.ForEach(house => {
            if(!house.IsHospital) {
                house.SetState(House.HouseState.Lockdown);
            }
        });

    }
}
