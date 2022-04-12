using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static float KEquation1(float v0, float a, float t)
    {
        return v0 + (a * t); // Returns v1, missing deltaX
    }

    public static float KEquation2(float v1, float v0, float t)
    {
        return t * ((v1 + v0) / 2); // Returns deltaX, missing a
    }

    public static float KEquation3(float v0, float a, float t)
    {
        return (v0 * t) + ((a * Mathf.Pow(t, 2)) / 2); // Returns deltaX, missing v1
    }

    public static float KEquation4(float v0, float a, float deltaX)
    {
        return Mathf.Sqrt(Mathf.Pow(v0, 2) + (2 * (a * deltaX))); // Returns v1, missing t
    }
}
