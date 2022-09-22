using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] NPCColliderTracker npcColliderTracker;


    [Range(0, 1)]
    [SerializeField] private float healthDecreaseRate = 0.01f;
    public float playerHealth = 1f;

    [Range(0, 1)]
    [SerializeField] private float infectCloseNPCChance = 0.1f;


    [SerializeField] private float updateRate = 1f;

    float nextUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextUpdate = Time.time + updateRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Player health: " + playerHealth);
        // Decrease player health based on the number of NPCs in the list
        playerHealth -= (npcColliderTracker.InfectedNPCList.Count * healthDecreaseRate * Time.deltaTime);
        playerHealth = Mathf.Clamp(playerHealth, 0, 1);

        if (Time.time >= nextUpdate)
        {
            nextUpdate = Time.time + updateRate;
            TimedUpdate();
        }

    }

    void TimedUpdate()
    {
        if (playerHealth < 0.3f)
        {
            // Infect nearby NPCs
            foreach (NPCBehavior npc in npcColliderTracker.NPCList)
            {
                if (Random.Range(0f, 1f) < infectCloseNPCChance)
                {
                    npc.infected = true;
                }
            }
        }
    }
}
