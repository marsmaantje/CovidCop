using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public NPCBehavior prefab;

    [field: SerializeField]
    public List<NPCBehavior> NPCList { get; private set; }

    [SerializeField] public int InitialNPCSpawnCount;

    public static NPCManager instance;

    [Header("NPC Spawn Settings")]
    [SerializeField] private float spawnMinRadius;
    [SerializeField] private float spawnMaxRadius;
    [SerializeField] private Transform spawnOrigin;


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
        NPCList.Add(npc);

    }

    // Update is called once per frame
    void Update()
    {

    }

    //on draw gizmo, draw spawn radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnOrigin.position, spawnMinRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(spawnOrigin.position, spawnMaxRadius);
    }
}
