using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Movement movement;

    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float minWalkTime;
    [SerializeField] private float maxWalkTime;
    [SerializeField] private NPCState currentState = NPCState.Waiting;
    public Rigidbody rigidbody;

    private float waitTime;
    
    private float walkTime;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    enum NPCState
    {
        Waiting,
        Walking,
        Influenced,
        Housed
    }
}
