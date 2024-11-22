using System;

public struct Vector3D
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public double Magnitude
    {
        get
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }
    }

    public static Vector3D operator +(Vector3D v1, Vector3D v2)
    {
        return new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    }

    public static Vector3D operator -(Vector3D v1, Vector3D v2)
    {
        return new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
    }

    public static Vector3D operator *(Vector3D v, double scalar)
    {
        return new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
    }

    public static Vector3D operator *(double scalar, Vector3D v)
    {
        return new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
    }

    public static Vector3D operator /(Vector3D v, double scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Деление на ноль невозможно.");
        return new Vector3D(v.X / scalar, v.Y / scalar, v.Z / scalar);
    }

    public static bool operator ==(Vector3D v1, Vector3D v2)
    {
        return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
    }

    public static bool operator !=(Vector3D v1, Vector3D v2)
    {
        return !(v1 == v2);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }

    public static Vector3D CrossProduct(Vector3D v1, Vector3D v2)
    {
        return new Vector3D(
            v1.Y * v2.Z - v1.Z * v2.Y,
            v1.Z * v2.X - v1.X * v2.Z,
            v1.X * v2.Y - v1.Y * v2.X
        );
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector3D)
        {
            Vector3D v = (Vector3D)obj;
            return this == v;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return (X, Y, Z).GetHashCode();
    }
}
