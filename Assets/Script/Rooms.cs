using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// this rooms class is an object which that is used by the floor class for floor generation, it contains overrided equals logic, a location class
/// and a set of bools representing the doors
/// </summary>

public class Rooms : System.Object, IEquatable<Rooms>
{
    internal Location location;
    internal List<bool> doors;//north south east west
    public Rooms(int x, int y)
    {
        location = new Location(x, y);
        doors = new List<bool>();
        for (int i = 0; i < 4; ++i)
        {
            doors.Add(false);
        }
    }
    public Rooms(Location loc, bool N, bool S, bool E, bool W)
    {
        doors = new List<bool>();
        for (int i = 0; i < 4; ++i)
        {
            doors.Add(false);
        }
        location = loc;
        if (N)
            doors[0] = N;
        if (S)
            doors[1] = S;
        if (E)
            doors[2] = E;
        if (W)
            doors[3] = W;
    }
    public void setDoor(int door) // use 0 through 3 in order to set the door to true
    {
        doors[door] = true;
    }
    public void printRoom()
    {
        location.printLocation();
        System.Console.Write("North is: ");
        System.Console.Write(doors[0]);
        System.Console.Write("\nSouth is: ");
        System.Console.Write(doors[1]);
        System.Console.Write("\nEast is: ");
        System.Console.Write(doors[2]);
        System.Console.Write("\nWest is: ");
        System.Console.Write(doors[3]);
        System.Console.WriteLine();
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        var other = obj as Rooms;
        if ((System.Object)other == null) return false;

        return (other.location == this.location) && (other.doors[0] == this.doors[0]) && (other.doors[1] == this.doors[1]) &&
            (other.doors[2] == this.doors[2]) && (other.doors[3] == this.doors[3]);
    }
    public bool Equals(Rooms other)
    {
        if ((object)other == null) return false;

        return (other.location == this.location) && (other.doors[0] == this.doors[0]) && (other.doors[1] == this.doors[1]) &&
            (other.doors[2] == this.doors[2]) && (other.doors[3] == this.doors[3]);
    }
    public override int GetHashCode()
    {
        return location.GetHashCode() ^ doors.Count;
    }
}
