using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    [SerializeField] private int houseCapacity = 4;

    private List<NPCBehavior> housedNPCs = new List<NPCBehavior>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    // NPC collides with house
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("NPC"))
        {
            NPCBehavior npc = NPCManager.instance.GetNPC(other);

            if (housedNPCs.Count < houseCapacity)
            {
                housedNPCs.Add(npc);
                npc.SetState(NPCBehavior.NPCState.Housed);
            }
        }
    }
}
