using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInFogSky : MonoBehaviour
{
    public GameObject FogSky;
    public Image fadePanel;
    private float fogRed, fogGreen, fogBlue, fogAlpha, fadeAlpha;
    public float fadeSpeed = 0.0025f;
    public float fogSpeed = 2.5f;
    private bool flag, sceneFlag;
    // Start is called before the first frame update
    void Start()
    {
        fogRed = FogSky.GetComponent<Renderer>().material.color.r;
        fogGreen = FogSky.GetComponent<Renderer>().material.color.g;
        fogBlue = FogSky.GetComponent<Renderer>().material.color.b;
        fogAlpha = FogSky.GetComponent<Renderer>().material.color.a;
        fadeAlpha = 0f;

        flag = false;
        sceneFlag = false;
    }

    void OnTriggerExit(Collider other)
    {
        flag = true;
        // FogSky.GetComponent<Renderer>().material.color = new Color(fogRed, fogGreen, fogBlue, 1.0f);
        // fogAlpha = 255;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag && fogAlpha < 1.0f)
        {
            fogAlpha += fadeSpeed;
            RenderSettings.fogEndDistance -= fogSpeed;
            FogSky.GetComponent<Renderer>().material.color = new Color(fogRed, fogGreen, fogBlue, fogAlpha);
        }
        else if(flag && fogAlpha >= 1)
        {
            sceneFlag = true;
        }
        if(sceneFlag)
        {
            fadeAlpha += fadeSpeed*2;
            fadePanel.color = new Color(0, 0, 0, fadeAlpha);
            if(fadeAlpha >= 1.0f)
            {
                SceneManager.LoadScene("HorrorScene 1");
            }
        }
    }
}
