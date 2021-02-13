using UnityEngine;
using CatlikeCoding.Movement;

[RequireComponent(typeof(Camera))]
public class DynamicCameraOW : MonoBehaviour
{
    /// DYNAMIC CAMERA (OVERWORLD) SCRIPT ///
    /// This script handles how the camera works, how it follows the player, and how it 
    /// moves around / through objects. Once again thank you @CatlikeCoding for your amazing
    /// guides! 10/10/20

    /// VARIABLES ///
    /// starting postion variables
    // the starting distance
    [SerializeField]
    private float distance = 6.5f;
    // what gravity the camera is aligned with
    private Quaternion gravityAlignment = Quaternion.identity;

    /// orbiting variables
    // the orientation of the camera can be described with two orbit angles, self explanatory
    private Vector2 orbitAngles = new Vector2(30f, 0f);
    // the speed in which the camera rotates, measured in degrees
    [SerializeField, Range(0f, 360f)]
    private float rotationSpeed = 90f;
    // constrains the angles so they dont get too big and weird
    [SerializeField, Range(-89f, 89f)]
    private float minVerticalAngle = -30f, maxVerticalAngle = 60f;
    // stores the rotation of the camera
    private Quaternion orbitRotation;

    /// focus variables
    // what transform to focus at
    [SerializeField]
    private Transform focus = default;
    // the distance the player can go until the camera starts following the player
    [SerializeField, Min(0f)]
    private float focusRadius = 1f;
    // saving the focus point in a variable
    private Vector3 focusPoint, previousFocusPoint;
    // centering speed, by percentage
    [SerializeField, Range(0f, 1f)]
    private float focusCentering = 0.5f;
    // how long it takes for the camera to align back to the player after rotating
    //[SerializeField, Min(0f)]
    //private float alignDelay = 3f;
    // smooth out the alignment
    [SerializeField, Range(0f, 90f)]
    private float alignSmoothRange = 45f;
    // keep track of last time that a manual rotation happened.
    private float lastManualRotationTime;
    // the speed in which the camera flips when switching gravity
    [SerializeField, Min(0f)]
    private float upAlignmentSpeed = 360f;

    /// clipping / zoom variables
    // a reference to the regular camera
    private Camera regularCamera;
    // camera clip limit 
    [SerializeField, Min(0f)]
    private float cameraClipLimit = 0.5f;
    // what layers to ignore
    [SerializeField]
    private LayerMask obstructionMask = -1;

    /// FUNCTIONS ///
    /// Awake is called when the object is activated or turned on
    void Awake()
    {
        regularCamera = GetComponent<Camera>();
        focus = FindObjectOfType<PlayerMovement>().transform;
        focusPoint = focus.position;
        transform.localRotation = orbitRotation = Quaternion.Euler(orbitAngles);
    }

    /// LateUpdate is called once per frame at the end of the frame
    void LateUpdate()
    {
        UpdateGravityAlignment();
        UpdateFocusPoint();
        LookAt();
    }

