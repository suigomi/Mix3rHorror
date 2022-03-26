﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading.Tasks;
 
public class LoadText : MonoBehaviour {
 
	//　読み込んだテキストを出力するUIテキスト
	[SerializeField]
	private Text dataText;
	//　読む込むテキストが書き込まれている.txtファイル
	[SerializeField]
	private TextAsset textAsset;
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
 
	void Start () {
		loadText1 = textAsset.text;
		splitText1 = loadText1.Split(char.Parse("\n"));
		textNum1 = 0;
		dataText.text = "test";
	}

    private void Awake()
    {
        toggleReference.action.started += Toggle;
    }

    private async void Toggle(InputAction.CallbackContext context)
    {
        if(splitText1[textNum1] != "" && !textFlag) {
            for(int i=0; i<splitText1[textNum1].Length; i++)
            {
                dataText.text = splitText1[textNum1].Substring(0, i);
                await Task.Delay(100);
                textFlag = true;
            }
            textFlag = false;
            dataText.text += " ▼";
            textNum1++;
            if(textNum1 >= splitText1.Length) {
                textNum1 = 0;
            }
        } else if(!textFlag) {
            dataText.text = "";
            textNum1++;
        }
    }
}