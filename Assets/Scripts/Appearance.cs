using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditorInternal;
using UnityEditor;

public class Appearance : MonoBehaviour
{
    private GameManager gameManager;

    [Serializable] struct Pair
    {
        [Tooltip("表示する侵食度")]
        public int appearanceValue;
        [Tooltip("表示するオブジェクトたち")]
        public GameObject[] gameObject;
    }



    [SerializeField] List<Pair> objects;
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
        foreach(Pair p in objects)
        {
            foreach(GameObject gameObj in p.gameObject)
            // ※インベントリに入っていない状態の時は除くを付け足す
            if (p.appearanceValue >= gameManager.sanValue || !gameManager.day)
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
