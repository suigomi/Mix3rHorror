using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;
using UnityEditor;

[Serializable]
public struct AppearanceTuple
{
    [Tooltip("表示するsan値"), Range(0, 100)]
    public int appearanceValue;
    [Tooltip("表示するオブジェクトたち")]
    [SerializeField] public List<GameObject> objects;
}

public class AppearanceManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField, Tooltip("昼だけ出現するオブジェクト")]
    List<GameObject> dayObjects;

    [SerializeField, Tooltip("夜だけ出現するオブジェクト")]
    List<GameObject> nightObjects;

    [SerializeField, Tooltip("San値によって出現するオブジェクト")]
    List<AppearanceTuple> appearanceTuple;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    void Update()
    {
        //夜と昼で表示するオブジェクトを変える--------------
        if (gameManager.day)
        {
            foreach(GameObject gameObj in dayObjects)
            {
                gameObj.SetActive(true);
            }
            foreach(GameObject gameObj in nightObjects)
            {
                gameObj.SetActive(false);
            }
        } 
        else
        {
            foreach (GameObject gameObj in dayObjects)
            {
                gameObj.SetActive(false);
            }
            foreach (GameObject gameObj in nightObjects)
            {
                gameObj.SetActive(true);
            }
        }
        //---------------------------------------------------


        //San値で表示するオブジェクトを変える----------------
        foreach (AppearanceTuple at in appearanceTuple)
        {
            foreach (GameObject gameObj in at.objects)
            {
                // ※インベントリに入っていない状態の時は除くを付け足す
                if (at.appearanceValue >= gameManager.sanValue || !gameManager.day)
                {
                    gameObj.SetActive(true);
                }
                else
                {
                    gameObj.SetActive(false);
                }
            }
        }
        //----------------------------------------------------
    }
}
