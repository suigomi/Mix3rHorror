using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerRun : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    private ContinuousMoveProviderBase XrOrign;
    private bool isrun;

    [SerializeField] int speed = 3;

    void Start()
    {
        XrOrign = GameObject.Find("XR Origin").GetComponent<ContinuousMoveProviderBase>();
        isrun = false;
        toggleReference.action.started += run;
    }

    

    private void run(InputAction.CallbackContext context)
    {
        isrun = !isrun;
        XrOrign.moveSpeed = (isrun) ? XrOrign.moveSpeed + speed : XrOrign.moveSpeed - speed;
    }
}
