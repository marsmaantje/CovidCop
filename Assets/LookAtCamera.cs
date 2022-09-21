using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform target;
    [SerializeField]Quaternion offset;

    // Start is called before the first frame update
    void Start()
    {
        //set camera to the main camera transform
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //set rotation to look at the target
        transform.LookAt(target);
        transform.rotation = transform.rotation * offset;
    }
}
