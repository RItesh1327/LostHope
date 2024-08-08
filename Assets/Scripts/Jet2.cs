using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Jet2 : MonoBehaviour
{
    // Rigidbody component for the player's jet
    public Rigidbody rb;
    // Speed of the jet's movement
    public float speed = 10.0f;

    // Force applied to the jet when it moves
    public float force = 1000.0f;

    // Maximum speed the jet can reach
    public float maxSpeed = 100.0f;

    // Minimum speed the jet can reach
    public float minSpeed = 0.0f;

    // Rotation speed of the jet
    public float rotationSpeed = 5.0f;

    // Torque applied to the jet to slow down its rotation
    public float angularDrag = 1000.0f;

    // Boost force applied to the jet when boosting
    public float boostForce = 10000.0f;

    // Duration of the boost
    public float boostDuration = 1.0f;

    // Timer for the boost
    private float boostTimer = 0.0f;

    // Flag to track if the boost is active
    private bool isBoosting = false;

    public TMP_Text speedText;
    public TMP_Text positionText;
    public TMP_Text rotationText;

    public float fillAmount = 0;
    public float fillRate = 5;
    public float requiredSpeed = 50;

    // Update is called once per frame
    void Update()
    {
        // Get the current speed of the jet
        float currentSpeed = rb.velocity.magnitude;

        // Get the input from the Oculus Touch controllers
        Vector2 leftThumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 rightThumbstickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        bool buttonOneInput = OVRInput.GetDown(OVRInput.Button.One);


        
        
        // If the boost button is pressed, activate the boost
        if (buttonOneInput && !isBoosting)
        {
            // Set the boost timer to the boost duration
            boostTimer = boostDuration;
            // Set the isBoosting flag to true
            isBoosting = true;
        }

        // If the boost is active
        if (isBoosting)
        {
            // Decrement the boost timer
            boostTimer -= Time.deltaTime;

            // Apply the boost force to the jet
            rb.AddForce(transform.forward * boostForce, ForceMode.Acceleration);

            // If the boost timer has expired
            if (boostTimer <= 0.0f)
            {
                // Set the isBoosting flag to false
                isBoosting = false;
            }
        }

        // If the left thumbstick is being moved, move the jet in the direction of the thumbstick input
        if (leftThumbstickInput.magnitude > 0.1f && !isBoosting)
        {
            // Calculate the movement direction based on the thumbstick input and the jet's current rotation
            Vector3 movementDirection = transform.TransformDirection(new Vector3(leftThumbstickInput.x, 0, leftThumbstickInput.y));
            // Normalize the movement direction so that the jet moves at a consistent speed regardless of the angle of the thumbstick
            movementDirection = movementDirection.normalized;
            // Apply the movement force to the jet
            rb.AddForce(movementDirection * force, ForceMode.Acceleration);
        }

        else
        {
            // Gradually decrease the jet's velocity to zero over 2 seconds
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime / 2.0f);
        }

        // If the right thumbstick is being moved, rotate the jet
        if (rightThumbstickInput.magnitude > 0.1f)
        {
            // Rotate the jet based on the vertical input of the right thumbstick
            Quaternion rot = Quaternion.Euler(new Vector3(-rightThumbstickInput.y * rotationSpeed, rightThumbstickInput.x * rotationSpeed, 0));
            rb.MoveRotation(rb.rotation * rot);
        }

        // Apply a torque to the jet to slow down its rotation
        rb.AddTorque(-rb.angularVelocity * angularDrag * Time.deltaTime, ForceMode.Acceleration);

        // Update the UI text elements with the current speed, position, and rotation of the jet
        if (SceneManager.GetActiveScene().name == "titan")
        {
            speedText.text = "Speed: " + ((rb.velocity.magnitude) * 5).ToString("F0");
            positionText.text = "FillAmount: " + fillAmount.ToString("F0");
            //rotationText.text = "Rotation: " + transform.rotation.eulerAngles.ToString("F2");
        }
        if(fillAmount >= 100)
        {
           StartCoroutine(sceneChange());
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            fillAmount += fillRate * Time.deltaTime;
            // Clamp the fill amount between 0 and 100
            fillAmount = Mathf.Clamp(fillAmount, 0.0f, 100.0f);
            rotationText.text = "Collecting Nitrogen";
        }
        else
        {
            rotationText.text = "Get closer to the ground ";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check the collider
        if (other.transform.tag == "valley")
        {
            // Check if the jet is travelling above the required speed
            
                // Increase the fill amount at the fill rate
               
            
        }

        rotationText.text = "Collecting Nitrogen";
    }

    public IEnumerator sceneChange()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnTriggerExit(Collider other)
    {
       
    }
}
