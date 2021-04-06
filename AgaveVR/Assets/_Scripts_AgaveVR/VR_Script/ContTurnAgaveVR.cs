using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
public class ContTurnAgaveVR : SnapAgaveBase
{
    public InputActionProperty m_RightHandSnapTurnAction;

    public InputActionProperty rightHandSnapTurnAction
    {
        get => m_RightHandSnapTurnAction;
        set => SetInputActionProperty(ref m_RightHandSnapTurnAction, value);
    }
    protected void OnEnable()
    {
       
        m_RightHandSnapTurnAction.EnableDirectAction();
    }

    protected void OnDisable()
    {
       
        m_RightHandSnapTurnAction.DisableDirectAction();
    }

    protected override Vector2 ReadInput()
    {
       
        var rightHandValue = m_RightHandSnapTurnAction.action?.ReadValue<Vector2>() ?? Vector2.zero;

        return rightHandValue;
    }

    void SetInputActionProperty(ref InputActionProperty property, InputActionProperty value)
    {
        if (Application.isPlaying)
            property.DisableDirectAction();

        property = value;

        if (Application.isPlaying && isActiveAndEnabled)
            property.EnableDirectAction();
    }
}
