using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;  
/// <summary>
/// An object designed to create text that gets destroyed after a few seconds of displaying.
/// </summary>
public class FloatingText : MonoBehaviour // floating text object
{
    private static readonly GUISkin Skin = Resources.Load<GUISkin>("GameSkin"); //takes in guiskin made and set in unity

    public static FloatingText Show(string text, string style, IFloatingTextPositioner positioner) // show function that returns the floating text object
    {
        var go = new GameObject("Floating Text"); //instancating game object
        var floatingText = go.AddComponent<FloatingText>(); // adding floating text as a script to it
        floatingText.Style = Skin.GetStyle(style); // using passed in string to find the style in Game skin
        floatingText._positioner = positioner; // setting private floating text position to passed in positioner
        floatingText._content = new GUIContent(text);//using passed in text as contents
        return floatingText; // returning itself
    }

    private GUIContent _content;
    private IFloatingTextPositioner _positioner;

    public string Text { get { return _content.text; } set { _content.text = value; } } //defining getter and setter

    public GUIStyle Style { get; set; }

    public void OnGUI()// On gui called by unity automatically
    {
        var position = new Vector2(); // postion set to a new vector 2 (x and y)
        var contentSize = Style.CalcSize(_content); //size set to private contents calculated font size
        if (!_positioner.GetPosition(ref position, _content, contentSize)) // if a return false on getposition //destory the game object
        {
            Destroy(gameObject); 
            return;
        }

        GUI.Label(new Rect(position.x, position.y, contentSize.x, contentSize.y), _content, Style); // uses a new rectanlge, the private guicontent, and the style.
    }

}

