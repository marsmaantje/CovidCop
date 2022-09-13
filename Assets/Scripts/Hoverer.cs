using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

public class Hoverer : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 groundVel { get; private set; } = Vector3.zero;
    public bool isJumping = false;

    [SerializeField]
    [Range(0, 2)]
    float _rideHeight = 1;
    readonly float _rideSpringStrength = 150;
    readonly float _stickHeight = 0.5f;
    readonly float _rideSpringDamper = 8;

    readonly float _uprightSpringStrength = 20;
    readonly float _uprightSpringDamper = 0.1f;

    bool isOnGround = false;
    public bool IsOnGround { get => isOnGround; }

    //beware can be null
    public Rigidbody rbGround { get; private set; } = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //movement
        HoverForce();
        UprightForce();
    }

    private void HoverForce()
    {
        //raycast down to check if we are grounded

        Vector3 rayDir = Vector3.down;
        RaycastHit hit;
        bool rayHit = Physics.Raycast(transform.position, rayDir, out hit, _rideHeight + (isJumping ? 0 : _stickHeight));
        isOnGround = rayHit;
        rbGround = hit.rigidbody;

        if (rayHit)
        {
            //our velocity
            Vector3 vel = rb.velocity;

            //ground velocity
            Vector3 otherVel = Vector3.zero;
            Rigidbody hitBody = hit.rigidbody;
            if (hitBody != null)
            {
                otherVel = hitBody.velocity;
            }
            groundVel = otherVel;

            //relative velocity to our raycast
            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, otherVel);

            //relative up velocity to our ground
            float relVel = rayDirVel - otherDirVel;

            //distance to our desired height
            float x = hit.distance - _rideHeight;

            //force we are going to apply
            float springForce = (x * _rideSpringStrength) - (relVel * _rideSpringDamper);

            //debug
            Debug.DrawLine(transform.position, transform.position + rayDir * (_rideHeight + _stickHeight), Color.red);

            Debug.DrawRay(hit.point, hit.normal, Color.green);

            //applying the force, compensating for our mass
            rb.AddForce(rayDir * springForce * rb.mass);

            //if we can, add the force oposite to the ground so it actually reacts to us standing on it
            if (hitBody != null)
            {
                hitBody.AddForceAtPosition(-rayDir * springForce * rb.mass, hit.point);
            }
        }
    }

    private void UprightForce()
    {
        Quaternion characterCurrent = transform.rotation;
        //current up direction
        Vector3 up = transform.up;
        //target up direction
        Vector3 targetUp = Vector3.up;
        //rotation to get to target up direction
        Quaternion toGoal = Quaternion.FromToRotation(up, targetUp);

        Vector3 rotAxis;
        float rotDegrees;

        toGoal.ToAngleAxis(out rotDegrees, out rotAxis);
        rotAxis.Normalize();

        float rotRadians = rotDegrees * Mathf.Deg2Rad;

        rb.AddTorque((rotAxis * (rotRadians * _uprightSpringStrength)) - (rb.angularVelocity * _uprightSpringDamper));
    }
}
