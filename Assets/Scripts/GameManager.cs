using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Lose when all npc are infected
    // Win when all npc are cured

    [SerializeField] private NPCManager npcManager;
    [SerializeField] private WinLose winLose;
    bool firsttime = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (firsttime)
            Debug.Log("game manager first update");
        firsttime = false;
        if (npcManager.InfectedNPCList.Count >= npcManager.NPCList.Count)
        {
            // Lose
            winLose.Lose();
            Debug.Log("Lost");
        }
        else if (npcManager.InfectedNPCList.Count <= 0)
        {
            // Win
            winLose.Win();
            Debug.Log("Won");
        }
    }
}
