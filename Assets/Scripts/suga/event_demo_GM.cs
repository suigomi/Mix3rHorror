using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// イベント処理用のデリゲート
public delegate void TestEventHandler(string name);

public class event_demo_GM : MonoBehaviour
{


    public TestEventHandler myEvent; 


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("onEvent", 1, 1);
    }

    string a = "a";

    // Update is called once per frame
    void onEvent()
    {
        myEvent(a);
        a = a + "a";
    }



}
