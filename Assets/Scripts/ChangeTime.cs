using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class ChangeTime : MonoBehaviour
{
    private GameManager gameManager;
    
    
    [Tooltip("変更するskybox(Exposureを変える)")]
    public Material skyBox;

    [Tooltip("Materialは再生するごとに変わって色が変わってしまうのでCopyを使用")]
    Material skyBoxCopy;

    [Tooltip("昼のExposure")]
    float dayExposure;

    [Tooltip("夜のExposure")]
    float nightExposure = 0f;

    [Tooltip("sanValueが1変化したときのExposureの変化量")]
    float dE;


    [Tooltip("変更するlight(Colorを変える)")]
    public GameObject lightObj;

    [Tooltip("昼のColor")]
    Color dayColorL;

    [Tooltip("夜のColor")]
    Color nightColorL = Color.black;

    [Tooltip("sanValueが1変化したときのlightの変化量")]
    private Color dCL;

    [Tooltip("昼のLighting Intensity")]
    float dayIntensity;

    [Tooltip("夜のLighting Intensity")]
    float nightIntensity = 0f;

    [Tooltip("sanValueが1変化したときのLighting Intensityの変化量")]
    float dI;



    [Tooltip("昼のfogのdensity")]
    float dayFog;

    [Tooltip("夜のfogのdensity")]
    float nightFog = 0f;

    [Tooltip("sanValueが1変化したときのdensityの変化量")]
    float dF;



    [Tooltip("昼のfogのcolor")]
    Color dayColorF;

    [Tooltip("夜のfogのcolor")]
    Color nightColorF = Color.black;

    [Tooltip("sanValueが1変化したときのcolorの変化量")]
    Color dCF;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
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
