using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// Actual function implementation of placing floating text
/// </summary>
public class FromWorldPointTextPositioner : IFloatingTextPositioner  // positioner using floating text positioner interface
{
    private readonly Camera _camera; 
    private readonly Vector3 _worldPosition;
    private readonly float _speed;
    private float _timeToLive;
    private float _yOffset;

    public FromWorldPointTextPositioner(Camera camera, Vector3 worldPosition, float timeToLive, float speed) //takes in camera's info, a vector3 of the passed in position, a time to live float, and a move speed of the text object
    {
        //setting private memebers to the passed in arguments
        _camera = camera;  
        _worldPosition = worldPosition;
        _timeToLive = timeToLive;
        _speed = speed;

    }

    public bool GetPosition(ref Vector2 position, GUIContent content, Vector2 size)
    {
        // implmentation of interface function
        if ((_timeToLive -= Time.deltaTime) <= 0) // if time to live change over time is less than or equal to 0, return false
            return false;

        var screenPosition = _camera.WorldToScreenPoint(_worldPosition); // set objects position in the screen 
        position.x = screenPosition.x - (size.x / 2); 
        position.y = Screen.height - screenPosition.y - _yOffset;

        _yOffset += Time.deltaTime * _speed; // moving the floating text slowly up
        return true;
    }
}

