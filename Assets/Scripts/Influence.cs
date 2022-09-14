using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class Influence : MonoBehaviour
{

    [SerializeField] private float influenceRadius = 10f;

    // Get player prefab
    public GameObject player;


    private List<NPCBehavior> influenced = new List<NPCBehavior>();

    private bool isPushing = false;
    private bool isPulling = false;




    // Start is called before the first frame update
    void Start()
    {
    }


    public void OnPush(InputAction.CallbackContext context)
    {
        isPushing = true;
        isPulling = false;
    }

    public void OnPull(InputAction.CallbackContext context)
    {
        isPulling = true;
        isPushing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushing || isPulling)
        {

            foreach (NPCBehavior npc in NPCManager.instance.NPCList)
            {
                if (GetNPCDistance(npc) < influenceRadius)
                {

                    Vector3 direction = player.transform.position - npc.transform.position;

                    direction.Normalize();

                    //rotate direction to be relative to the npc
                    direction = npc.transform.InverseTransformDirection(direction);

                    npc.movement.DoMove(new Vector2(direction.x, direction.z) * (isPushing ? 1 : -1));
                }
                else
                {
                    {
                        npc.movement.DoMove(Vector2.zero);
                    }
                }
            }
        }

        /*
        // Output distance between player and NPC prefab
        foreach (NPCBehavior npc in NPCManager.instance.NPCList)
        {
            // Get index of NPC in npcs

            float distance = Vector3.Distance(player.transform.position, npc.transform.position);

            if (distance < influenceRadius) influenced.Add(npc);
        }
        */

        foreach(NPCBehavior npc in influenced)
        {

            if(influenced.Count < 1) break;
 
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);

            if(distance > influenceRadius) influenced.Remove(npc);
            
        }


    }

    private float GetNPCDistance(NPCBehavior npc)
    {
        return Vector3.Distance(player.transform.position, npc.transform.position);
    }
}