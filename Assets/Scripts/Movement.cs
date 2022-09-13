using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Hoverer))]
public class Movement : MonoBehaviour
{
    #region publc variables
    [Header("Locomotion")]
    public Transform pivot;
    public float maxSpeed = 8;
    public float Acceleration = 10;
    public AnimationCurve AccelerationFactorFromDot;
    public float MaxAccelerationForce = 150;
    public AnimationCurve MaxAccelerationForceFactorFromDot;
    public Vector3 ForceScale = new Vector3(1, 0, 1);
    public float GravityScaleDrop = 10;

    [Header("Jumping")]
    public float JumpUpVelocity = 7.5f;
    public AnimationCurve JumpUpVelFactorFromExistingY;
    public AnimationCurve AnalogJumpUpForce;
    public float JumpTerminalVelocity = 22.5f;
    public float JumpDuration = 2 / 3f;
    public float CoyoteTimeThreshold = 1 / 3f;
    public float AutoJumpAfterLandThreshold = 1 / 3f;
    public float JumpFallFactor = 2;
    public float JumpSkipGroundCheckDuration = 0.5f;
    public float jumpInterval = 0.3f;

    [Header("Rotation")]
    public float rotationSpeed = 2;
    public bool viewDesiredDirection = false;
    public AnimationCurve rotationSpeedFactorFromMagnitude;
    public AnimationCurve rotationDampingCurve;

    #endregion

    #region private variables

    float m_MovementControlDisabledTimer = 0;

    Vector2 viewValue = Vector2.zero;
    Vector2 inputVelocity = Vector2.zero;

    bool pJump;
    bool jumpRequest = false;
    float lastJumpRequest = 0;
    float lastGroundContact = 0;
    float lastJump = 0;

    Rigidbody rb;
    Hoverer hoverer;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (pivot == null)
            pivot = transform;

        hoverer = GetComponent<Hoverer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region movement
        //map input from pivot to world space on xz plane
        Vector3 inputVel = new Vector3(inputVelocity.x, 0, inputVelocity.y);
        if (inputVel.magnitude > 1)
            inputVel.Normalize();

        Vector3 unitGoal = Quaternion.Euler(0, pivot.eulerAngles.y, 0) * inputVel;

        if (m_MovementControlDisabledTimer > 0)
        {
            unitGoal = Vector3.zero;
            m_MovementControlDisabledTimer -= Time.fixedDeltaTime;
        }

        Vector3 goalVel = unitGoal * maxSpeed;
        Vector3 unitVel = goalVel.normalized;

        float velDot = Vector3.Dot(unitGoal, unitVel);
        float accel = Acceleration * AccelerationFactorFromDot.Evaluate(velDot);
        float maxAccelForceFactor = MaxAccelerationForceFactorFromDot.Evaluate(velDot);

        goalVel = Vector3.MoveTowards(goalVel,
                                      (goalVel) + hoverer.groundVel,
                                      accel * Time.fixedDeltaTime);

        Vector3 neededAccel = (goalVel - rb.velocity) / Time.fixedDeltaTime;

        float maxAccel = MaxAccelerationForce * MaxAccelerationForceFactorFromDot.Evaluate(velDot) * maxAccelForceFactor;

        neededAccel = Vector3.ClampMagnitude(neededAccel, maxAccel);

        rb.AddForce(Vector3.Scale(neededAccel * rb.mass, ForceScale));
        #endregion

        #region rotation
        //rotate the player
        if (viewDesiredDirection)
        {
            Vector3 viewDirection = new Vector3(viewValue.x, 0, viewValue.y);

            //transform the input vector into world space
            Vector3 inputDir = Quaternion.Euler(0, pivot.eulerAngles.y, 0) * viewDirection;

            if (viewDirection.magnitude > 1) 
                viewDirection.Normalize();

                
            float rotationSpeedFactor = rotationSpeedFactorFromMagnitude.Evaluate(viewDirection.magnitude);

            float yAngleDifference = Vector3.SignedAngle(transform.forward, inputDir, Vector3.up);
            yAngleDifference = Mathf.Clamp(yAngleDifference, -rotationSpeed * rotationSpeedFactor, rotationSpeed * rotationSpeedFactor);

            float damping = 1 - rotationDampingCurve.Evaluate(Mathf.Abs(rb.angularVelocity.y / Time.fixedDeltaTime / rotationSpeed));
            yAngleDifference *= damping;

            rb.AddRelativeTorque(Vector3.up * yAngleDifference);
        }
        else
        {
            rb.AddRelativeTorque(0, viewValue.x * rotationSpeed, 0);
        }
        #endregion

        #region jumping
        if (jumpRequest && Time.time - lastJump > jumpInterval)
        {
            if (hoverer.IsOnGround || Time.time < lastGroundContact + CoyoteTimeThreshold)
            {
                //jump
                hoverer.isJumping = true;
                rb.AddForce(Vector3.up * JumpUpVelocity, ForceMode.VelocityChange);
                if (hoverer.rbGround != null)
                    hoverer.rbGround.AddForce(Vector3.up * (-JumpUpVelocity - rb.velocity.y), ForceMode.VelocityChange);
                lastJump = Time.time;
                jumpRequest = false;
            }

            if (Time.time + AutoJumpAfterLandThreshold > lastJumpRequest)
            {
                jumpRequest = false;
            }
        }
        #endregion

        #region misc
        //add extra gravity to the player
        rb.AddForce(Vector3.down * GravityScaleDrop * rb.mass);

        if (hoverer.IsOnGround)
            lastGroundContact = Time.time;
        #endregion
    }

    public void DoJump(bool pressed)
    {
        //call JumpStart, Jumping and JumpEnd based on pressed and pJump
        if (pressed && !pJump)
        {
            JumpStart();
        }
        else if (!pressed && pJump)
        {
            JumpEnd();
        }
        else if (pressed)
        {
            Jumping();
        }
        pJump = pressed;
    }

    void JumpStart()
    {
        jumpRequest = true;
        lastJumpRequest = Time.time;
    }

    void Jumping()
    {

    }

    void JumpEnd()
    {
        jumpRequest = false;
        hoverer.isJumping = false;
    }

    public void DoMove(Vector2 inputVel)
    {
        inputVelocity = inputVel;
    }

    public void DoView(Vector2 newViewValue)
    {
        viewValue = newViewValue;
    }
}
