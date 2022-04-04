using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ChangeTime : MonoBehaviour
{
    private GameManager gameManager;
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

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        dayExposure = skyBox.GetFloat("_Exposure");
        dE = (dayExposure - nightExposure) / gameManager.sanValueMax;

        dayColorL = lightObj.GetComponent<Light>().color;
        dCL = (dayColorL - nightColorL) / gameManager.sanValueMax;

        dayFog = RenderSettings.fogDensity;
        dF = (dayFog - nightFog) / gameManager.sanValueMax;

        dayColorF = RenderSettings.fogColor;
        dCF = (dayColorF - nightColorF) / gameManager.sanValueMax;

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
        if (gameManager.sanValue > gameManager.sanValueMin)
        {
            gameManager.day = !gameManager.day; //昼と夜の切り替え
        }

        if (gameManager.day)
        {
            float exposure = dE * gameManager.sanValue + nightExposure;
            Color lightColor = dCL* gameManager.sanValue  + nightColorL;
            float fogDensity = dF * gameManager.sanValue + nightFog;
            Color fogColor = dCF * gameManager.sanValue + nightColorF;


            skyBoxCopy.SetFloat("_Exposure", exposure);
            lightObj.GetComponent<Light>().color = lightColor;
            RenderSettings.fogDensity = fogDensity;
            RenderSettings.fogColor = fogColor;
        }
        else
        {
            skyBoxCopy.SetFloat("_Exposure", nightExposure);
            lightObj.GetComponent<Light>().color = nightColorL;//setActiveで切った方が軽いかも
            RenderSettings.fogDensity = nightFog;//fogを切った方が軽いかも
            RenderSettings.fogColor = nightColorF;

            gameManager.SubSunValue(10);
        }
    }

}
