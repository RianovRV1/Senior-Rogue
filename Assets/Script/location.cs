using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// contains an X and Y int coordinate pair. Overrides equals method for comparison operations
/// </summary>

public class Location : System.Object, IEquatable<Location>
{
    internal float _x { get; private set; } // auto implments getters and setters for x and y
    internal float _y { get; private set; }
    public Location(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public void printLocation()
    {
        System.Console.Write("X: ");
        System.Console.Write(_x.ToString("D"));
        System.Console.Write(", Y:");
        System.Console.Write(_y.ToString("D"));
        System.Console.WriteLine();
    }



    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        var other = obj as Location;
        if ((System.Object)other == null) return false;

        return (_x == other._x) && (_y == other._y);
    }
    public bool Equals(Location other)
    {
        if ((object)other == null)
            return false;

        return (_x == other._x) && (_y == other._y);
    }

    public override int GetHashCode()
    {
        return (int)_x ^ (int)_y;
    }

    public static bool operator ==(Location a, Location b)
    {
        // If both are null, or both are same instance, return true.
        if (System.Object.ReferenceEquals(a, b))
        {
            return true;
        }

        // If one is null, but not both, return false.
        if (((object)a == null) || ((object)b == null))
        {
            return false;
        }

        // Return true if the fields match:
        return a._x == b._x && a._y == b._y;
    }

    public static bool operator !=(Location a, Location b)
    {
        return !(a == b);
    }

    public static Location operator *(Location a, Location b)
    {
        return new Location(a._x * b._x, a._y * b._y);
    }
}


