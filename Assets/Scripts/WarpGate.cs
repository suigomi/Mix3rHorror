using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpGate : MonoBehaviour
{
    public void TextEndEvents()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
