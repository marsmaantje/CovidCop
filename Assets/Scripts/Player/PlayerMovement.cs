using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public Movement movement;
    bool isMouse = false;
    public PhysicMaterial deathMaterial;
    new public Collider collider;
    public PlayerInput input;
    Gamepad controller;

    public float maxLowRumble = 0.4f;
    public float maxHighRumble = 0.6f;
    public float rumbleFaloffTime = 1;
    float lastDamage = -100;

    // Start is called before the first frame update
    void Start()
    {
        if (movement == null) Debug.LogError("No movement script attached to player");
        if (collider == null) Debug.LogError("No collider attached to player");
        if (input == null) Debug.LogError("No player input attached to player");
        
        if (input != null && input.user.controlScheme.HasValue)
            isMouse = input.user.controlScheme.Value.name == "Keyboard and Mouse";

        if (!isMouse && input != null)
            controller = (Gamepad)input.user.pairedDevices[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (isMouse)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            //raycast from mouse position to ground
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000))
            {
                //get relative position from hit to player
                Vector3 relativePosition = hit.point - transform.position;
                relativePosition.Normalize();
                //rotate relativePosition with camera rotation
                relativePosition = Quaternion.Euler(0, -Camera.main.transform.eulerAngles.y, 0) * relativePosition;
                movement.DoView(new Vector2(relativePosition.x, relativePosition.z));
            }
        }
        else
        {
            //haptics on controller
            if (controller != null)
            {
                float timeSinceLastDamage = Time.time - lastDamage;
                float progress = Mathf.Clamp01(timeSinceLastDamage / rumbleFaloffTime);
                controller.SetMotorSpeeds(Mathf.Lerp(maxLowRumble, 0, progress), Mathf.Lerp(maxHighRumble, 0, progress));
            }
        }
    }

    //when damaged, rumble controller
    void OnDamage(float newHealth)
    {
        if (!isMouse)
        {
            lastDamage = Time.time;
        }
    }

    //on view event
    public void OnView(InputAction.CallbackContext context)
    {
        if (!isMouse)
        {
            Vector2 newViewDelta = context.ReadValue<Vector2>();
            movement.DoView(newViewDelta);
        }
    }

    //on move event
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVel = context.ReadValue<Vector2>();
        movement.DoMove(inputVel);
    }

    //on jump event
    public void OnJump(InputAction.CallbackContext context)
    {
        movement.DoJump(context.started || context.performed);
    }
}
