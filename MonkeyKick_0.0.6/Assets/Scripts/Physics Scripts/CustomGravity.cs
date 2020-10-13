using UnityEngine;
using System.Collections.Generic;

public static class CustomGravity
{
    /// CUSTOM GRAVITY SCRIPT ///
    /// This script handles the custom gravity used for some puzzles and some
    /// attack / dodge sections of the game. Thanks @CatlikeCoding once again!
    /// 10/12/20

    /// VARIABLES ///
    // storing all gravity sources
    private static List<GravitySource> sources = new List<GravitySource>();

    /// FUNCTIONS ///
    /// GetGravity is a getter for... yknow, the gravity.
    public static Vector3 GetGravity (Vector3 position)
    {
        Vector3 g = Vector3.zero;
        for (int i = 0; i < sources.Count; i++)
        {
            g += sources[i].GetGravity(position);
        }

        return g;
    }
    // overloaded!
    public static Vector3 GetGravity(Vector3 position, out Vector3 upAxis)
    {
        Vector3 g = Vector3.zero;
        for (int i = 0; i < sources.Count; i++)
        {
            g += sources[i].GetGravity(position);
        }

        upAxis = -g.normalized;
        return g;
    }

    /// GetUpAxis is the getter for upAxis
    public static Vector3 GetUpAxis (Vector3 position)
    {
        Vector3 g = Vector3.zero;
        for (int i = 0; i < sources.Count; i++)
        {
            g += sources[i].GetGravity(position);
        }

        return -g.normalized;
    }

    /// Register adds a source to the list 
    public static void Register(GravitySource source)
    {
        Debug.Assert(!sources.Contains(source), "Duplicate registration of gravity source!", source);
        sources.Add(source);
    }

    /// Unregister removes a source from the list
    public static void Unregister(GravitySource source)
    {
        Debug.Assert(sources.Contains(source), "Unregistration of unknown gravity source!", source);
        sources.Remove(source);
    }
}
