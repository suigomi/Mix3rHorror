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



    [SerializeField] List<AppearanceTuple> appearanceTuple;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAppearance();
    }

    public void ChangeAppearance()
    {
        foreach(AppearanceTuple at in appearanceTuple)
        {
            foreach(GameObject gameObj in at.objects)
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
    }

}
