using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBrokenBike : MonoBehaviour
{
    [SerializeField] GameObject repairedBike;
    [SerializeField] GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount >= 1)
        {
            Destroy(this.transform.root.gameObject);
            repairedBike.SetActive(true);
            Enemy.SetActive(true);
        }
    }
}
