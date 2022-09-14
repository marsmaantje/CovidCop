using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    [SerializeField] private int houseCapacity = 4;

    [Range(1, 5)]
    [SerializeField] private float minWaitTime = 1f;
    [Range(1, 5)]
    [SerializeField] private float maxWaitTime = 5f;

    [Range(1, 10)]
    [SerializeField] private float npcHouseCooldown = 2f;


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
        if (other.gameObject.CompareTag("NPC"))
        {
            NPCBehavior npc = NPCManager.instance.GetNPC(other);

            if (housedNPCs.Count < houseCapacity && npc.getLastHousedTime() < Time.time - npcHouseCooldown)
            {
                housedNPCs.Add(npc);
                npc.SetState(NPCBehavior.NPCState.Housed);

                Invoke("ReleaseNPC", Random.Range(minWaitTime, maxWaitTime));
            }
        }
    }


    private void ReleaseNPC()
    {
        NPCBehavior npc = housedNPCs[0];
        housedNPCs.RemoveAt(0);
        // Set npc position outside the transform position
        npc.setLastHousedTime(Time.time);
        npc.SetState(NPCBehavior.NPCState.Walking);
        npc.gameObject.SetActive(true);
    }



}
