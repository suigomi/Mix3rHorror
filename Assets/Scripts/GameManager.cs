using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //昼か夜か
    public bool day = true;

    //侵食度
    public int sanValue;
    public int sanValueMax = 100; //初期値が100
    public int sanValueMin = 0; //0になるとゲームオーバー

    // Start is called before the first frame update
    void Start()
    {
        sanValue = sanValueMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubSunValue(int x) //san値からxだけ引く(Substruct)
    {
        sanValue -= x;
        sanValue = Mathf.Max(sanValue, sanValueMin);
    }

}
