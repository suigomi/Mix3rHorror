using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.Events;
 
public class Scene1LoadText : MonoBehaviour {
 
	//　読み込んだテキストを出力するUIテキスト
	[SerializeField]
	private Text dataText;
	//　読む込むテキストが書き込まれている.txtファイル
	[SerializeField]
	private TextAsset[] textAsset;
	//　テキストファイルから読み込んだデータ
	private string loadText1;
	//　Resourcesフォルダから直接テキストを読み込む
	private string[] splitText1;
	//　現在表示中テキスト1番号
	private int textNum1;
	//　現在表示中テキスト2番号
	private int textNum2;

    bool textFlag = false;
    
    public InputActionReference toggleReference = null;
 
    public void drawText(int sceneNumber)
    {
        switch(sceneNumber)
        {
            case 1:
		        loadText1 = textAsset[0].text;
                break;
            case 2:
		        loadText1 = textAsset[1].text;
                break;
            case 3:
		        loadText1 = textAsset[2].text;
                break;
            default:
                break;
        }
		splitText1 = loadText1.Split(char.Parse("\n"));
		textNum1 = 0;
		dataText.text = "test";
    }

	void Start ()
    {
		dataText.text = "Press A";
        loadText1 = textAsset[0].text;
		splitText1 = loadText1.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        textFlag = true;
	}

    void OnTriggerEnter(Collider other)
    {
        toggleReference.action.started += Toggle;
    }

    private async void Toggle(InputAction.CallbackContext context)
    {
        if(splitText1[textNum1].Contains("/end/"))
        {
            dataText.text = "";
            textFlag = false;
        }
        else if(splitText1[textNum1].Length > 1 && textFlag) {
            for(int i=0; i<splitText1[textNum1].Length; i++)
            {
                dataText.text = splitText1[textNum1].Substring(0, i);
                await Task.Delay(50);
                textFlag = false;
            }
            textFlag = true;
            dataText.text += " ▼\nPress A";
            textNum1++;
            if(textNum1 >= splitText1.Length) {
                textNum1 = 0;
            }
        }
        else if(textFlag) {
            textNum1++;
        }
    }
}