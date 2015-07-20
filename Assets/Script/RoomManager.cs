using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
/// <summary>
///  This is the main management code, it contains quite a few public members in order to be set up in Unity's inspector.
///  The Start method calls all the functions needed to set up the level the main function from the Floor Class
///  The make room method places a room tile at a location
///  The Spawn entity places player, items, and enemy objects at a designated room
///  The place each room function takes a generated Floor object and places the floor tiles
///  the selectRoom has the logic to choose the correct room for placement
///  The Kill player function does a basic text display then calls the coroutine to kill the player and go to the game over screen
///  the goto next level function does the same thing as the kill player in theory, it starts the next level co routine which will either start the loading screen or the finish screen
/// </summary>

public class RoomManager : MonoBehaviour
{
    
    public static RoomManager Instance {get; private set;}
    public int roomLimit;
    public GameObject tutorialGraphic;
    public static int roomSize = 16;

    public GameObject Room1;// all the room prefabs for the room manager to place 
    public GameObject LRoom1;
    public GameObject TRoom1;
   
    public GameObject DashRoom1;
    public GameObject Room2;// all the room prefabs for the room manager to place 
    public GameObject LRoom2;
    public GameObject TRoom2;
    
    public GameObject DashRoom2;
    public GameObject Room3;// all the room prefabs for the room manager to place 
    public GameObject LRoom3;
    public GameObject TRoom3;
    
    public GameObject Room4;// all the room prefabs for the room manager to place 
    public GameObject LRoom4;
    public GameObject TRoom4;
    public GameObject FourRoom;
    
    internal static int _level = 1;
    
    public Player Player { get; private set; } //player in the scene view, for placement purposes
    public GameObject Enemy; // enemy prefab, for placement purposes
    public GameObject Item; //item  prefab for placement purposes
    public Camera camera;// camera for setting room script's camera
    private List<String> _phrases;
    public Animator anim;
   
    internal List<Room> _floors; // in order of placement copy of rooms
    internal List<Room> _goFloor; // realtime instance of rooms, not in order of placement
    private float //offset values for room instanciation 
        _offsetx = 130f,
        _offsety = 78f,
        _rotationOffset = 90f;
    private System.Random ran;
    // Use this for initialization
    void Awake() //sets roomManagers self reference for code call
    {
        Instance = this;
    }
    void Start() // sets up some taunting strings, makes the room, places the floor tutorial graphic if its the first level.
    {
        if (_level == 1)
            Instantiate(tutorialGraphic, transform.position, transform.rotation);
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
        getRooms();
       

       
        
        
    }
    private void getRooms() // a method that will throw away the array created as apposed to leaving it in memory.
    {
        Room[] temp = UnityEngine.Object.FindObjectsOfType(typeof(Room)) as Room[];
        _goFloor = new List<Room>(temp);
    }
    public Room MakeRoom(float rotation, float xoffset, float yoffset, GameObject roomType, Player playerEntity, GameObject enemyEntity, GameObject itemEntity) //function that makes a room taking in the offset values and the room prefab types
    {
        
        var _room = roomType.GetComponent<Room>(); //looks for room script
        _room.camera = camera; // sets rooms camera
        _room.transform.position = new Vector3(xoffset, yoffset); // sets its position based on offset
        // use of Euler angles to set rotation, based on rotation offset
        // setting rooms rotation to the rotation offset
        var rot = Quaternion.identity;
        rot.eulerAngles = new Vector3(0, 0, rotation);
        roomType.transform.rotation = rot;
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

        
        

        Instantiate(_room, _room.transform.position, _room.transform.rotation); // creating room clone in the scene view
      
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
    public void placeEachRoom(Floor floor)// tile placement function
    {
        selectRoom(floor, 0); // seed placment
        for(int i = 1; i < floor._rooms.Count; ++i)
        {
            selectRoom(floor, i);
        }
    }
    private void selectRoom(Floor floor, int i) //tile placement logic to choose the correct tile
    {
        
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == false) // up
        {
            if (i == 0)
                 MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room1, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room1, null, Enemy, null);

        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == false) // down
        {
            if (i == 0)
                SpawnEntity(Player, null, null, MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room3, Player, null, null));
            else
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room3, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // left
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room2, Player, null, null);
            else
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room2, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room4, Player, null, null);
            else
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), Room4, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // up right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom1, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom1, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // up left
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom2, Player, null, null);
            else
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom2, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // down left
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom3, Player, null, null);
            else
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom3, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // down right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom4, Player, null, null);
            else
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), LRoom4, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == false) // up down
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), DashRoom1, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), DashRoom1, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // left right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), DashRoom2, Player, null, null);
            else
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), DashRoom2, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // up down right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom1, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom1, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // up left right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom2, Player, null, null);
            else
                MakeRoom(_rotationOffset * 1, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom2, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // up left down
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom3, Player, null, null);
            else
                MakeRoom(_rotationOffset * 2, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom3, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // left down right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom4, Player, null, null);
            else
                MakeRoom(_rotationOffset * 3, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), TRoom4, null, Enemy, null);
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // left down up right
        {
            if (i == 0)
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), FourRoom, Player, null, null);
            else
                MakeRoom(_rotationOffset * 0, (_offsetx * floor._rooms[i]._location._x), (_offsety * floor._rooms[i]._location._y), FourRoom, null, Enemy, null);
        }
    }
    public void KillPlayer() // kills the player
    {
        
        int choice = ran.Next() % _phrases.Count;
        FloatingText.Show(String.Format("{0}", _phrases[choice]), "PointStarText", new FromWorldPointTextPositioner(Camera.main, Player.transform.position, 1.5f, 50));
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo() //coroutine; which kills player, has actual logic
    {
        Player.Kill();
        roomSize = 16;
        _level = 1;
        yield return new WaitForSeconds(0.75f);
        Application.LoadLevel("GameOver");

        
    }
    public void GotoNextLevel(string levelName) //Starts up the next level
    {
        StartCoroutine(GotoNextLevelCo(levelName));
    }

    private IEnumerator GotoNextLevelCo(string levelName) // logic to starting next level
    {
        
        yield return new WaitForSeconds(3f);
        if (roomSize >= 17)
        {
            Application.LoadLevel("finish");
        }
        else
        {

            if (string.IsNullOrEmpty(levelName))
            {
                roomSize += 1;
                _level += 1;
                Application.LoadLevel("loading");
            }

            else
            {
                roomSize += 1;
                _level += 1;
                Application.LoadLevel(levelName);
            }
        }
    }
}