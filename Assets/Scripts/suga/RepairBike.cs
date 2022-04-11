using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RepairBike : MonoBehaviour
{

    [SerializeField]
    AssembleBikeObj[] assembleBikeObjs;

    



    private void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<BikeParts>().GetPartTag == BikeParts.Part.Wrench)
        {
            if (assembleBikeObjs.All(value => value.IsSetted))
            {
                foreach (AssembleBikeObj obj in assembleBikeObjs)
                {
                    obj.BikePart.Repair();
                }
            }
        }
    }

}
