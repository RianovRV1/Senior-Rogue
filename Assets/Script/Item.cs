using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// base class, contains methods to be collected and set to inactive or destroyed, play an animation (if any) and play a sound (if any)
/// The giveToPlayer method can be overriden to make weapon items or armor items
/// </summary>
public class Item : MonoBehaviour // base item class with virtual methods
{
    
    public GameObject Effect;

    public AudioClip SoundFX; // sound clip for later polish
    internal bool _isCollected; // internal declaration so only child can access it through code, invisible to unity inspector 
    public virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (_isCollected) //if its already been collected, return out
            return;
        var player = other.GetComponent<Player>();//look for player script in object
        if (player == null) // return out if no player
            return;
        
        if (SoundFX!= null) // if sound clip is not null, play sound at the position
            AudioSource.PlayClipAtPoint(SoundFX, transform.position);

        if(Effect != null) // if the special effect isnt null, play it at position if item
            Instantiate(Effect, transform.position, transform.rotation);
        GiveToPlayer(player); // call give item to player

        
    }

    public virtual void GiveToPlayer(Player player)
    {
        FloatingText.Show("Stage Completed", "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 3f, 40)); // whatever method you need to do to affect the player on pickup
        _isCollected = true; //set is collected to true
        gameObject.SetActive(false); // set the gameobject to false so it doesnt do anything anymore
        RoomManager.Instance.GotoNextLevel("Loading");
    }

    

}

