using UnityEngine;

public class GravitySource : MonoBehaviour
{
    /// GRAVITY SOURCE ///
    /// determines what the source of gravity is, can have multiple on the scene! 
    /// thank you @Catlike Coding! 10/12/2020
    
    /// FUNCTIONS ///
    /// GetGravity is similar to CustomGravity's, except this one is default gravity and non static
    public virtual Vector3 GetGravity(Vector3 position)
    {
        return Physics.gravity;
    }

    /// OnEnable or OnDisable turn the gravity source on or off
    private void OnEnable()
    {
        CustomGravity.Register(this);
    }
    private void OnDisable()
    {
        CustomGravity.Unregister(this);
    }
}
