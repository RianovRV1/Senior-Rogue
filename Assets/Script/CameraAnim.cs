using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
public class CameraAnim : MonoBehaviour 
{
    internal Animator camAnim; //settiong this to internal lets me have other scripts wait for this to finish
    public AnimationClip clip;
    void OnEnable()
    {
        camAnim = GetComponent<Animator>();
        PlayAnimationAsync("Enter Level");

        //anim.enabled = false;
    }

    void Update()
    {
        
        //PlayAnimationAsync("Enter Level");
        if(Time.timeSinceLevelLoad > 1f)
            camAnim.enabled = false;
    }
    

    IEnumerator PlayAnimationAsync(string animationName)
    {
        
        camAnim.Play("Enter Level");
        yield return new WaitForSeconds(clip.length); //This does the magic
        //Do the stuff after the animation ends
        Time.timeScale = 1;
        camAnim.enabled = false;
    }
}
