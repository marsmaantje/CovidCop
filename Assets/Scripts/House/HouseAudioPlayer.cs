using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseAudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] House house;

    [Range(0.5f, 2f)]
    [SerializeField] float pitchEnter = 1f;

    [Range(0.5f, 2f)]
    [SerializeField] float pitchLeave = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
            Debug.LogError("No audio source found on HouseAudioPlayer");

        if(house == null)
            Debug.LogError("No house found on HouseAudioPlayer");

        house.OnNPCAdded += OnEnter;
        house.OnNPCRemoved += OnExit;
    }

    void OnEnter(NPCBehavior npc)
    {
        audioSource.pitch = pitchEnter;
        audioSource.Play();
    }

    void OnExit(NPCBehavior npc)
    {
        audioSource.pitch = pitchLeave;
        audioSource.Play();
    }
}
