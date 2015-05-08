using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// Contains logic to start up a level, used on the game over screen and the start screen
/// </summary>
public class GameOver : MonoBehaviour
{
    public string FirstLevel;

    void OnMouseDown()
    {
        StartLevel();
    }
  public  void StartLevel()
    {
        Time.timeScale = 1;  
      Application.LoadLevel(FirstLevel);
    }
}