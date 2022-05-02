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

    void Start()
    {
        XrOrign = GameObject.Find("XR Origin").GetComponent<ContinuousMoveProviderBase>();
        isrun = false;
        toggleReference.action.started += run;
    }

    

    private void run(InputAction.CallbackContext context)
    {
        isrun = !isrun;
        XrOrign.moveSpeed = (isrun) ? XrOrign.moveSpeed + 10 : XrOrign.moveSpeed - 10;
    }

    /*
    private ContinuousMoveProviderBase XrOrign;

    private TestPlayerActions testPlayerActions;
    // Start is called before the first frame update
    void Start()
    {
        XrOrign = GameObject.Find("XR Origin").GetComponent<ContinuousMoveProviderBase>();
    }

    void Awake()
    {
        testPlayerActions = new TestPlayerActions();
        testPlayerActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (testPlayerActions.Player.Run.WasPressedThisFrame())
        {
            XrOrign.moveSpeed += 10;
        }
        if (testPlayerActions.Player.Run.WasReleasedThisFrame())
        {
            XrOrign.moveSpeed -= 10;
        }
    }
    */
}
