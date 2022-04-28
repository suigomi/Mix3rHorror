using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AssembleBikeObj : MonoBehaviour
{

    XRSocketInteractor socket;

    [SerializeField]
    BikeParts.Part targetPart;

    bool isSetted = false;

    public BikeParts BikePart { get; private set; }


    public bool IsSetted{
        get{ return isSetted; }
    }

    // Start is called before the first frame update
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        InvokeRepeating("onHoBar", 0.01f, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHoBar()
    {
        IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
        if (objName != null&& objName.transform.GetComponent<BikeParts>()!=null)
        {
            BikeParts bikePart = objName.transform.GetComponent<BikeParts>();


            if (targetPart == bikePart.GetPartTag)
            {

                if((transform.position - objName.transform.position).sqrMagnitude == 0)
                {
                    this.BikePart = bikePart;

                    Destroy(objName.transform.GetComponent<XRGrabInteractable>());
                    Destroy(objName.transform.GetComponent<Rigidbody>());
                    Destroy(this.GetComponent<XRSocketInteractor>());
                    objName.transform.parent = this.transform.parent;

                    isSetted = true;
                }
                
            }
        }
        
    }
}
