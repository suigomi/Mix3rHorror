using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerRun : MonoBehaviour
{
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
}
