using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Influence : MonoBehaviour
{

    private bool isInfluencable = false;

    // Get player prefab
    public GameObject player;

    private GameObject[] npcs;




    // Start is called before the first frame update
    void Start()
    {
        // Find all NPC prefabs
        npcs = GameObject.FindGameObjectsWithTag("NPC");


    }

    // Update is called once per frame
    void Update()
    {
        // Output distance between player and NPC prefab
        Debug.Log("Amount of NPC in scene: " + npcs.Length);
        foreach (GameObject npc in npcs)
        {
            // Get index of NPC in npcs
            
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);



            isInfluencable = distance < 5;

            int index = System.Array.IndexOf(npcs, npc);
            Debug.Log("Index of NPC: " + index + " Distance to player: " + distance + " Influencable: " + isInfluencable);

            
            
            
        }


        // If E is pressed, move NPC away from player until distance is greater than 10
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (GameObject npc in npcs)
            {
                float distance = Vector3.Distance(player.transform.position, npc.transform.position);
                if (distance < 10)
                {
                    npc.transform.position = Vector3.MoveTowards(npc.transform.position, player.transform.position, 1);
                }
            }
        }


    }
}
