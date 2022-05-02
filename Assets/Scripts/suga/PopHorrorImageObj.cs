﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopHorrorImageObj : MonoBehaviour
{

    [SerializeField]
    HorrorImages horrorImages;

    [SerializeField]
    Image Image;

    bool isPoped = false;
    Transform player;
    [SerializeField] int distance = 10;

    AudioSource jumpScareAudio;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        jumpScareAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isPoped)
        {
            if ((player.position - transform.position).sqrMagnitude < distance)
            {
                jumpScareAudio.PlayOneShot(jumpScareAudio.clip);
                Sprite sprite = horrorImages.images[Random.Range(0, horrorImages.images.Length - 1)];
                Image.sprite = sprite;
                Image.enabled = true;
                Invoke("Vanish", 2);
                isPoped = true;
                Invoke("Refresh", 5);
            }
        }
    }

    void Vanish()
    {
        Image.enabled = false;
    }

    void Refresh()
    {
        isPoped = false;
    }

}
