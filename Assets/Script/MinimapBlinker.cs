using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
class MinimapBlinker : MonoBehaviour
{
    private Camera followCam;
    public SpriteRenderer sprite;
    void Awake()
    {
        followCam = Camera.main;
        transform.position = followCam.transform.position;
       
    }
    void Update()
    {
        //sprite.enabled = false;
        transform.position = followCam.transform.position;
        if (Time.fixedTime % .5 < .2)
        {
            sprite.enabled = false;
        }
        else
        {
            sprite.enabled = true;
        }

    }
}

