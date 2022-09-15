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
                npc.SetState(NPCBehavior.NPCState.Lockdown);
                Vector2 direction = new Vector2(
                    npc.transform.position.x - HouseManager.instance.houses[0].transform.position.x, 
                    npc.transform.position.z - HouseManager.instance.houses[0].transform.position.z);
                
                direction.Normalize();

                direction = npc.transform.InverseTransformDirection(direction);

                
                Debug.Log(direction);
                npc.movement.DoMove(direction);
                // move npc to nearest house
            });
        }
    }
}
