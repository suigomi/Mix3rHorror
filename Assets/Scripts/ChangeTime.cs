using UnityEngine;
using UnityEngine.InputSystem;

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
    //一回のExposureの変化量
    [SerializeField] private Color dC;

    //何回で真っ暗にするか
    public int step = 10;

    //昼か夜か
    [SerializeField] bool day = true;


    // Start is called before the first frame update
    void Start()
    {
        dayExposure = skyBox.GetFloat("_Exposure");
        dayColor = lightObj.GetComponent<Light>().color;
        dE = (dayExposure - nightExposure) / step;
        dC = (dayColor - nightColor) / step;


        skyBoxCopy = new Material(skyBox);
        RenderSettings.skybox = skyBoxCopy;
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
            dayColor -= dC;
            dayExposure = (dayExposure >= nightExposure) ? dayExposure : nightExposure;

            skyBoxCopy.SetFloat("_Exposure", dayExposure);
            lightObj.GetComponent<Light>().color = dayColor;
        }
        else
        {
            skyBoxCopy.SetFloat("_Exposure", nightExposure);
            lightObj.GetComponent<Light>().color = nightColor;
        }
    }
}
