using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketedItemSize : MonoBehaviour
{
    XRSocketInteractor socket;
    IXRSelectInteractable objName;
    Vector3 socketedObjectFirstSize;
    public static float MaxElementOfVector3(Vector3 v)
    {
        float[] element_value = new float[] { Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z) };
        return Mathf.Max(element_value);
    }
   
    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }
 
    public void socketFit()
    {
        objName = socket.GetOldestInteractableSelected();
        socketedObjectFirstSize = objName.transform.localScale;
       
        // Debug.Log(objName.transform.name + " in socket of " + transform.name);
        float meshSize = MaxElementOfVector3(gameObject.GetComponent<Renderer>().bounds.extents) / MaxElementOfVector3(objName.transform.gameObject.GetComponent<Renderer>().bounds.extents);
        Debug.Log(meshSize);
        objName.transform.localScale *= meshSize;
    }

    public void returnSize()
    {
        objName.transform.localScale = socketedObjectFirstSize;
    }
}
