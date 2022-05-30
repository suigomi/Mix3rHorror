using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "motorcycle" || other.tag == "player")
        {
            if(GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.GameClear();
            }
        }
    }
}
