using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine.XR.Interaction.Toolkit;

public class SpaceRangerController : MonoBehaviour
{
    public Camera MainCam;
    // CharacterController component for the player character
    public CharacterController cc;
    
    private XROrigin xROrigin;

    private CharacterControllerDriver driver;

    public SoundManager soundmanager;
    public AudioSource soundsource;
    public AudioClip soundclip;
    

    // Oculus Touch controller input
    private OVRInput.Controller controller;

    // The maximum oxygen level for the player character
    public float maxOxygen = 100.0f;

    // The current oxygen level for the player character
    public float oxygen;

    // The rate at which the player character consumes oxygen
    public float oxygenConsumptionRate = 5.0f;

    // The rate at which the player character replenishes oxygen
    public float oxygenReplenishmentRate = 10.0f;

    // The speed of the player character's movement
    public float speed = 10.0f;

    // Maximum speed the player character can reach
    public float maxSpeed = 100.0f;

    // Minimum speed the player character can reach
    public float minSpeed = 0.0f;

    // Acceleration rate of the player character
    public float acceleration = 5.0f;

    // Deceleration rate of the player character
    public float deceleration = 5.0f;

    // Rotation speed of the player character
    public float rotationSpeed = 5.0f;

    // Boost multiplier for the player character's speed
    public float boostMultiplier = 2.0f;

    // Whether the boost button is being held down
    private bool boosting = false;
    private bool gameBegins = false;
    // Whether the player character is on the ground
    private bool onGround = true;

    // The layer mask for the ground layer
    public LayerMask groundLayer;

    // The distance from the player character's feet to the ground
    public float groundDistance = 0.2f;

    // The force applied to the player character when it jumps
    public float jumpForce = 10.0f;

    // The UI text element that displays the oxygen level
    public TMP_Text oxygenText;

    public GameObject[] uiElements;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        xROrigin = GetComponent<XROrigin>();
        driver = GetComponent<CharacterControllerDriver>();
        gameBegins = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();
        if(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick,controller).magnitude > 0.1f)
        {
            soundmanager.PlayBGM(soundsource,soundclip);
        }
        // Get the current speed of the player character
       // float currentSpeed = cc.velocity.magnitude;

        // Get the input from the Oculus Touch controllers
        //controller = OVRInput.GetActiveController();

        //// If the left joystick is being moved, accelerate or decelerate the player character
        //if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).magnitude > 0.1f)
        //{
        //    Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller);
        //    if (joystickInput.y > 0 && currentSpeed < maxSpeed)
        //    {
        //        if (boosting)
        //        {
        //            cc.Move(transform.forward * acceleration * boostMultiplier * Time.deltaTime);
        //        }
        //        else
        //        {
        //            cc.Move(transform.forward * acceleration * Time.deltaTime);
        //        }
        //    }
        //    else if (joystickInput.y < 0 && currentSpeed > minSpeed)
        //    {
        //        cc.Move(-transform.forward * deceleration * Time.deltaTime);
        //    }
        //}
        //// If the right joystick is being moved, rotate the player character
        //if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, controller).magnitude > 0.1f)
        //{
        //    Vector2 joystickInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick, controller);
        //    Quaternion rot = Quaternion.Euler(new Vector3(0, joystickInput.x * rotationSpeed, 0));
        //    transform.rotation = transform.rotation * rot;
        //}

     

        //// Check if the player character is on the ground
        //onGround = Physics.Raycast(transform.position, -Vector3.up, groundDistance, groundLayer);

        //// If the Y button is pressed and the player character is on the ground, make it jump
        //if (OVRInput.Get(OVRInput.Button.One, controller) && onGround)
        //{
        //    cc.Move(Vector3.up * jumpForce * Time.deltaTime);
        //}

        if (gameBegins)
        {
            // Decrease the oxygen level over time
            oxygen -= oxygenConsumptionRate * Time.deltaTime;
            // If the oxygen level is less than 0, set it to 0
            if (oxygen < 0)
            {
                oxygen = 0;
            }

            // If the B button is pressed, replenish the oxygen level
            if (OVRInput.Get(OVRInput.Button.Four, controller))
            {
                oxygen += oxygenReplenishmentRate * Time.deltaTime;

                // If the oxygen level exceeds the maximum, set it to the maximum
                if (oxygen > maxOxygen)
                {
                    oxygen = maxOxygen;
                }
            }

            // Update the oxygen level text
            oxygenText.text = "Oxygen: " + oxygen.ToString("F0") + "%";
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            
        }
    }
    /// <summary>
    /// Updates the <see cref="CharacterController.height"/> and <see cref="CharacterController.center"/>
    /// based on the camera's position.
    /// </summary>
    protected virtual void UpdateCharacterController()
    {
        if (xROrigin == null || cc == null)
            return;

        var height = Mathf.Clamp(xROrigin.CameraInOriginSpaceHeight, driver.minHeight, driver.maxHeight);

        Vector3 center = xROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + cc.skinWidth;

        cc.height = height;
        cc.center = center;
    }

    public void uiEnable()
    {
        gameBegins = true;
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(true);
            if(uiElements[i].GetComponent<Canvas>().enabled == false)
            {
                uiElements[i].GetComponent<Canvas>().enabled = true;
            }
        }
       
        this.gameObject.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
    }
    public void uiEnable2()
    {
        MainCam.GetComponent<Camera>().enabled = true;
    }

}


