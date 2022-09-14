using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class Influence : MonoBehaviour
{

    [SerializeField] private float influenceRadius = 10f;

    // Get player prefab
    public GameObject player;


    private List<NPCBehavior> influenced = new List<NPCBehavior>();




    // Start is called before the first frame update
    void Start()
    {
    }


    public void OnPush(InputAction.CallbackContext context)
    {
        foreach (NPCBehavior npc in influenced)
        {
            Vector3 direction = player.transform.position - npc.transform.position;

            direction.Normalize();


            npc.movement.DoMove(new Vector2(direction.x, direction.z));
        }
    }

    public void OnPull(InputAction.CallbackContext context)
    {

        foreach (NPCBehavior npc in influenced)
        {
            Vector3 direction = player.transform.position - npc.transform.position;

            direction.Normalize();

            direction = -direction;
            npc.movement.DoMove(new Vector2(direction.x, direction.z));
        }


    }

    // Update is called once per frame
    void Update()
    {



        // Output distance between player and NPC prefab
        Debug.Log("Amount of NPC in scene: " + NPCManager.instance.NPCList.Count);
        foreach (NPCBehavior npc in NPCManager.instance.NPCList)
        {
            // Get index of NPC in npcs

            float distance = Vector3.Distance(player.transform.position, npc.transform.position);

            if (distance < influenceRadius) influenced.Add(npc);
            
            int index = NPCManager.instance.NPCList.IndexOf(npc);
            Debug.Log("Index of NPC: " + index + " Distance to player: " + distance);
        }




    }
}