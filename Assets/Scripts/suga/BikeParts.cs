using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BikeParts : MonoBehaviour
{


    public enum Part
    {
        Tire,
        Tank
    }

    [SerializeField]
    private Part part;


    public Part GetPartTag
    {
        get { return part; }
    }


}
