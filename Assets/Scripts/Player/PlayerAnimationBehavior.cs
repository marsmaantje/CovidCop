using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationBehavior : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MovementSpeed", rb.velocity.magnitude);
        animator.SetBool("IsSick", playerHealth.playerHealth < 0.3f);
    }
}
