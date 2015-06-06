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
    public List<Enemy> enemies;
    public Enemy enemy;
    // enemy prefab set in the inspector 
    
   // array to be used for procedural generaion
    
    public void Start()
    {
        
        enemies = new List<Enemy>();
        grabMyEnemies();
        foreach (Enemy it in enemies)
        {
            it.gameObject.SetActive(false);
        }

    }
    public void OnTriggerEnter2D(Collider2D other) //collision detection for camera movement
    {

        var player = other.GetComponent<Player>(); // check to see if collided object is player
        if (player == null) //if no player, return out of the function
            return;

        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -60);// set camera to current room
        foreach (Enemy it in enemies)
        {

            if (it == null)
                return;
            else
            {
                Enemy temp = it;
                temp.gameObject.SetActive(true);
            }
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;
        foreach (Enemy it in enemies)
        {
            if (it == null)
                return;
            else
            {
                it.transform.position = it.startLocation;
                var rot = Quaternion.identity;
                rot.eulerAngles = new Vector3(0, 0, 0);
                it.transform.rotation = rot;
                it.gameObject.SetActive(false);
            }
        }
    }
    public void grabMyEnemies()
    {
        Enemy[] tempList = UnityEngine.Object.FindObjectsOfType(typeof(Enemy)) as Enemy[];
        foreach(Enemy it in tempList)
        {
            var x = Math.Abs(it.transform.position.x - transform.position.x);
            var y = Math.Abs(it.transform.position.y - transform.position.y);
            if((x < 55 && y < 29))
            {
                enemies.Add(it);
            }
            
        }
    }

}
