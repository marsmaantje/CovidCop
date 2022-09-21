using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] NPCColliderTracker npcColliderTracker;


    [Range(0, 1)]
    [SerializeField] private float healthDecreaseRate = 0.01f;
    private float playerHealth = 1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player health: " + playerHealth);
        // Decrease player health based on the number of NPCs in the list
        playerHealth -= npcColliderTracker.NPCList.Count * healthDecreaseRate * Time.deltaTime;
    }
}
