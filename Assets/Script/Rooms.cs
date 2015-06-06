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
    internal Location _location;
    internal List<bool> _doors;//north south east west
    public Rooms(int x, int y)
    {
        _location = new Location(x, y);
        _doors = new List<bool>();
        for (int i = 0; i < 4; ++i)
        {
            _doors.Add(false);
        }
    }
    public Rooms(Location loc, bool N, bool S, bool E, bool W)
    {
        _doors = new List<bool>();
        for (int i = 0; i < 4; ++i)
        {
            _doors.Add(false);
        }
        _location = loc;
        if (N)
            _doors[0] = N;
        if (S)
            _doors[1] = S;
        if (E)
            _doors[2] = E;
        if (W)
            _doors[3] = W;
    }
    public void setDoor(int door) // use 0 through 3 in order to set the door to true
    {
        _doors[door] = true;
    }
    public void printRoom()
    {
        _location.printLocation();
        System.Console.Write("North is: ");
        System.Console.Write(_doors[0]);
        System.Console.Write("\nSouth is: ");
        System.Console.Write(_doors[1]);
        System.Console.Write("\nEast is: ");
        System.Console.Write(_doors[2]);
        System.Console.Write("\nWest is: ");
        System.Console.Write(_doors[3]);
        System.Console.WriteLine();
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        var other = obj as Rooms;
        if ((System.Object)other == null) return false;

        return (other._location == this._location) && (other._doors[0] == this._doors[0]) && (other._doors[1] == this._doors[1]) &&
            (other._doors[2] == this._doors[2]) && (other._doors[3] == this._doors[3]);
    }
    public bool Equals(Rooms other)
    {
        if ((object)other == null) return false;

        return (other._location == this._location) && (other._doors[0] == this._doors[0]) && (other._doors[1] == this._doors[1]) &&
            (other._doors[2] == this._doors[2]) && (other._doors[3] == this._doors[3]);
    }
    public override int GetHashCode()
    {
        return _location.GetHashCode() ^ _doors.Count;
    }
}
