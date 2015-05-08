using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// A function used to have an object follow the player, currently not in use in game
/// </summary>
public class FollowPlayer : MonoBehaviour
{
    public Vector2 Offset;
    public Transform Following;

    public void Update()
    {
        transform.position = Following.transform.position + (Vector3)Offset;
    }

}
