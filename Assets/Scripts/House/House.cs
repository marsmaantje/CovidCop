using System.Collections;
using System;
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


    [Range(0, 1)]
    [SerializeField] private float infectChance = 1f;



    [SerializeField] private bool isHospital = false;


    private List<NPCBehavior> housedNPCs = new List<NPCBehavior>();

    public Action<NPCBehavior> OnNPCAdded;
    public Action<NPCBehavior> OnNPCRemoved;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<NPCBehavior> getHousedNPCs()
    {
        return this.housedNPCs;
    }




    // NPC collides with house
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            NPCBehavior npc = NPCManager.instance.GetNPC(other);
            Debug.Log("NPC entered house " + this.GetInstanceID());

            if (housedNPCs.Count < houseCapacity && npc.getLastHousedTime() < Time.time - npcHouseCooldown)
            {
                housedNPCs.Add(npc);
                npc.SetState(NPCBehavior.NPCState.Housed);

                if (!isHospital && npc.infected)
                { 
                    foreach (NPCBehavior npcInHouse in housedNPCs)
                    {
                        if (UnityEngine.Random.Range(0f, 1f) < infectChance)
                        {
                            npcInHouse.infected = true;
                        }
                    }
                }

                Invoke("ReleaseNPC", UnityEngine.Random.Range(minWaitTime, maxWaitTime));
                OnNPCAdded?.Invoke(npc);
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

        if (isHospital) npc.infected = false;
        

        OnNPCRemoved?.Invoke(npc);
    }



}
