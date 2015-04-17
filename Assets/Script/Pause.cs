using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Pause : MonoBehaviour
{
   
    bool paused = false;
    public GameObject pauseMenuPanel;
    private Animator anim;
    //public Transform Location;
    void Start()
    {
        anim = pauseMenuPanel.GetComponent<Animator>();
        
    }
    void Update()
    {
       
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            Paused();
    }
    public void Paused()
    {
        Debug.Log("pressing");
        if (!paused)
        {
            anim.SetTrigger("PAUSED");
            //play the Slidein animation
            
            //set the isPaused flag to true to indicate that the game is paused
            paused = true;
            //freeze the timescale
            Time.timeScale = 0;
        }
        else if (paused)
        {
            paused = false;
            anim.SetTrigger("UNPAUSED");
            Time.timeScale = 1;
            
        }
    }
}

