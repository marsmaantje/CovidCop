using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCBehavior prefab;

    [field: SerializeField]
    public List<NPCBehavior> NPCList { get; private set; }

    [field: SerializeField]
    public List<Transform> GatheringPoints { get; private set; } = new List<Transform>();


    [SerializeField] public int InitialNPCSpawnCount;

    public static NPCManager instance;

    [Header("NPC Spawn Settings")]
    [SerializeField] private float spawnMinRadius;
    [SerializeField] private float spawnMaxRadius;
    [SerializeField] private Transform spawnOrigin;


    [Range(0, 1)]
    [SerializeField] private float infectedOnSpawnChance = 1f;



    public Dictionary<Collider, NPCBehavior> npcColliders = new Dictionary<Collider, NPCBehavior>();


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        NPCList = new List<NPCBehavior>(InitialNPCSpawnCount);

        for (int i = 0; i < InitialNPCSpawnCount; i++)
            spawnNPC();



    }

    void spawnNPC()
    {
        Vector2 flatSpawnPos = Random.insideUnitCircle.normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
        Vector3 spawnPos = new Vector3(flatSpawnPos.x, 0, flatSpawnPos.y) + spawnOrigin.position;
        NPCBehavior npc = Instantiate(prefab, spawnPos, Quaternion.identity);
        npcColliders.Add(npc.collider, npc);
        NPCList.Add(npc);

        if (Random.value < infectedOnSpawnChance)
            npc.infected = true;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (spawnOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spawnOrigin.position, spawnMinRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnOrigin.position, spawnMaxRadius);
        }
    }

    //on draw gizmo, draw spawn radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (Transform t in GatheringPoints)
        {
            Gizmos.DrawSphere(t.position, 1);
        }
    }

    public NPCBehavior GetNPC(Collider collider)
    {
        if (npcColliders.ContainsKey(collider))
            return npcColliders.GetValueOrDefault(collider);

        return null;
    }
}
