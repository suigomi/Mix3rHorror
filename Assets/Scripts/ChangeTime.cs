using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ChangeTime : MonoBehaviour
{
    //変更するskybox(Exposureを変える)
    public Material skyBox;
    //Materialは再生するごとに変わって色が変わってしまうのでCopyを使用
    public Material skyBoxCopy;
    //昼のExposure
    [SerializeField] float dayExposure;
    //夜のExposure
    [SerializeField] float nightExposure = 0;
    //一回のExposureの変化量
    [SerializeField] float dE;


    //変更するlight(Colorを変える)
    public GameObject lightObj;
    //昼のColor
    [SerializeField] Color dayColorL;
    //夜のColor
    [SerializeField] Color nightColorL = Color.black;
    //一回のlightの変化量
    [SerializeField] private Color dCL;

    //昼のfogのdensity
    [SerializeField] float dayFog;
    //夜のfogのdensity
    [SerializeField] float nightFog = 0;
    //一回のdensityの変化量
    [SerializeField] float dF;

    //昼のfogのcolor
    [SerializeField] Color dayColorF;
    //夜のfogのcolor
    [SerializeField] Color nightColorF = Color.black;
    //一回のcolorの変化量
    [SerializeField] Color dCF;



    //何回で真っ暗にするか
    public int step = 10;
    //何回夜になったか
    private int numNight = 0;

    //昼か夜か
    [SerializeField] bool day = true;


    // Start is called before the first frame update
    void Start()
    {
        dayExposure = skyBox.GetFloat("_Exposure");
        dE = (dayExposure - nightExposure) / step;

        dayColorL = lightObj.GetComponent<Light>().color;
        dCL = (dayColorL - nightColorL) / step;

        dayFog = RenderSettings.fogDensity;
        dF = (dayFog - nightFog) / step;

        dayColorF = RenderSettings.fogColor;
        dCF = (dayColorF - nightColorF) / step;

        skyBoxCopy = new Material(skyBox);//skyboxのコピー
        RenderSettings.skybox = skyBoxCopy;//skyboxをいれる

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
        if (numNight <= step)
        {
            day = !day; // 昼と夜の反転 
        }

        if (day)
        {
            dayExposure -= dE;
            skyBoxCopy.SetFloat("_Exposure", dayExposure);//skyboxを昼に

            dayColorL -= dCL;
            lightObj.GetComponent<Light>().color = dayColorL;//lightを昼に

            dayFog -= dF;
            RenderSettings.fogDensity = dayFog;//fogのdensityを昼に

            dayColorF -= dCL;
            RenderSettings.fogColor = dayColorF;
            
        }
        else
        {
            skyBoxCopy.SetFloat("_Exposure", nightExposure);
            lightObj.GetComponent<Light>().color = nightColorL;//setActiveで切った方が軽いかも
            RenderSettings.fogDensity = nightFog;//fogを切った方が軽いかも
            RenderSettings.fogColor = nightColorF;

            numNight++;
        }
    }
}
