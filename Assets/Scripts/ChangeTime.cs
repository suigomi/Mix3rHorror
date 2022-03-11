using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeTime : MonoBehaviour
{
    public Material daySky;
    public Material nightSky;
    public GameObject dayLight;
    public GameObject nightLight;
    bool day = false;
    // Start is called before the first frame update
    void Start()
    {
        TimeReversal();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            TimeReversal();
        }
    }

    public void TimeReversal()
    {
        day = !day;
        if (day)
        {
            RenderSettings.skybox = daySky;
            RenderSettings.sun = dayLight.GetComponent<Light>();
            dayLight.SetActive(true);
            nightLight.SetActive(false);
        }
        else
        {
            RenderSettings.skybox = nightSky;
            RenderSettings.sun = nightLight.GetComponent<Light>();
            dayLight.SetActive(false);
            nightLight.SetActive(true);
        }
    }
}
