using UnityEngine;

public class GravityPlane : GravitySource
{
    /// GRAVITY PLANE (GRAVITY SOURCE) ///
    /// A type of gravity that is simple: makes a plane of gravity.
    /// thanks @Catlike Coding! 10/12/2020

    /// VARIABLES ///
    // here it is folks, the gravity
    [SerializeField]
    private float gravity = 9.81f;
    // the range for the gravity
    [SerializeField, Min(0f)]
    private float range = 1f;

    /// FUNCTIONS ///
    /// override GetGravity
    public override Vector3 GetGravity(Vector3 position)
    {
        Vector3 up = transform.up;
        float distance = Vector3.Dot(up, position - transform.position);
        if (distance > range)
        {
            return Vector3.zero;
        }
        return -gravity * up;
    }

    /// draw the field of the gravity in the inspector
    private void OnDrawGizmos()
    {
        Vector3 scale = transform.localScale;
        scale.y = range;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, scale);
        Vector3 size = new Vector3(1f, 0f, 1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, size);

        if (range > 0f)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Vector3.up, size);
        }
    }
}
