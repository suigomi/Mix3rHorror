using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
 
public class ColliderLoadText : MonoBehaviour {
 
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

    bool iventFlag = true;
    bool textFlag = false;
    
    public InputActionReference toggleReference = null;

    public GameObject player;

    public GameObject targetObj;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player && iventFlag)
        {
            dataText.text = "Press A";
            loadText1 = textAsset[0].text;
            splitText1 = loadText1.Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            textFlag = true;
            player.GetComponent<ContinuousMoveProviderBase>().enabled = false;
            toggleReference.action.started += Text1;
            iventFlag = !iventFlag;
        }
    }

    private async void Text1(InputAction.CallbackContext context)
    {
        if(splitText1[textNum1].Contains("/end/"))
        {
            dataText.text = "";
            textFlag = false;
            if(targetObj != null)
            {
                targetObj.SendMessage("TextEndEvents");
            }
            player.GetComponent<ContinuousMoveProviderBase>().enabled = true;
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