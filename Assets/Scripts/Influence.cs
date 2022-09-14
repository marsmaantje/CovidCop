using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class Influence : MonoBehaviour
{
    [SerializeField] private bool colliderBased = false;
    [SerializeField] private float influenceRadius = 10f;

    [SerializeField]private List<NPCBehavior> influenced = new List<NPCBehavior>();

    [SerializeField]private bool isPushing = false;
    [SerializeField]private bool isPulling = false;




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
            if (!colliderBased)
            {
                foreach (NPCBehavior npc in NPCManager.instance.NPCList)
                {
                    if (GetNPCDistance(npc) < influenceRadius)
                    {

                        Vector3 direction = transform.position - npc.transform.position;

                        direction.Normalize();

                        //rotate direction to be relative to the npc
                        direction = npc.transform.InverseTransformDirection(direction);

                        npc.movement.DoMove(new Vector2(direction.x, direction.z) * (isPushing ? 1 : -1));
                        npc.SetState(NPCBehavior.NPCState.Influenced);
                    }
                    else
                    {
                        npc.movement.DoMove(Vector2.zero);
                        npc.StopInfluence();
                    }
                }
            }
            else
            {
                foreach (NPCBehavior npc in influenced)
                {
                    Vector3 direction = transform.position - npc.transform.position;

                    direction.Normalize();

                    //rotate direction to be relative to the npc
                    direction = npc.transform.InverseTransformDirection(direction);

                    npc.movement.DoMove(new Vector2(direction.x, direction.z) * (isPushing ? 1 : -1));
                    npc.SetState(NPCBehavior.NPCState.Influenced);

                    if (GetNPCDistance(npc) > influenceRadius)
                    {
                        npc.movement.DoMove(Vector2.zero);
                        npc.StopInfluence();
                        influenced.Remove(npc);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NPCBehavior npc = NPCManager.instance.GetNPC(other);
        if (npc != null)
            influenced.Add(npc);
    }
    
    private void OnTriggerExit(Collider other)
    {
        NPCBehavior npc = NPCManager.instance.GetNPC(other);
        if (npc != null)
        {
            influenced.Remove(npc);
            npc.StopInfluence();
        }
    }

    private float GetNPCDistance(NPCBehavior npc)
    {
        return Vector3.Distance(transform.position, npc.transform.position);
    }
}