using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class testtt : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StreamReader sr = new StreamReader("text.txt", Encoding.GetEncoding("Shift_JIS"));
        string text = sr.ReadToEnd();

        string[] texts = text.Split(new[] {"\r\n"}, StringSplitOptions.None);


        int i = 0;
        foreach(string t in texts)
        {
            print(t=="\r");

            // 文字コードを指定
            Encoding enc = Encoding.GetEncoding("Shift_JIS");

            // ファイルを開く
            StreamWriter writer = new StreamWriter($"sugatext{i++}", false, enc);

            // テキストを書き込む
            writer.WriteLine(t);

            // ファイルを閉じる
            writer.Close();

        }






    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
