using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetEngineMovement : MonoBehaviour
{
    // Rigidbody component for the player's jet
    public Rigidbody rb;

    // Oculus Touch controller input
    private OVRInput.Controller controller;

    // Speed of the jet's movement
    public float speed = 10.0f;

    // Force applied to the jet when it moves
    public float force = 1000.0f;

    // Maximum speed the jet can reach
    public float maxSpeed = 100.0f;

    // Minimum speed the jet can reach
    public float minSpeed = 0.0f;

    // Acceleration rate of the jet
    public float acceleration = 5.0f;

    // Deceleration rate of the jet
    public float deceleration = 5.0f;

    // Rotation speed of the jet
    public float rotationSpeed = 5.0f;

    // Boost multiplier for the jet's speed
    public float boostMultiplier = 2.0f;

    // Whether the boost button is being held down
    private bool boosting = false;

    // Update is called once per frame
    void Update()
    {
        // Get the current speed of the jet
        float currentSpeed = rb.velocity.magnitude;

        // Get the input from the Oculus Touch controllers
        controller = OVRInput.GetActiveController();

        // If the left joystick is being moved, rotate the jet
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).magnitude > 0.1f)
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
            Quaternion rot = Quaternion.Euler(new Vector3(0, joystickInput.x * rotationSpeed, 0));
            rb.MoveRotation(rb.rotation * rot);
        }

        // If the A button is pressed, boost the jet's speed
        if (OVRInput.Get(OVRInput.Button.One, controller))
        {
            boosting = true;
        }
        else
        {
            boosting = false;
        }

        // If the X button is pressed, decrease the jet's speed
        if (OVRInput.Get(OVRInput.Button.Two, controller))
        {
            if (currentSpeed > minSpeed)
            {
                rb.AddForce(-transform.forward * deceleration, ForceMode.Acceleration);
            }
        }

        // If the right joystick is being moved, accelerate or decelerate the jet
        if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, controller).magnitude > 0.1f)
        {
            Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, controller);
            if (joystickInput.y > 0 && currentSpeed < maxSpeed)
            {
                if (boosting)
                {
                    rb.AddForce(transform.forward * acceleration * boostMultiplier, ForceMode.Acceleration);
                }
                else
                {
                    rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
                }
            }
            else if (joystickInput.y < 0 && currentSpeed > minSpeed)
            {
                rb.AddForce(-transform.forward * deceleration, ForceMode.Acceleration);
            }
        }
    }
}

