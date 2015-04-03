using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class GameOver : MonoBehaviour
{
    public string FirstLevel;

    void OnMouseDown()
    {
        Application.LoadLevel(FirstLevel);
    }
}