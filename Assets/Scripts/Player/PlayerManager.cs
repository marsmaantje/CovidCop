using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] public static PlayerManager instance;
    [SerializeField] public PlayerAnimationBehavior playerAnimationBehavior;
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerHealth playerHealth;
    [SerializeField] public PlayerAbilities playerAbilities;
    [SerializeField] public PlayerVisualBehaviour playerVisualBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
