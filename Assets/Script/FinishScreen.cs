using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
/// <summary>
/// Contains simple logic to wait for a few seconds then bring the player back to the start screen
/// </summary>
public class FinishScreen : MonoBehaviour
    
{
    public void Start()
    {
        GoToStart();
    }

    public void GoToStart()
    {
        StartCoroutine(GoToStartCo());
    }
    private IEnumerator GoToStartCo()
    {
        yield return new WaitForSeconds(5f);
        Application.LoadLevel("Start Screen");
    }

}

