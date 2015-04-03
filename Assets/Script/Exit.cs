using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Exit : MonoBehaviour
{

    void OnMouseDown()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
