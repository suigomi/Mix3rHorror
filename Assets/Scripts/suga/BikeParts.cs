using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BikeParts : MonoBehaviour
{

    [SerializeField]
    Mesh repairMesh;

    public enum Part
    {
        Tire,
        Tank,
        Wrench
    }

    [SerializeField]
    private Part part;


    public Part GetPartTag
    {
        get { return part; }
    }

    public void Repair()
    {
        GetComponent<MeshFilter>().mesh = repairMesh;
    }

}
