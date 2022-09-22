using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCBehavior prefab;

    [field: SerializeField]
    public List<NPCBehavior> NPCList { get; private set; }
    public List<NPCBehavior> InfectedNPCList { get; private set; }
    public int InfectedCount { get => InfectedNPCList.Count; }

    [field: SerializeField]
    public List<Transform> GatheringPoints { get; private set; } = new List<Transform>();


    [SerializeField] public int InitialNPCSpawnCount;

    public static NPCManager instance;

    [Header("NPC Spawn Settings")]
    [SerializeField] private bool spawnOnGatheringPoints = false;
    [SerializeField] private float spawnMinRadius;
    [SerializeField] private float spawnMaxRadius;
    [SerializeField] private Transform spawnOrigin;

    [SerializeField] private int InfectedCountOnStart = 5;



    public Dictionary<Collider, NPCBehavior> npcColliders = new Dictionary<Collider, NPCBehavior>();


    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("NPCManager Start");

        if (instance == null)
            instance = this;

        NPCList = new List<NPCBehavior>(InitialNPCSpawnCount);
        InfectedNPCList = new List<NPCBehavior>(InitialNPCSpawnCount);

        for (int i = 0; i < InitialNPCSpawnCount; i++)
            spawnNPC();

        for (int i = 0; i < InfectedCountOnStart; i++)
        {
            NPCList[i].infected = true;
            InfectedNPCList.Add(NPCList[i]);
        }

    }

    void spawnNPC()
    {
        Vector3 spawnPos;
        if (spawnOnGatheringPoints)
        {
            Vector3 targetPos = GatheringPoints[Random.Range(0, GatheringPoints.Count)].position;
            Vector2 spawnOffset = Random.insideUnitCircle.normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
            spawnPos = new Vector3(targetPos.x + spawnOffset.x, 0, targetPos.z + spawnOffset.y);
        }
        else
        {
            Vector2 flatSpawnPos = Random.insideUnitCircle.normalized * Random.Range(spawnMinRadius, spawnMaxRadius);
            spawnPos = new Vector3(flatSpawnPos.x, 0, flatSpawnPos.y) + spawnOrigin.position;
        }
        NPCBehavior npc = Instantiate(prefab, spawnPos, Quaternion.identity);
        npcColliders.Add(npc.collider, npc);
        NPCList.Add(npc);

        npc.OnInfected += OnNPCInfected;
        npc.OnCured += OnNPCCured;
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

        if (spawnOnGatheringPoints && GatheringPoints != null && GatheringPoints.Count > 0)
        {
            foreach (Transform point in GatheringPoints)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(point.position, spawnMinRadius);
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(point.position, spawnMaxRadius);
            }

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

    void OnNPCInfected(NPCBehavior npc)
    {
        if (!InfectedNPCList.Contains(npc))
        {
            InfectedNPCList.Add(npc);
        }
    }

    void OnNPCCured(NPCBehavior npc)
    {
        if (InfectedNPCList.Contains(npc))
        {
            InfectedNPCList.Remove(npc);
        }
    }
}
