using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Tooltip("Day or Night")]
    public bool day = true;


    [Tooltip("侵食度")]
    public int sanValue;
    [Tooltip("侵食度の最大値(ゲームスタート時の値)")]
    public int sanValueMax = 100;
    [Tooltip("侵食度の最小値(ゲームオーバーになる値)")]
    public int sanValueMin = 0;

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
        sanValue = Mathf.Clamp(sanValue - x, sanValueMin, sanValueMax);//Min <= sanValue <= MaxになるようにClamp関数で制限
    }

}
