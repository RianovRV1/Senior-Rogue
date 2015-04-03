﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class RoomManager : MonoBehaviour
{
    
    public static RoomManager Instance {get; private set;}
    public int roomLimit;
    
    public static int roomSize = 4;
    public GameObject Room;// all the room prefabs for the room manager to place 
    public GameObject LRoom;
    public GameObject TRoom;
    public GameObject FourRoom;
    internal static int level = 1;
    public GameObject DashRoom;
    public Player Player { get; private set; } //player in the scene view, for placement purposes
    public GameObject Enemy; // enemy prefab, for placement purposes
    public GameObject Item; //item  prefab for placement purposes
    public Camera camera;// camera for setting room script's camera
    private List<String> _phrases;
    public List<Room> _floors;
    private float //offset values for room instanciation 
        _offsetx = 78f,
        _offsety = 78f,
        _rotationOffset = 90f;
    private System.Random ran;
    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        ran = new System.Random();
        _phrases = new List<String>();
        _phrases.Add("Rekt");
        _phrases.Add("Suck it");
        _phrases.Add("Git Gud");
        _phrases.Add("Try Harder");
        _phrases.Add("You're Bad");
        _phrases.Add("Pwned");
        _phrases.Add("Not Your Day");
        Player = FindObjectOfType<Player>();
        roomLimit = roomSize;
        _floors = new List<Room>();
        placeEachRoom(new Floor(roomLimit));
        SpawnEntity(null, null, Item, _floors[_floors.Count - 1]);
        
        
    }

    // Update is called once per frame
   
    void Update()
    {
        
    }
    public Room MakeRoom(float rotation, float xoffset, float yoffset, GameObject roomType, Player playerEntity, GameObject enemyEntity, GameObject itemEntity) //function that makes a room taking in the offset values and the room prefab types
    {
        
        var _room = roomType.GetComponent<Room>(); //looks for room script
        _room.camera = camera; // sets rooms camera
        _room.transform.position = new Vector3(xoffset, yoffset); // sets its position based on offset
        var rot = Quaternion.identity; // value to set a quaternion rotation
        rot.eulerAngles = new Vector3(0, 0, rotation); // use of Euler angles to set rotation, based on rotation offset
        _room.transform.rotation = rot; // setting rooms rotation to the rotation offset

        if (playerEntity != null) // if not null, get player component then set the player to the center of the room
        {
            var player = playerEntity.GetComponent<Player>();
            Player.transform.position = _room.transform.position;
        }
        if (enemyEntity != null) // if not null, get enemy component then set the enemy to specified positon in the room, set its player transform value to the globally decalared player transform value in the RoomManager script
        {
            var enemy = enemyEntity.GetComponent<Enemy>();
            //enemy.player.transform = Player.transform;
            int i = ran.Next() % 20;
            if ((i >= 0 && i <= 10) && i != 8 )
            {
                Instantiate(enemyEntity, _room.transform.position + new Vector3(10, 10), transform.rotation);
            }


        }
        if (itemEntity != null)// if item is not null, get the item component the set its postion to the specfied postion within the room
        {
            var item = itemEntity.GetComponent<Item>();
            Instantiate(itemEntity, _room.transform.position + new Vector3(2, -10), transform.rotation);
        }
        
        var rom = Instantiate(_room, _room.transform.position, _room.transform.rotation); // creating room clone in the scene view
        _floors.Add(_room);
        

        
        return _room; // returns the room object for spawning entities
    }
    public void SpawnEntity(Player playerEntity, GameObject enemyEntity, GameObject itemEntity, Room room) // function used to actually spawn the objects into rooms, takes in player, item, and enemy entity, as well as the room to place it in
    {
        ran = new System.Random();
        if (playerEntity != null) // if not null, get player component then set the player to the center of the room
        {
            var player = playerEntity.GetComponent<Player>();
            Player.transform.position = room.transform.position;
        }
        if (enemyEntity != null) // if not null, get enemy component then set the enemy to specified positon in the room, set its player transform value to the globally decalared player transform value in the RoomManager script
        {
            var enemy = enemyEntity.GetComponent<Enemy>();
            //enemy.player.transform = Player.transform;
            int i = ran.Next() % 3;
            if (i == 0)
            {

                Instantiate(enemy, room.transform.position + new Vector3(10, 10), transform.rotation);
                //room.enemies.Add(enemy);
            }


        }
        if (itemEntity != null)// if item is not null, get the item component the set its postion to the specfied postion within the room
        {
            var item = itemEntity.GetComponent<Item>();
            Instantiate(itemEntity, room.transform.position + new Vector3(2, -10), transform.rotation);
        }
        


    }
    public void placeEachRoom(Floor floor)
    {
        selectRoom(floor, 0); // seed placment
        for(int i = 1; i < floor._rooms.Count; ++i)
        {
            selectRoom(floor, i);
        }
    }
    private void selectRoom(Floor floor, int i)
    {
        
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == false) // up
        {
            if (i == 0)
                 MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, null, Enemy, null);

        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == false) // down
        {
            if (i == 0)
                SpawnEntity(Player, null, null, MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, Player, null, null));
            else
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == false) // left
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == true) // right
        {
            if (i == 0)
               MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), Room, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == true) // up right
        {
            if (i == 0)
                 MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == false) // up left
        {
            if (i == 0)
                 MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, Player, null, null);
            else
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == false) // down left
        {
            if (i == 0)
                 MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, Player, null, null);
            else
               MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == true) // down right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, Player, null, null);
            else
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), LRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == false) // up down
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), DashRoom, Player, null, null);
            else
              MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), DashRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == true) // left right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), DashRoom, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), DashRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == false && floor._rooms[i].doors[3] == true) // up down right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == false && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == true) // up left right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == false) // up left down
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, Player, null, null);
            else
                 MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == false && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == true) // left down right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, Player, null, null);
            else
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), TRoom, null, Enemy, null);
        }
        if (floor._rooms[i].doors[0] == true && floor._rooms[i].doors[1] == true && floor._rooms[i].doors[2] == true && floor._rooms[i].doors[3] == true) // left down up right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), FourRoom, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i].location._x), (_offsety * floor._rooms[i].location._y), FourRoom, null, Enemy, null);
        }
    }
    public void KillPlayer()
    {
        
        int choice = ran.Next() % _phrases.Count;
        FloatingText.Show(String.Format("{0}", _phrases[choice]), "PointStarText", new FromWorldPointTextPositioner(Camera.main, Player.transform.position, 1.5f, 50));
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo() //coroutine; explain how a coroutine works 
    {
        Player.Kill();
        roomSize = 4;
        level = 1;
        yield return new WaitForSeconds(0.75f);
        Application.LoadLevel("GameOver");

        
    }
    public void GotoNextLevel(string levelName)
    {
        StartCoroutine(GotoNextLevelCo(levelName));
    }

    private IEnumerator GotoNextLevelCo(string levelName)
    {
        
        yield return new WaitForSeconds(3f);
        if (roomSize >= 5)
        {
            Application.LoadLevel("finish");
        }
        else
        {

            if (string.IsNullOrEmpty(levelName))
            {
                roomSize += 1;
                level += 1;
                Application.LoadLevel("loading");
            }

            else
            {
                roomSize += 1;
                level += 1;
                Application.LoadLevel(levelName);
            }
        }
    }
}