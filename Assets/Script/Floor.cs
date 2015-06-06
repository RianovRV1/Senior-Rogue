using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// class that creates a floor using rooms
/// First it places a room at the origin, then it grabs all the possible locations, culls it to the valid locations,
/// randomly selects the location for the next room, places the rooms and sets the doors accordingly.
/// </summary>
public class Floor
{
    public List<Rooms> _rooms;
    public Floor(int roomCount)
    {
        Random rand = new Random();
        _rooms = new List<Rooms>();
        if (roomCount == 0)
            return;
        else
        {
            for (int i = 0; i < roomCount; ++i)
            {
                if (i == 0)
                {
                    _rooms.Add(new Rooms(0, 0));
                }
                else
                {
                    _rooms = addRoom(_rooms, rand);
                }
            }

        }
    }
    public void printFloor()
    {
        for (int i = 0; i < _rooms.Count; ++i)
        {
            _rooms[i].printRoom();
        }
    }
    private List<Rooms> addRoom(List<Rooms> rooms, Random rand)
    {
        List<Rooms> ret = rooms;
        List<Location> possibleLoc = checkValidNeighbors(rooms);
        int roomChoice = rand.Next() % possibleLoc.Count;// randomly choose a room within valid rooms
        ret = addDoors(rooms, possibleLoc[roomChoice]);
        return ret;
    }
    private List<Location> checkValidNeighbors(List<Rooms> rooms) // checks to see if there is already at an existing rooms location
    {

        List<Location> loc = new List<Location>();
        List<Location> all = allPossibleRooms(rooms);

        for (int i = 0; i < rooms.Count; ++i)
        {
            for (int j = 0; j < all.Count; ++j)
            {
                if (rooms[i]._location.Equals(all[j]))
                {
                    all.RemoveAt(j);
                    --j;
                }

            }
        }
        loc = all;

        return loc;
    }
    private List<Location> allPossibleRooms(List<Rooms> rooms)
    {
        List<Location> loc = new List<Location>();

        for (int i = 0; i < rooms.Count; ++i)
        {
            int currentLocX = rooms[i]._location._x;
            int currentLocY = rooms[i]._location._y;

            loc.Add(new Location(currentLocX, (currentLocY + 1)));

            currentLocY = rooms[i]._location._y;

            loc.Add(new Location(currentLocX, currentLocY - 1));

            currentLocY = rooms[i]._location._y;

            loc.Add(new Location(currentLocX - 1, currentLocY));
            currentLocX = rooms[i]._location._x;


            loc.Add(new Location(currentLocX + 1, currentLocY));
            currentLocX = rooms[i]._location._x;


        }
        return loc;
    }
    private List<Rooms> addDoors(List<Rooms> rooms, Location chosenLoc)
    {
        List<Rooms> ret = rooms;
        bool N = false;
        bool S = false;
        bool E = false;
        bool W = false;

        int direction = 0;
        int i = 0;
        for (int j = 0; j < ret.Count; ++j)
        {
            Location loc1 = new Location(ret[j]._location._x - 1, ret[j]._location._y);
            Location loc2 = new Location(ret[j]._location._x + 1, ret[j]._location._y);
            Location loc3 = new Location(ret[j]._location._x, ret[j]._location._y + 1);
            Location loc4 = new Location(ret[j]._location._x, ret[j]._location._y - 1);

            if (chosenLoc.Equals(loc1))
            {
                W = true;

                direction = 2;
                break;
            }
            if (chosenLoc.Equals(loc2))
            {
                E = true;

                direction = 3;
                break;
            }
            if (chosenLoc.Equals(loc3))
            {
                S = true;

                direction = 0;
                break;
            }
            if (chosenLoc.Equals(loc4))
            {
                N = true;

                direction = 1;
                break;
            }
            ++i;
        }

        ret.Add(new Rooms(chosenLoc, N, S, E, W));



        ret[i].setDoor(direction);

        return ret;

    }
    private int removeRedundant(List<Rooms> rooms)
    {
        int result = 0;
        for (int i = 0; i < rooms.Count; ++i)//nested for loop to set a reference point to check for duplicate rooms and remove said duplicates
        {

            Location locStart = rooms[i]._location;
            for (int j = 0; j < rooms.Count; ++j)
            {
                if (rooms[j]._location.Equals(locStart))
                {
                    if (rooms[j].Equals(rooms[0]))
                        continue;
                    else
                    {
                        ++result;
                        rooms.RemoveAt(j);
                        --j;//sets loop back one to compensate for item removal otherwise it would skip the item that is now at current j.
                    }
                }

            }
        }
        return result;
    }

}
