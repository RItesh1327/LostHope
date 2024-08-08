using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // speed at which the player moves
    public float turnSpeed = 180f; // speed at which the player turns
    void Update()
    {
        // get input for movement and turning
        float movementInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // move the player forward or backward based on movement input
        transform.position += transform.forward * movementInput * movementSpeed * Time.deltaTime;
        // turn the player left or right based on turn input
        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }
}