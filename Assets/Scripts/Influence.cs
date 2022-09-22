using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;





public class Influence : MonoBehaviour
{
    [SerializeField] private bool colliderBased = false;
    [SerializeField] new private Collider collider;
    [SerializeField] private float influenceRadius = 10f;

    [SerializeField] private List<NPCBehavior> influenced = new List<NPCBehavior>();

    [SerializeField] private bool isPushing = false;
    [SerializeField] private bool isPulling = false;
    [SerializeField] private Material InfluenceEffectMaterial;
    [ColorUsage(true, true)]
    [SerializeField] private Color pushColor = Color.red;
    [ColorUsage(true, true)]
    [SerializeField] private Color pullColor = Color.blue;
    [SerializeField] private float EffectSpeed = 1;

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip pushSound;
    [SerializeField] private AudioClip pullSound;




    // Start is called before the first frame update
    void Start()
    {
    }


    public void OnPush(InputAction.CallbackContext context)
    {
        if (!isPushing)
        {
            audioSource.clip = pushSound;
            audioSource.Play();
        }

        isPushing = true;
        isPulling = false;
        InfluenceEffectMaterial.SetColor("_CircleColor", pushColor);
        InfluenceEffectMaterial.SetFloat("_Speed", EffectSpeed);


    }

    public void OnPull(InputAction.CallbackContext context)
    {

        if (!isPulling)
        {
            audioSource.clip = pullSound;
            audioSource.Play();
        }

        isPulling = true;
        isPushing = false;
        InfluenceEffectMaterial.SetColor("_CircleColor", pullColor);
        InfluenceEffectMaterial.SetFloat("_Speed", -EffectSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPushing || isPulling)
        {
            if (!colliderBased)
            {
                foreach (NPCBehavior npc in NPCManager.instance.NPCList)
                {
                    if (GetNPCDistance(npc) < influenceRadius)
                    {

                        Vector3 direction = transform.position - npc.transform.position;

                        direction.Normalize();

                        //rotate direction to be relative to the npc
                        direction = npc.transform.InverseTransformDirection(direction);

                        npc.movement.DoMove(new Vector2(direction.x, direction.z) * (isPushing ? 1 : -1));
                        npc.SetState(NPCBehavior.NPCState.Influenced);
                    }
                    else
                    {
                        npc.movement.DoMove(Vector2.zero);
                        npc.StopInfluence();
                    }
                }
            }
            else
            { //use the collider for npc management
                List<NPCBehavior> toRemove = new List<NPCBehavior>();
                foreach (NPCBehavior npc in influenced)
                {
                    Vector3 direction = transform.position - npc.transform.position;

                    direction.Normalize();

                    //rotate direction to be relative to the npc
                    direction = npc.transform.InverseTransformDirection(direction);

                    npc.movement.DoMove(new Vector2(direction.x, direction.z) * (isPushing ? 1 : -1));
                    npc.SetState(NPCBehavior.NPCState.Influenced);

                    if (!npc.gameObject.activeSelf)
                    {
                        toRemove.Add(npc);
                    }

                }
                foreach (NPCBehavior npc in toRemove)
                {
                    influenced.Remove(npc);
                    npc.movement.DoMove(Vector2.zero);
                    npc.StopInfluence();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NPCBehavior npc = NPCManager.instance.GetNPC(other);
        if (npc != null && !influenced.Contains(npc))
            influenced.Add(npc);
    }

    private void OnTriggerExit(Collider other)
    {
        NPCBehavior npc = NPCManager.instance.GetNPC(other);
        if (npc != null && influenced.Contains(npc))
        {
            influenced.Remove(npc);
            npc.StopInfluence();
        }
    }

    private float GetNPCDistance(NPCBehavior npc)
    {
        return Vector3.Distance(transform.position, npc.transform.position);
    }
}