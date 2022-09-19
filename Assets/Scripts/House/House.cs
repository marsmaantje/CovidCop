using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class House : MonoBehaviour
{
    [SerializeField] private int houseCapacity = 4;

    [Header("Wait Times")]
    [Range(1, 5)]
    [SerializeField] private float minWaitTime = 1f;
    [Range(1, 5)]
    [SerializeField] private float maxWaitTime = 5f;

    [Range(1, 10)]
    [SerializeField] private float npcHouseCooldown = 2f;

    [Range(0, 1)]
    [SerializeField] private float infectChance = 1f;

    [SerializeField] private bool isHospital = false;

    [SerializeField] private HouseManager houseManager;


    private List<NPCBehavior> housedNPCs = new List<NPCBehavior>();
    private List<NPCBehavior> lockdownNPCs = new List<NPCBehavior>();

    public Action<NPCBehavior> OnNPCAdded;
    public Action<NPCBehavior> OnNPCRemoved;

    public int HouseCapacity { get => houseCapacity; }
    public bool IsHospital { get => isHospital; }

    public List<NPCBehavior> getHousedNPCs()
    {
        return this.housedNPCs;
    }

    public List<NPCBehavior> getLockdownNPCs() {
        return this.lockdownNPCs;
    }

    public void addLockdownNPC(NPCBehavior npc) {
        this.lockdownNPCs.Add(npc);
    }

    void Start()
    {

        houseManager = FindObjectOfType<HouseManager>();

        houseManager.houses.Add(this);
        Debug.Log("House added to HouseManager: " + this.name);
    }


    // NPC collides with house
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            NPCBehavior npc = NPCManager.instance.GetNPC(other);

            if (housedNPCs.Count < houseCapacity && npc.getLastHousedTime() < Time.time - npcHouseCooldown)
            {
                if (!isHospital)
                { //normal house
                    housedNPCs.Add(npc);
                    npc.SetState(NPCBehavior.NPCState.Housed);


                    if (npc.infected)
                    {
                        foreach (NPCBehavior npcInHouse in housedNPCs)
                        {
                            if (UnityEngine.Random.value < infectChance)
                            {
                                npcInHouse.infected = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (NPCBehavior npcInHouse in housedNPCs)
                        {
                            if (npcInHouse.infected && UnityEngine.Random.value < infectChance)
                            {
                                npcInHouse.infected = true;
                            }
                        }
                    }
                    Invoke("ReleaseNPC", UnityEngine.Random.Range(minWaitTime, maxWaitTime));
                    OnNPCAdded?.Invoke(npc);
                }
                else
                { //hospital should only accept infected people
                    if (npc.infected)
                    {
                        housedNPCs.Add(npc);
                        npc.SetState(NPCBehavior.NPCState.Housed);
                        Invoke("ReleaseNPC", UnityEngine.Random.Range(minWaitTime, maxWaitTime));
                        OnNPCAdded?.Invoke(npc);
                    }
                }
            }
        }
    }

    private void ReleaseNPC()
    {
        NPCBehavior npc = housedNPCs[0];
        housedNPCs.RemoveAt(0);

        npc.setLastHousedTime(Time.time);
        npc.gameObject.SetActive(true);
        npc.SetState(NPCBehavior.NPCState.Walking);

        if (isHospital) npc.infected = false;

        OnNPCRemoved?.Invoke(npc);
    }
}
