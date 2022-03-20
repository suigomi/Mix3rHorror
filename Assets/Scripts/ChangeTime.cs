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
    [SerializeField] Color dayColor;
    //夜のColor
    [SerializeField] Color nightColor = Color.black;
    //一回のlightの変化量
    [SerializeField] private Color dC;


    //昼の環境光の強さ
    [SerializeField] float dayIntensity = 1f;
    //夜の環境光の強さ
    [SerializeField] float nightIntensity = 0f;
    //一回の環境光の変化量
    [SerializeField] float dI;


    //何回で真っ暗にするか
    public int step = 10;

    //昼か夜か
    [SerializeField] bool day = true;


    // Start is called before the first frame update
    void Start()
    {
        dayExposure = skyBox.GetFloat("_Exposure");
        dE = (dayExposure - nightExposure) / step;

        dayColor = lightObj.GetComponent<Light>().color;
        dC = (dayColor - nightColor) / step;

        dI = (dayIntensity - nightIntensity) / step;

        skyBoxCopy = new Material(skyBox);//skyboxのコピー
        RenderSettings.skybox = skyBoxCopy;//skyboxをいれる

        RenderSettings.ambientIntensity = dayIntensity;//環境光の強さ
        RenderSettings.reflectionIntensity = dayIntensity;//環境光の反射の強さ
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
        day = !day; // 昼と夜の反転 
        if (day)
        {
            dayExposure -= dE;
            dayExposure = (dayExposure >= nightExposure) ? dayExposure : nightExposure;//夜より暗くならないように

            dayColor -= dC;

            dayIntensity -= dI;
            dayIntensity = (dayIntensity >= nightIntensity) ? dayIntensity : nightIntensity;//夜より暗くならないように

            skyBoxCopy.SetFloat("_Exposure", dayExposure);//skyboxを昼に
            lightObj.GetComponent<Light>().color = dayColor;//lightを昼に
            RenderSettings.ambientIntensity = dayIntensity;//環境光を昼に
            RenderSettings.reflectionIntensity = dayIntensity;//環境光の反射を昼に
        }
        else
        {
            skyBoxCopy.SetFloat("_Exposure", nightExposure);
            lightObj.GetComponent<Light>().color = nightColor;
            RenderSettings.ambientIntensity= nightIntensity;
            RenderSettings.reflectionIntensity = nightIntensity;
        }
    }
}
