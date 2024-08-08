using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimate : MonoBehaviour
{
    public InputActionProperty pinchAnim;
    public InputActionProperty gripAnim;
    public Animator handanimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float triggerVal =  pinchAnim.action.ReadValue<float>();
        handanimator.SetFloat("Trigger", triggerVal);
        float gripVal = gripAnim.action.ReadValue<float>();
        handanimator.SetFloat("Grip", gripVal);
    }
}
