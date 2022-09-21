using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationScript : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
            Debug.LogError("Animator not found");
        if (rb == null)
            Debug.LogError("Rigidbody not found");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("MovementSpeed", rb.velocity.magnitude);
    }
}