    /// LookAt keeps the camera focused and looking at the focus
    private void LookAt()
    {
        //if (ManualRotation()/* || AutomaticRotation()*/)
        //{
        //    ConstrainAngles();
        //    orbitRotation = Quaternion.Euler(orbitAngles);
        //}

        Quaternion lookRotation = gravityAlignment * orbitRotation;

        Vector3 lookDirection = transform.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;

        Vector3 rectOffSet = lookDirection * regularCamera.nearClipPlane;
        Vector3 rectPosition = lookPosition + rectOffSet;
        Vector3 castFrom = focus.position;
        Vector3 castLine = rectPosition - castFrom;
        float castDistance = castLine.magnitude;
        Vector3 castDirection = castLine / castDistance;

        if (Physics.BoxCast(castFrom, CameraClip, castDirection, 
            out RaycastHit hit, lookRotation, castDistance, obstructionMask))
        {
            rectPosition = castFrom + castDirection * hit.distance;
            lookPosition = rectPosition - rectOffSet;
        }

        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    /// UpdateGravityAlignment keeps the camera updated with the current gravity
    private void UpdateGravityAlignment()
    {
        Vector3 fromUp = gravityAlignment * Vector3.up;
        Vector3 toUp = CustomGravity.GetUpAxis(focusPoint);
        float dot = Mathf.Clamp(Vector3.Dot(fromUp, toUp), -1f, 1f);
        float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
        float maxAngle = upAlignmentSpeed * Time.deltaTime;

        Quaternion newAlignment = Quaternion.FromToRotation(fromUp, toUp) * gravityAlignment;
        if (angle <= maxAngle)
        {
            gravityAlignment = newAlignment;    
        }
        else
        {
            gravityAlignment = Quaternion.SlerpUnclamped(gravityAlignment, newAlignment, maxAngle / angle);
        }
    }

    /// UpdateFocusPoint relaxes the camera and moves the focus point
    private void UpdateFocusPoint()
    {
        previousFocusPoint = focusPoint;
        Vector3 targetPoint = focus.position;

        if (focusRadius > 0f)
        {
            float vectDistance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (vectDistance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }

            if (vectDistance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / vectDistance);
            }

            focusPoint = targetPoint;
            //focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }

    /// ManualRotation takes input and rotates the camera. might use it, might not.
    private bool ManualRotation()
    {
        Vector2 input = new Vector2(Input.GetAxis("Vertical Camera"), Input.GetAxis("Horizontal Camera"));
        const float e = 0.001f;

        // it only returns when a change is made
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            lastManualRotationTime = Time.unscaledTime;
            return true;
        }

        return false;
    }

    /*/// AutomaticRotation returns the orbit when not manually moved
    private bool AutomaticRotation()
    {
        if (Time.unscaledTime - lastManualRotationTime < alignDelay)
        {
            return false;
        }

        Vector3 alignedDelta = Quaternion.Inverse(gravityAlignment) * (focusPoint - previousFocusPoint);
        Vector2 movement = new Vector2(alignedDelta.x, alignedDelta.z);
        float movementDeltaSqr = movement.sqrMagnitude;

        if (movementDeltaSqr < 0.000001f)
        {
            return false;
        }

        float headingAngle = GetAngle(movement / Mathf.Sqrt(movementDeltaSqr));
        float deltaAbs = Mathf.Abs(Mathf.DeltaAngle(orbitAngles.y, headingAngle));
        float rotationChange = rotationSpeed * Mathf.Min(Time.unscaledDeltaTime, movementDeltaSqr);

        if (deltaAbs < alignSmoothRange)
        {
            rotationChange *= deltaAbs / alignSmoothRange;
        }
        else if (180f - deltaAbs < alignSmoothRange)
        {
            rotationChange *= (180f - deltaAbs) / alignSmoothRange;
        }

        orbitAngles.y = Mathf.MoveTowardsAngle(orbitAngles.y, headingAngle, rotationChange);
        return true;
    }*/

    /// ConstrainAngles does what its named
    private void ConstrainAngles()
    {
        orbitAngles.x = Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y >= 360f)
        {
            orbitAngles.y -= 360f;
        }
    }

    /// OnValidate doesnt allow the constraints to go overboard / underboard
    private void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    /// CameraClip calculates the boxcast for clipping
    // wow, my first getter function, i must be getting smart hahaha
    private Vector3 CameraClip
    {
        get
        {
            Vector3 halfExtends;
            halfExtends.y = regularCamera.nearClipPlane * Mathf.Tan(cameraClipLimit * Mathf.Deg2Rad * regularCamera.fieldOfView);
            halfExtends.x = halfExtends.y * regularCamera.aspect;
            halfExtends.z = 0f;
            return halfExtends;
        }
    }

    /// VERY USEFUL
    public static float GetAngle(Vector2 direction)
    {
        float angle = Mathf.Acos(direction.y) * Mathf.Rad2Deg;
        return direction.x < 0f ? 360f - angle: angle;
    }
}
