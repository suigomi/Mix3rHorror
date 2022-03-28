using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Appearance : MonoBehaviour
{
    private GameManager gameManager;

    [Serializable] struct Pair
    {
        public int appearanceValue;//表示する侵食度
        public GameObject gameObject;//表示するオブジェクト
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
            // ※インベントリに入っていない状態の時は除くを付け足す
            if (p.appearanceValue >= gameManager.sanValue || !gameManager.day)
            {
                p.gameObject.SetActive(true);
            }
            else
            {
                p.gameObject.SetActive(false);
            }
        }
    }

}
