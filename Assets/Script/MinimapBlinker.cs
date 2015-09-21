using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
class MinimapBlinker : MonoBehaviour
{
    public Camera followcam;
    public SpriteRenderer sprite;
    void Awake()
    {
        transform.position = followcam.transform.position;
        
    }
    void Update()
    {
        //sprite.enabled = false;
        transform.position = followcam.transform.position;
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

