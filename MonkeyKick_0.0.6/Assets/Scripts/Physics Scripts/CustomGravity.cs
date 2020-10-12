using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomGravity
{
    /// CUSTOM GRAVITY SCRIPT ///
    /// This script handles the custom gravity used for some puzzles and some
    /// attack / dodge sections of the game. Thanks @CatlikeCoding once again!
    /// 10/12/20

    /// FUNCTIONS ///
    /// GetGravity is a getter for... yknow, the gravity.
    public static Vector3 GetGravity (Vector3 position)
    {
        return position.normalized * Physics.gravity.y;
    }
    // overloaded!
    public static Vector3 GetGravity(Vector3 position, out Vector3 upAxis)
    {
        Vector3 up = position.normalized;
        upAxis = Physics.gravity.y < 0f ? up : -up;
        return upAxis * Physics.gravity.y;
    }

    /// GetUpAxis is the getter for upAxis
    public static Vector3 GetUpAxis (Vector3 position)
    {
        Vector3 up = position.normalized;
        return Physics.gravity.y < 0f ? up : -up;
    }
}
