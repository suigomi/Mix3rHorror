using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopHorrorImageObj : MonoBehaviour
{

    bool isPoped = false;
    Transform player;
    int distance = 10;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        if (!isPoped)
        {
            if ((player.position - transform.position).sqrMagnitude < distance)
            {
                player.GetComponent<PopHorrorImage>().Pop();
                isPoped = true;
                Invoke("Refresh", 5);
            }
            
        }
    }

    void Refresh()
    {
        isPoped = false;
    }

}
