using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Exit : MonoBehaviour
{

    void OnMouseDown()
    {
        Quitfunc();
    }
   public void Quitfunc()
    {
        //Time.timeScale = 1; 
       Application.Quit();
        Debug.Log("quit");
    }
}
