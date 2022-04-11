using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class event_demo_receiver : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<event_demo_GM>().myEvent +=onReceive;    
    }


    void onReceive(string a)
    {
        print(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
