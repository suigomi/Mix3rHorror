using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class PlayerRun : MonoBehaviour
{
    private ContinuousMoveProviderBase XrOrign;
    // Start is called before the first frame update
    void Start()
    {
        XrOrign = GameObject.Find("XR Origin").GetComponent<ContinuousMoveProviderBase>();
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
