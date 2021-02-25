using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR;


[Serializable] public class GrabEvent : UnityEvent { }
public class GrabButtonInteration : MonoBehaviour
{

    public InputActionProperty grabAction;


    public GrabEvent GrabButtonPressed;
    public GrabEvent GrabButtonRel;


    private void Awake()
    {
        grabAction.action.performed += OnGrabAction;
    }
    private void OnDisable()
    {
        grabAction.action.performed -= OnGrabAction;
    }
    private void OnGrabAction(InputAction.CallbackContext context)
    {

       // Debug.Log(context);
        bool pressed = context.ReadValueAsButton();

        if (pressed)
        {
            GrabButtonPressed.Invoke();
        }
        else
        {
            GrabButtonRel.Invoke();
        }


    }
}
