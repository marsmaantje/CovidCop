using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class NPCBehavior : MonoBehaviour
{
    [SerializeField] public Movement movement;

    [SerializeField] private float minWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private float minWalkTime;
    [SerializeField] private float maxWalkTime;
    [SerializeField] private float maxWalkTargetDistance;
    [SerializeField] public NPCState currentState = NPCState.Waiting;
    private NPCState previousState = NPCState.Walking;
    public Rigidbody rigidbody;
    public Collider collider;

    private float waitTime;
    private float waitStart;

    private float walkTimeLimit;
    private float walkTimeStart;
    private Transform walkTarget;

    public MaterialPropertyBlock materialPropertyBlock;
    public Renderer renderer;

    public bool infected = false;
    bool pInfected = false;

    private float lastHousedTime = float.MinValue;

    public Action<NPCBehavior> OnInfected;
    public Action<NPCBehavior> OnCured;

    // Start is called before the first frame update
    void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        renderer.SetPropertyBlock(materialPropertyBlock);
    }

    // Update is called once per frame
    void Update()
    {
        if (infected != pInfected)
        {
            materialPropertyBlock.SetColor("_BaseColor", infected ? Color.green : Color.blue);
            renderer.SetPropertyBlock(materialPropertyBlock);
            pInfected = infected;
            if(infected)
            {
                OnInfected?.Invoke(this);
            }
            else
            {
                OnCured?.Invoke(this);
            }
        }

        if (currentState != previousState)
            OnStateChange(previousState, currentState);
        previousState = currentState;

        switch (currentState)
        {
            case NPCState.Waiting:
                if (Time.time - waitStart > waitTime)
                {
                    currentState = NPCState.Walking;
                }
                movement.DoMove(Vector2.zero);
                break;
            case NPCState.Walking:
                if (Time.time - walkTimeStart > walkTimeLimit || Vector3.Distance(transform.position, walkTarget.position) < maxWalkTargetDistance)
                {
                    currentState = NPCState.Waiting;
                }
                else
                {
                    Vector3 relativeTarget = walkTarget.position - transform.position;
                    //rotate relativeTarget to be relative to the NPC
                    relativeTarget = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0) * relativeTarget;
                    movement.DoMove(new Vector2(relativeTarget.x, relativeTarget.z));
                }
                break;
            case NPCState.Housed:
                gameObject.SetActive(false);
                break;
            case NPCState.Influenced:

                break;
            case NPCState.Lockdown:
                startLockdown();
                break;
            default:
                Debug.LogError("NPCState not implemented");
                break;
        }

    }

    void startLockdown()
    {
        List<House> houses = new List<House>(HouseManager.instance.houses);
        
        houses.RemoveAll(house => house.IsHospital);

        // Rank houses on distance from NPC
        houses.Sort((a, b) => Vector3.Distance(a.transform.position, transform.position).CompareTo(Vector3.Distance(b.transform.position, transform.position)));
        // Find the closest house that has space
        House closestHouse = null;
        foreach (House house in houses)
        {
            if (house.getHousedNPCs().Count < house.HouseCapacity)
            {
                closestHouse = house;
                break;
            }
        }

        if (closestHouse != null)
        {
            closestHouse.addLockdownNPC(this);
            // Move to the house
            Vector3 relativeTarget = closestHouse.transform.position - transform.position;
            //rotate relativeTarget to be relative to the NPC
            relativeTarget = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0) * relativeTarget;
            movement.DoMove(new Vector2(relativeTarget.x, relativeTarget.z));
            // Add to the house
        }
        else
        {
            // No house found, just wait
            currentState = NPCState.Waiting;
        }


    }

    

    void OnStateChange(NPCState oldState, NPCState newState)
    {
        switch (newState)
        {
            case NPCState.Walking:
                walkTimeLimit = UnityEngine.Random.Range(minWalkTime, maxWalkTime);
                walkTimeStart = Time.time;

                //pick a target at random
                walkTarget = NPCManager.instance.GatheringPoints[UnityEngine.Random.Range(0, NPCManager.instance.GatheringPoints.Count)];
                break;
            case NPCState.Waiting:
                waitTime = UnityEngine.Random.Range(minWaitTime, maxWaitTime);
                waitStart = Time.time;

                break;
            case NPCState.Housed:
                gameObject.SetActive(false);
                break;
            case NPCState.Influenced:

                break;
            case NPCState.Lockdown:
                break;
            default:
                Debug.LogError("NPCState not implemented");

                break;
        }
    }

    public void StopInfluence()
    {
        if (currentState == NPCState.Influenced)
        {
            currentState = NPCState.Waiting;
        }
    }

    public void StopLockdown()
    {
        if (currentState == NPCState.Lockdown)
        {
            currentState = NPCState.Waiting;
        }
    }

    // Setter for state
    public void SetState(NPCState state)
    {
        if (gameObject.activeSelf)
        {
            currentState = state;
            OnStateChange(previousState, currentState);
        }
    }

    public float getLastHousedTime()
    {
        return this.lastHousedTime;
    }

    public void setLastHousedTime(float time)
    {
        this.lastHousedTime = time;
    }

    public enum NPCState
    {
        Waiting,
        Walking,
        Influenced,
        Housed,
        Lockdown
    }



}
