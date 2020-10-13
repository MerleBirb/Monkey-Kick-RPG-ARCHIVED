using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CustomGravityRigidbody : MonoBehaviour
{
    /// CUSTOM GRAVITY RIGIDBODY ///
    /// as the title describes, helps the rigidbody get accustomed to the custom
    /// gravity. thanks again @CatlikeCoding! 10/12/2020

    /// VARIABLES ///
    // store the regular rigidbody
    private Rigidbody rb;
    // delay until gravity is put to "sleep" on the object
    private float floatDelay;
    // storing whether a rb is allowed to sleep or not
    [SerializeField]
    private bool floatToSleep = false;

    /// Awake is called when the object activates, or turns on
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    /// FixedUpdate happens once per frame at the fixed framerate
    void FixedUpdate()
    {
        if (floatToSleep)
        {
            if (rb.IsSleeping())
            {
                floatDelay = 0f;
                return;
            }

            if (rb.velocity.sqrMagnitude < 0.0001f)
            {
                floatDelay += Time.deltaTime;
                if (floatDelay >= 1f)
                {
                    return;
                }
            }
            else
            {
                floatDelay = 0f;
            }
        }  

        rb.AddForce(CustomGravity.GetGravity(rb.position), ForceMode.Acceleration);
    }
}
