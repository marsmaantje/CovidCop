using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
            Debug.LogError("No audio source found on HouseAudioPlayer");
    }

    void OnEnter(NPCBehavior npc)
    {
        
    }

    void OnExit(NPCBehavior npc)
    {
        
    }
}
