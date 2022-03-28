using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class sample1 : MonoBehaviour
{


    XRSocketInteractor socket;
    
    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        InvokeRepeating("onHoBar", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHoBar()
    {
        IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
        if (objName.transform.tag == "Respawn")
        {
            Destroy(objName.transform.GetComponent<XRGrabInteractable>());
            Destroy(objName.transform.GetComponent<Rigidbody>());
            objName.transform.parent = this.transform.parent;
        }
    }

}
