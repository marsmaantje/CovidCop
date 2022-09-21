using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualBehaviour : MonoBehaviour
{

    MaterialPropertyBlock visualOverride;

    [SerializeField] Renderer playerRenderer;

    [SerializeField] PlayerHealth playerHealth;

    [SerializeField] Texture2D healthyTexture;
    [SerializeField] Texture2D sickTexture;


    // Start is called before the first frame update
    void Start()
    {
        if(playerRenderer != null)
        {
            visualOverride = new MaterialPropertyBlock();
            visualOverride.SetTexture("_BaseMap", healthyTexture);
            playerRenderer.SetPropertyBlock(visualOverride);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth.playerHealth < 0.3f)
        {
            visualOverride.SetTexture("_BaseMap", sickTexture);
            playerRenderer.SetPropertyBlock(visualOverride);
        }
        else
        {
            visualOverride.SetTexture("_BaseMap", healthyTexture);
            playerRenderer.SetPropertyBlock(visualOverride);
        }
    }
}
