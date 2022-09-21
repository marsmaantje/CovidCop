using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NPCColliderTracker : MonoBehaviour
{
    public List<NPCBehavior> NPCList { get; private set; } = new List<NPCBehavior>();

    [SerializeField] List<NPCBehavior> IgnoreList = new List<NPCBehavior>();

    public Action<NPCBehavior> NPCEntered;
    public Action<NPCBehavior> NPCLeft;

    private void OnTriggerEnter(Collider other)
    {
        NPCBehavior npc = NPCManager.instance.GetNPC(other);
        if(npc != null && !IgnoreList.Contains(npc))
        {
            NPCList.Add(npc);
            NPCEntered?.Invoke(npc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        NPCBehavior npc = NPCManager.instance.GetNPC(other);
        if (npc != null && NPCList.Contains(npc) && !IgnoreList.Contains(npc))
        {
            NPCList.Remove(npc);
            NPCLeft?.Invoke(npc);
        }
    }

    private void Update()
    {
        List<NPCBehavior> removeList = new List<NPCBehavior>();
        foreach (NPCBehavior npc in NPCList)
        {
            if(!npc.isActiveAndEnabled)
            {
                NPCLeft?.Invoke(npc);
                removeList.Add(npc);
            }
        }

        foreach (NPCBehavior npc in removeList)
        {
            NPCList.Remove(npc);
        }
    }
}
