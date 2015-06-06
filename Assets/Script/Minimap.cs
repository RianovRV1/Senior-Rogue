using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
/// <summary>
/// Experimental found online, may not use ever
/// </summary>
public class Minimap : MonoBehaviour
{

    public SpriteRenderer myRenderer;

    private Texture2D _tex;
    private Sprite _sp;

    void Start()
    {
        _tex = new Texture2D(100, 100);
    }

    void Update()
    {
        for (int i = 0; i < 100; ++i)
        {
            for (int j = 0; j < 100; ++j)
            {
                _tex.SetPixel(i, j, new Color(1, 1, 0, 0));
            }
        }

        // Apply all SetPixel calls
        _tex.Apply();
        _sp = Sprite.Create(_tex, new Rect(0, 0, 15f, 15f), new Vector2(0.0f, 0.0f), 0.1f);
        myRenderer.sprite = _sp;
        
    }
}