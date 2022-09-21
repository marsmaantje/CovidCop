using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVisualBehavior : MonoBehaviour
{
    [SerializeField] Renderer visual;
    public List<Texture2D> HealthyVisuals;
    public List<Texture2D> SickVisuals;
    //material property block
    MaterialPropertyBlock visualOverride;

    public GameObject particleEffect;

    public NPCBehavior npc;

    private int visualIndex;

    private bool pInfected = false;
    
    // Start is called before the first frame update
    void Start()
    {
        visualIndex = Random.Range(0, Mathf.Min(HealthyVisuals.Count, SickVisuals.Count));
        if(visual != null)
        {
            visualOverride = new MaterialPropertyBlock();
            visualOverride.SetTexture("_BaseMap", HealthyVisuals[visualIndex]);
            visual.SetPropertyBlock(visualOverride);
        }

        if (particleEffect != null)
        {
            particleEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(npc.infected != pInfected)
        {//our infected state changed, we should update the visuals
            pInfected = npc.infected;
            if (visual != null)
            {
                visualOverride.SetTexture("_BaseMap", pInfected ? SickVisuals[visualIndex] : HealthyVisuals[visualIndex]);
                visual.SetPropertyBlock(visualOverride);
            }
            if (particleEffect != null)
            {
                particleEffect.SetActive(pInfected);
            }

        }
    }
}
