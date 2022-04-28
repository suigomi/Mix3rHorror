using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BrokenParts : MonoBehaviour
{


    public GameObject separationObj;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Wrench>() != null)
        {
            Instantiate(separationObj, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
