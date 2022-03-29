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
    [SerializeField] float nightExposure = 0f;
    //sanValueが1変化したときのExposureの変化量
    [SerializeField] float dE;


    //変更するlight(Colorを変える)
    public GameObject lightObj;
    //昼のColor
    [SerializeField] Color dayColorL;
    //夜のColor
    [SerializeField] Color nightColorL = Color.black;
    //sanValueが1変化したときのlightの変化量
    [SerializeField] private Color dCL;

    //昼のLighting Intensity
    [SerializeField] float dayIntensity;
    //夜のLighting Intensity
    [SerializeField] float nightIntensity = 0f;
    //sanValueが1変化したときのLighting Intensityの変化量
    [SerializeField] float dI;

    //昼のfogのdensity
    [SerializeField] float dayFog;
    //夜のfogのdensity
    [SerializeField] float nightFog = 0f;
    //sanValueが1変化したときのdensityの変化量
    [SerializeField] float dF;

    //昼のfogのcolor
    [SerializeField] Color dayColorF;
    //夜のfogのcolor
    [SerializeField] Color nightColorF = Color.black;
    //sanValueが1変化したときのcolorの変化量
    [SerializeField] Color dCF;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        dayExposure = skyBox.GetFloat("_Exposure");
        dE = (dayExposure - nightExposure) / gameManager.sanValueMax; //SanValueが1変化するときの変化量 = (初めの値 - 終わりの値:0) / (San値の最大値:100 - San値の最小値:0)

        dayColorL = lightObj.GetComponent<Light>().color;
        dCL = (dayColorL - nightColorL) / gameManager.sanValueMax;

        dayFog = RenderSettings.fogDensity;
        dF = (dayFog - nightFog) / gameManager.sanValueMax;

        dayColorF = RenderSettings.fogColor;
        dCF = (dayColorF - nightColorF) / gameManager.sanValueMax;

        dayIntensity = RenderSettings.ambientIntensity;
        dI = (dayIntensity - nightIntensity) / gameManager.sanValueMax;

        skyBoxCopy = new Material(skyBox);//skyboxのコピー
        RenderSettings.skybox = skyBoxCopy;//skyboxのコピーをいれる
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
            float exposure = dE * gameManager.sanValue + nightExposure;//1次関数(切片:夜の値、傾き:dE)として表す
            Color lightColor = dCL* gameManager.sanValue  + nightColorL;
            float intensity = dI * gameManager.sanValue + nightIntensity;
            float fogDensity = dF * gameManager.sanValue + nightFog;
            Color fogColor = dCF * gameManager.sanValue + nightColorF;

            skyBoxCopy.SetFloat("_Exposure", exposure);
            lightObj.GetComponent<Light>().color = lightColor;
            RenderSettings.ambientIntensity = intensity;
            RenderSettings.fogDensity = fogDensity;
            RenderSettings.fogColor = fogColor;
        }
        else
        {
            skyBoxCopy.SetFloat("_Exposure", nightExposure);
            lightObj.GetComponent<Light>().color = nightColorL;//setActiveで切った方が軽いかも
            RenderSettings.ambientIntensity = nightIntensity;
            RenderSettings.fogDensity = nightFog;//fogを切った方が軽いかも
            RenderSettings.fogColor = nightColorF;

            //----テスト用
            gameManager.SubSunValue(10);
            //----
        }
    }

}
