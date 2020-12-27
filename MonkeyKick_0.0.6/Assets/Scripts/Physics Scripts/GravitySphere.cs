using UnityEngine;

public class GravitySphere : GravitySource
{
    /// GRAVITY SPHERE (GRAVITY SOURCE) ///
    /// a type of gravity that acts like a planetoid!
    /// thanks once again @Catlike Coding! 10/13/20

    /// VARIABLES ///
    // here it is folks, the gravity
    [SerializeField]
    private float gravity = 9.81f;
    // kinda like the range, but circular of course
    [SerializeField, Min(0f)]
    private float outerRadius = 10f;
    [SerializeField, Min(0f)]
    private float outerFallOffRadius = 15f;
    private float outerFallOfFactor, innerFallOffFactor;
    // the opposite: inner radius is for inverted spheres!
    [SerializeField, Min(0f)]
    private float innerFallOffRadius = 1f, innerRadius = 5f;

    /// FUNCTIONS ///
    /// Awake is called when the object activates, or turns on
    private void Awake()
    {
        OnValidate();
    }

    /// override the gravity function to work like a sphere instead!
    public override Vector3 GetGravity(Vector3 position)
    {
        Vector3 vector = transform.position - position;
        float distance = vector.magnitude;
        if (distance > outerFallOffRadius || distance < innerFallOffRadius)
        {
            return Vector3.zero;
        }

        float g = gravity / distance;
        if (distance > outerRadius)
        {
            g *= 1f - (distance - outerRadius) * outerFallOfFactor;
        }
        else if(distance < innerRadius)
        {
            g *= 1f - (innerRadius - distance) * innerFallOffFactor;
        }
        return g * vector;
    }

    /// draw the field of the gravity in the inspector
    private void OnDrawGizmos()
    {
        Vector3 p = transform.position;
        if (innerFallOffRadius > 0f && innerFallOffRadius < innerRadius)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(p, innerFallOffRadius);
        }

        Gizmos.color = Color.yellow;
        if (innerRadius > 0f && innerRadius < outerRadius)
        {
            Gizmos.DrawWireSphere(p, innerRadius);
        }

        Gizmos.DrawWireSphere(p, outerRadius);
        if (outerFallOffRadius > outerRadius)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(p, outerFallOffRadius);
        }
    }

    /// Forces the outerFallOfRadius to be bigger than the outerRadius
    private void OnValidate()
    {
        innerFallOffRadius = Mathf.Max(innerFallOffRadius, 0f);
        innerRadius = Mathf.Max(innerRadius, innerFallOffRadius);
        outerRadius = Mathf.Max(outerRadius, innerRadius);
        outerFallOffRadius = Mathf.Max(outerFallOffRadius, outerRadius);

        innerFallOffFactor = 1f / (innerRadius - innerFallOffRadius);
        outerFallOfFactor = 1f / (outerFallOffRadius - outerRadius);
    }
}
