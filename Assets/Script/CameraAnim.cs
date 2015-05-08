using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
public class CameraAnim : MonoBehaviour 
{
    Animator anim;
    public AnimationClip clip;
    void Start()
    {
        anim = GetComponent<Animator>();
        PlayAnimationAsync("Enter Level");
        //anim.enabled = false;
    }
    void Update()
    {
        
        //PlayAnimationAsync("Enter Level");
        if(Time.timeSinceLevelLoad > 1f)
            anim.enabled = false;
    }
    

    IEnumerator PlayAnimationAsync(string animationName)
    {
        
        anim.Play("Enter Level");
        yield return new WaitForSeconds(clip.length); //This does the magic
        //Do the stuff after the animation ends
        Time.timeScale = 1;
        anim.enabled = false;
    }
}
