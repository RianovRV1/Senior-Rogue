using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class Location : System.Object, IEquatable<Location>
{
    public int _x { get; private set; } // auto implments getters and setters for x and y
    public int _y { get; private set; }
    public Location(int x, int y)
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
        return _x ^ _y;
    }
}


