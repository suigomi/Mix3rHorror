using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    public GameObject GameOverImage;
    float gameOverImageAlpha;
    float red, green, blue;

    // Start is called before the first frame update
    void Start()
    {
        sanValue = sanValueMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (sanValue <= 0)
        {
            GameOver();
        }
    }

    public void SubSanValue(int x) //san値からxだけ引く(Substruct)
    {
        sanValue = Mathf.Clamp(sanValue - x, sanValueMin, sanValueMax);//Min <= sanValue <= MaxになるようにClamp関数で制限
    }

    public void GameOver()
    {
        StartCoroutine("GameOverCoroutine");
    }

    IEnumerator GameOverCoroutine()
    {
        GameOverImage.SetActive(true);
        while(gameOverImageAlpha<1)
        {
            GameOverImage.GetComponent<Image>().color = new Color(1, 1, 1, gameOverImageAlpha);
            gameOverImageAlpha += 0.003f;
            yield return null;
        }
    }
}
