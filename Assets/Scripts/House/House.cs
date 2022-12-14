using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class House : MonoBehaviour
{
    [SerializeField] private int houseCapacity = 4;

    [Header("Wait Times")]
    [Range(1, 20)]
    [SerializeField] public float minWaitTime = 1f;
    [Range(1, 20)]
    [SerializeField] public float maxWaitTime = 5f;
    [Range(10, 50)]
    [SerializeField] public float minLockdownWaitTime = 10f;
    [Range(10, 50)]
    [SerializeField] public float maxLockdownWaitTime = 20f;

    private float startMinWaitTime;
    private float startMaxWaitTime;


    [Range(0, 10)]
    [SerializeField] private float npcHouseCooldown = 2f;

    [Range(0, 1)]
    [SerializeField] private float infectChance = 1f;

    [SerializeField] private bool isHospital = false;

    [SerializeField] private HouseManager houseManager;
    [SerializeField] private PlayerManager playerManager;


    [SerializeField] private HouseState currentState = HouseState.Normal;
    private HouseState previousState = HouseState.Normal;


    private List<NPCBehavior> housedNPCs = new List<NPCBehavior>();
    private List<NPCBehavior> lockdownNPCs = new List<NPCBehavior>();

    public Action<NPCBehavior> OnNPCAdded;
    public Action<NPCBehavior> OnNPCRemoved;

    public int HouseCapacity { get => houseCapacity; }
    public int housedNPCCount { get => housedNPCs.Count; }
    public bool IsHospital { get => isHospital; }

    public List<NPCBehavior> getHousedNPCs()
    {
        return this.housedNPCs;
    }

    public List<NPCBehavior> getLockdownNPCs()
    {
        return this.lockdownNPCs;
    }

    public void addLockdownNPC(NPCBehavior npc)
    {
        this.lockdownNPCs.Add(npc);
    }

    void Start()
    {

        houseManager = FindObjectOfType<HouseManager>();

        houseManager.houses.Add(this);
        //Debug.Log("House added to HouseManager: " + this.name);

        startMinWaitTime = minWaitTime;
        startMaxWaitTime = maxWaitTime;

    }

    void Update()
    {
        switch (currentState)
        {
            case HouseState.Normal:
                minWaitTime = startMinWaitTime;
                maxWaitTime = startMaxWaitTime;
                break;

            case HouseState.Lockdown:
                minWaitTime = minLockdownWaitTime;
                maxWaitTime = maxLockdownWaitTime;
                break;
        }
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
                    if (currentState == HouseState.Lockdown)
                    {
                        Invoke("LockdownRelease", UnityEngine.Random.Range(minWaitTime, maxWaitTime));
                    }
                    else
                    {
                        Invoke("ReleaseNPC", UnityEngine.Random.Range(minWaitTime, maxWaitTime));
                    }
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

        if(other.gameObject.CompareTag("Player")) {
            if(isHospital) {
                if(PlayerManager.instance.playerHealth.playerHealth < 0.3f) {
                    PlayerManager.instance.gameObject.SetActive(false);
                    Invoke("ReleasePlayer", 3f);
                }
            }
            
        }
    }

    private void ReleasePlayer() {
        PlayerManager.instance.gameObject.SetActive(true);
        PlayerManager.instance.playerHealth.playerHealth = 1f;
    }

    private void ReleaseNPC()
    {
        this.SetState(HouseState.Normal);
        NPCBehavior npc = housedNPCs[0];

        housedNPCs.RemoveAt(0);

        npc.setLastHousedTime(Time.time);
        npc.gameObject.SetActive(true);
        npc.SetState(NPCBehavior.NPCState.Walking);

        if (isHospital) npc.infected = false;

        OnNPCRemoved?.Invoke(npc);
    }

    private void LockdownRelease()
    {
        if (this.housedNPCs.Count < 1)
        {
            this.SetState(HouseState.Normal);
            return;
        }

        NPCBehavior npc = housedNPCs[0];


        // 80% chance to cure
        if (npc.infected && UnityEngine.Random.value < 0.8f)
        {
            npc.infected = false;
        }

        

        housedNPCs.Remove(npc);
        lockdownNPCs.Remove(npc);

        npc.setLastHousedTime(Time.time);
        npc.gameObject.SetActive(true);
        npc.SetState(NPCBehavior.NPCState.Walking);

        OnNPCRemoved?.Invoke(npc);
    }

    public void SetState(HouseState state)
    {
        previousState = currentState;
        currentState = state;
    }

    public enum HouseState
    {
        Normal,
        Lockdown
    }
}
