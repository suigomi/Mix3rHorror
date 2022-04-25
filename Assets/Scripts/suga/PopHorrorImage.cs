using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopHorrorImage : MonoBehaviour
{

    [SerializeField]
    HorrorImages horrorImages;

    [SerializeField]
    Image Image;

    public void Pop()
    {
        Sprite sprite = horrorImages.images[Random.Range(0, horrorImages.images.Length - 1)];
        Image.sprite = sprite;
        Image.enabled = true;
        Invoke("Vanish", 2);
    }


    void Vanish()
    {
        Image.enabled = false;
    } 

}
