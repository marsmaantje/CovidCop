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
            Vector3 direction = npc.transform.position - HouseManager.instance.houses[0].transform.position;


            direction.Normalize();

            direction = npc.transform.InverseTransformDirection(direction);


            Debug.Log(direction);
            npc.movement.DoMove(new Vector2(direction.x, direction.z) * 2);
            // move npc to nearest house
        });

    }
}
