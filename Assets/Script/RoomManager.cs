using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    public static int roomSize = 15;
    //-----------------------------------------------------------------------------------------
    //public GameObject Room1;// all the room prefabs for the room manager to place 
    //public GameObject LRoom1;
    //public GameObject TRoom1;
    //public GameObject DashRoom1;

    //public GameObject Room2;// all the room prefabs for the room manager to place 
    //public GameObject LRoom2;
    //public GameObject TRoom2;
    //public GameObject DashRoom2;

    //public GameObject Room3;// all the room prefabs for the room manager to place 
    //public GameObject LRoom3;
    //public GameObject TRoom3;
    
    //public GameObject Room4;// all the room prefabs for the room manager to place 
    //public GameObject LRoom4;
    //public GameObject TRoom4;

    //public GameObject FourRoom;
    //-------------------------------------------------------------------------------------------------------
    internal static int _level = 1;
    
    public Player Player { get; private set; } //player in the scene view, for placement purposes
    public Enemy Enemy; // enemy prefab, for placement purposes
    public Item Item; //item  prefab for placement purposes
    private Camera myCamera;// camera for setting room script's camera
    private List<String> _phrases;
    public Animator anim;
   
    internal List<Room> _floors; // in order of placement copy of rooms cannot use this to access objects while game is running
    internal List<Room> _goFloor; // realtime instance of rooms, not in order of placement can use this to access objects
    private float //offset values for room instantiation 
        _offsetx = 130f,
        _offsety = 78f,
        _rotationOffset = 90f;
    private Location _offset;
    private System.Random ran;
    // Use this for initialization
    void Awake() //sets roomManagers self reference for code call
    {
        ran = new System.Random();
        Instance = this;
        myCamera = Camera.main;
        _offset = new Location(_offsetx, _offsety);
        _floors = new List<Room>();
        roomLimit = roomSize;
        placeEachRoom();
    }
    void Start() // sets up some taunting strings, makes the room, places the floor tutorial graphic if its the first level.
    {
        
        
       
        _phrases = new List<String>();
        _phrases.Add("Rekt");
        _phrases.Add("Suck it");
        _phrases.Add("Git Gud");
        _phrases.Add("Try Harder");
        _phrases.Add("You're Bad");
        _phrases.Add("Pwned");
        _phrases.Add("Not Your Day");
        Player = FindObjectOfType<Player>();
        if (_level == 1)
            Instantiate(tutorialGraphic, transform.position, transform.rotation);
        SpawnEntity(Player, _floors[0]);
        SpawnEntity(Item, _floors[_floors.Count - 1]);
        getRooms();

    }
    private void getRooms() // a method that will throw away the array created as apposed to leaving it in memory.
    {
        Room[] temp = UnityEngine.Object.FindObjectsOfType(typeof(Room)) as Room[];
        _goFloor = new List<Room>(temp);
    }
    public Room MakeRoom(float rotation, Location offset, string roomType) //function that makes a room taking in the offset values and the room prefab types
    {

        //Debug.Log(String.Format("x: {0}, y: {1}", offset._x, offset._y));
        var prefab = Resources.Load("Prefabs/" + roomType) as GameObject;
        var _room = prefab.GetComponent<Room>(); //looks for room script
        //_room.myCamera = myCamera; // sets rooms camera Not needed but here we are
        _room.transform.position = new Vector3(offset._x, offset._y); // sets its position based on offset

        //-------------------------------------------------------------
        // use of Euler angles to set rotation, based on rotation offset
        // setting rooms rotation to the rotation offset
        //var rot = Quaternion.identity;
        //rot.eulerAngles = new Vector3(0, 0, rotation);
        //_room.transform.rotation = rot;
        //-------------------------------------------------------------------
        if (_floors.Count > 1 && _floors.Count < roomLimit - 1)
        {
            SpawnEntity(Enemy, _room);
        }

        var instance = Instantiate(_room) as Room;
        _floors.Add(instance);
        
        return _room; // returns the room object for spawning entities
    }


    //Made overloaded function for SpawnEntity.


    public void SpawnEntity(Player playerEntity, Room room)
    {
        var player = playerEntity.GetComponent<Player>();
        Player.transform.position = room.transform.position;
    }

    public void SpawnEntity(Enemy enemyEntity, Room room)
    {
        
        var enemy = enemyEntity.GetComponent<Enemy>();
        
        int i = ran.Next() % 3;
        if (i == 0)
        {

            Instantiate(enemy, room.transform.position + new Vector3(10, 10), transform.rotation);
            //room.enemies.Add(enemy); may work will test later
        }
    }

    public void SpawnEntity(Item itemEntity, Room room)
    {
        var item = itemEntity.GetComponent<Item>();
        Instantiate(itemEntity, room.transform.position + new Vector3(2, -10), transform.rotation);
    }



    public void placeEachRoom()// tile placement function
    {
        Floor floor = new Floor(roomLimit);
        selectRoom(floor, 0); // seed placment
        for (int i = 1; i < floor._rooms.Count; ++i)
        {
            
            selectRoom(floor, i);
        }
       
       
    }
    private void selectRoom(Floor floor, int i) //tile placement logic to choose the correct tile
    {
        Location passedOffset = _offset * floor._rooms[i]._location;
        string tile = "";
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == false) // up
        {
            
            MakeRoom(_rotationOffset * 0, (passedOffset), "Room1");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == false) // down
        {
            MakeRoom(_rotationOffset * 2, (passedOffset), "Room3");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // left
        {
            MakeRoom(_rotationOffset * 1, (passedOffset), "Room2");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // right
        {
            MakeRoom(_rotationOffset * 3, (passedOffset), "Room4");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // up right
        {
            MakeRoom(_rotationOffset * 0, (passedOffset), "LRoom1");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // up left
        {
            MakeRoom(_rotationOffset * 1, (passedOffset), "LRoom2");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // down left
        {
            MakeRoom(_rotationOffset * 2, (passedOffset), "LRoom3");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // down right
        {
            MakeRoom(_rotationOffset * 3, (passedOffset), "LRoom4");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == false) // up down
        {
            MakeRoom(_rotationOffset * 0, (passedOffset), "DashVert");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // left right
        {
            MakeRoom(_rotationOffset * 1, (passedOffset), "DashHori");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == false && floor._rooms[i]._doors[3] == true) // up down right
        {
            MakeRoom(_rotationOffset * 0, (passedOffset), "TRoom1");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == false && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // up left right
        {
            MakeRoom(_rotationOffset * 1, (passedOffset), "TRoom2");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == false) // up left down
        {
            MakeRoom(_rotationOffset * 2, (passedOffset), "TRoom3");
        }
        if (floor._rooms[i]._doors[0] == false && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // left down right
        {
            MakeRoom(_rotationOffset * 3, (passedOffset), "TRoom4");
        }
        if (floor._rooms[i]._doors[0] == true && floor._rooms[i]._doors[1] == true && floor._rooms[i]._doors[2] == true && floor._rooms[i]._doors[3] == true) // left down up right
        {
            MakeRoom(_rotationOffset * 0, (passedOffset), "FourDoorRoom");
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
        SceneManager.LoadScene("GameOver");
        //Application.LoadLevel("GameOver");

        
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
            SceneManager.LoadSceneAsync("finish"); //remove async if unexpected behavior occurs 
            //Application.LoadLevel("finish");
        }
        else
        {

            if (string.IsNullOrEmpty(levelName))
            {
                roomSize += 1;
                _level += 1;
                SceneManager.LoadSceneAsync("loading");
                //Application.LoadLevel("loading");
            }

            else
            {
                roomSize += 1;
                _level += 1;
                SceneManager.LoadSceneAsync(levelName);
                //Application.LoadLevel(levelName); depricated 
            }
        }
    }
}