using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
