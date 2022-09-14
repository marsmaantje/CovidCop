using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCBehavior : MonoBehaviour
{
    [SerializeField] public Movement movement;

    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float minWalkTime;
    [SerializeField] private float maxWalkTime;
    [SerializeField] private NPCState currentState = NPCState.Waiting;
    public Rigidbody rigidbody;
    public Collider collider;

    private float waitTime;
    
    private float walkTime;

    public bool infected = false;


    private float lastHousedTime = float.MinValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case NPCState.Waiting:
                
                break;
            case NPCState.Walking:
                
                break;
            case NPCState.Housed:
                gameObject.SetActive(false);
                break;
            case NPCState.Influenced:

                break;
            default:
                Debug.LogError("NPCState not implemented");
                break;
        }

    }

    // Setter for state
    public void SetState(NPCState state)
    {
        currentState = state;
    }

    public float getLastHousedTime() {
        return this.lastHousedTime;
    }

    public void setLastHousedTime(float time) {
        this.lastHousedTime = time;
    }

    public enum NPCState
    {
        Waiting,
        Walking,
        Influenced,
        Housed
    }


    
}
