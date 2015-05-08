using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
/// <summary>
/// this is the room monobehavior which contains the logic for moving the camara which is done through the ontrigger enter 2D
/// </summary>
public class Room : MonoBehaviour

{

    public GameObject Player; //player prefab set in the inspector 
    public Camera camera; // camera set in the inspector
    public List<GameObject> enemies;
    public Enemy enemy;
    // enemy prefab set in the inspector 
    
   // array to be used for procedural generaion
    
    public void Start()
    {
        
        enemies = new List<GameObject>();
    }
    public void OnTriggerEnter2D(Collider2D other) //collision detection for camera movement
    {

        var player = other.GetComponent<Player>(); // check to see if collided object is player
        if (player == null) //if no player, return out of the function
            return;

        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -60); // set camera to current room
    }


}
