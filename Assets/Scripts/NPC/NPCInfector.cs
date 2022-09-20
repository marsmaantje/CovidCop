using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class NPCInfector : MonoBehaviour
{
    [SerializeField] NPCBehavior NPC;
    [SerializeField] NPCColliderTracker ColliderTracker;

    [Range(0,1)]
    [SerializeField] float InfectionChance = 0.7f;
    [Tooltip(" Seconds between each infect try")]
    [SerializeField] float updateRate = 1f;
    float nextUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(NPC == null)
            Debug.LogError("NPCInfector: NPC is null");
        
        if (ColliderTracker == null)
            Debug.LogError("NPCInfector: ColliderTracker is null");

        nextUpdate = Time.time + updateRate;
    }

    // Update is called once per frame
    void Update()
    {
        // If the next update is reached
        if (Time.time >= nextUpdate)
        {
            Debug.Log(Time.time + ">=" + nextUpdate);
            // Change the next update (current second+1)
            nextUpdate = Time.time + updateRate;
            // Call your fonction
            TimedUpdate();
        }
    }
    
    void TimedUpdate()
    {
        if(NPC.infected)
        {
            foreach (NPCBehavior npc in ColliderTracker.NPCList)
            {
                if (!npc.infected)
                {
                    if (Random.Range(0f, 1f) <= InfectionChance)
                    {
                        npc.infected = true;
                    }
                }
            }
        }
    }

    void NPCEntered(NPCBehavior npc)
    {
        //chance to infect npc
        if (NPC.infected && Random.Range(0f, 1f) <= InfectionChance)
        {
            npc.infected = true;
        }
    }
}
