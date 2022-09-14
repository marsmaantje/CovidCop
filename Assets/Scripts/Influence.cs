using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Influence : MonoBehaviour
{

    [SerializeField] private float influenceRadius = 10f;

    // Get player prefab
    public GameObject player;

    private List<GameObject> npcs;
    private List<GameObject> influenced = new List<GameObject>();




    // Start is called before the first frame update
    void Start()
    {
        // Find all NPC prefabs
        npcs = new List<GameObject>(GameObject.FindGameObjectsWithTag("GameController"));



    }

    // Update is called once per frame
    void Update()
    {
        // Output distance between player and NPC prefab
        Debug.Log("Amount of NPC in scene: " + npcs.Count);
        foreach (GameObject npc in npcs)
        {
            // Get index of NPC in npcs

            float distance = Vector3.Distance(player.transform.position, npc.transform.position);



            if (distance < influenceRadius)
            {
                influenced.Add(npc);
            }





            int index = npcs.IndexOf(npc);
            Debug.Log("Index of NPC: " + index + " Distance to player: " + distance);




        }




        foreach (GameObject npc in influenced)
        {



            Vector3 direction = player.transform.position - npc.transform.position;

            direction.Normalize();

            float distance = Vector3.Distance(player.transform.position, npc.transform.position);







            // Move NPC smoothly

            if (Input.GetKeyDown(KeyCode.E))
            {
                npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                npc.GetComponent<Rigidbody>().velocity = direction * 2;

            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {

                direction = -direction;
                npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                npc.GetComponent<Rigidbody>().velocity = direction * 2;
            }

            if (distance > influenceRadius)
            {
                npc.GetComponent<Rigidbody>().velocity = Vector3.zero;
                influenced.Remove(npc);
            }

            if (distance < 2)
            {
                npc.GetComponent<Rigidbody>().velocity = Vector3.zero;

            }
        }


    }
}