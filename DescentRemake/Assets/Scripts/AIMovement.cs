using UnityEngine;
using System.Collections.Generic;

public enum AIMovementPriority
{
    CollisionEvasion = 3,
    EvasiveManouver = 2,
    TrackingMovement = 1,
    FollowingMovement = 0,
    RandomManouver = -1,
    Station = -2,
    Patrol = -3,
}

public interface AIMovementController
{
    Vector3 AccelerationStep( AIMovementPriority priority, float maxAcceleration, Vector3 lowPriorityAcceleration );
    Vector3 AngularAccelerationStep( AIMovementPriority priority, float maxAngularAcceleration, Vector3 lowPriorityAcceleration );

    /* No op implementations:
    public Vector3 AccelerationStep(AIMovementPriority priority, float maxAcceleration, Vector3 lowPriorityAcceleration)
    {
        return lowPriorityAcceleration;
    }

    public Vector3 AngularAccelerationStep(AIMovementPriority priority, float maxAngularAcceleration, Vector3 lowPriorityAcceleration)
    {
        return lowPriorityAcceleration;
    }
    */
}

public class AIMovement : MonoBehaviour {
    public float Acceleration;
    public float AngularAcceleration;
    //public float DebugScale;

    SortedList<AIMovementPriority, AIMovementController> controllers = new SortedList<AIMovementPriority, AIMovementController>();
    public void Register(AIMovementPriority priority, AIMovementController controller )
    {
        controllers.Add(priority, controller);
    }
    public void Unregister(AIMovementPriority priority )
    {
        controllers.Remove(priority);
    }

    protected new Rigidbody rigidbody;
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        Vector3 accelerationStep = Vector3.zero;
        Vector3 angularAccelerationStep = Vector3.zero;
        foreach ( KeyValuePair<AIMovementPriority, AIMovementController> pair in controllers )
        {
            Vector3 refinedAccelerationStep = pair.Value.AccelerationStep(pair.Key, Acceleration, accelerationStep);
            if (!refinedAccelerationStep.IsNan())
                accelerationStep = refinedAccelerationStep;
            else
                Debug.Log("Nan on movement priority " + pair.Key.ToString());

            Vector3 refinedAngularAccelerationStep = pair.Value.AngularAccelerationStep(pair.Key, AngularAcceleration, angularAccelerationStep);
            if (!refinedAngularAccelerationStep.IsNan())
                angularAccelerationStep = refinedAngularAccelerationStep;
            else
                Debug.Log("Nan on movement priority" + pair.Key.ToString());
        }
        rigidbody.velocity += accelerationStep;
        rigidbody.angularVelocity += angularAccelerationStep;
    }

    public static Vector3 CombineAcceleration(Vector3 high, Vector3 low, float max)
    {
        float highMagnitude = high.magnitude;
        Vector3 result;
        if( highMagnitude < Mathf.Epsilon )
        {
            result = high + low;
            float mag = result.sqrMagnitude;
            if (mag > max*max)
                result *= (max / Mathf.Sqrt(mag));
            if (result.IsNan())
                Debug.Log("NaN");
            return result;
        }
        Vector3 highDirection = high / highMagnitude;
        float lowHighComponent = Vector3.Dot(highDirection, low);
        Vector3 nonHighComponent = low - lowHighComponent * highDirection;
        float combinedHighMagnitude = Mathf.Min(max, Mathf.Max(highMagnitude, lowHighComponent));
        Vector3 combinedHighComponent = highDirection * combinedHighMagnitude;

        result = combinedHighComponent + nonHighComponent;
        if (result.IsNan())
            Debug.Log("NaN");

        float sqrMax = max * max;
        if (sqrMax >= result.sqrMagnitude)
            return result;

        float nonHighSqrMagnitude = nonHighComponent.sqrMagnitude;
        Vector3 scaledNonHighComponent = nonHighComponent;
        if( nonHighSqrMagnitude > Mathf.Epsilon )
            scaledNonHighComponent *= Mathf.Sqrt(Mathf.Max(0, sqrMax - combinedHighMagnitude * combinedHighMagnitude)) / Mathf.Sqrt(nonHighSqrMagnitude);
        result = combinedHighComponent + scaledNonHighComponent;

        if (result.IsNan())
            Debug.Log("NaN");
        return result;
    }

    public Vector3 CalculateAngularAccelerationStep(Quaternion targetRotation)
    {
        // Calculate the target rotation relative to current rotation.
        Quaternion relativeTargetRotation = targetRotation * Quaternion.Inverse(transform.rotation);

        // Get the components of relative target rotation.
        Vector3 relativeTargetRotationAxis; float relativeTargetRotationAngle;
        relativeTargetRotation.ToAngleAxis(out relativeTargetRotationAngle, out relativeTargetRotationAxis);
        if (relativeTargetRotationAngle > 180f)
        {
            relativeTargetRotationAngle = 360f - relativeTargetRotationAngle;
            relativeTargetRotationAxis = -relativeTargetRotationAxis;
        }
        relativeTargetRotationAngle *= Mathf.PI / 180f;
        relativeTargetRotationAxis.Normalize();

        // Get the current angular velocity.
        Vector3 angularVelocity0 = rigidbody.angularVelocity;

        // Calculate the maximum angular velocity we want.
        float angularVelocityLimit = Mathf.Sqrt(2 * AngularAcceleration * relativeTargetRotationAngle);

        // Accelerate toward a point that is the relative angular position truncated to the velocity limit.
        Vector3 angularVelocityTarget = angularVelocityLimit * relativeTargetRotationAxis;
        Vector3 angularAccelerationStep = angularVelocityTarget - angularVelocity0;
        angularAccelerationStep *= Mathf.Min(1.0f, AngularAcceleration * Time.fixedDeltaTime / angularAccelerationStep.magnitude);

        /*
        if (DebugScale > 0)
        {
            Debug.DrawLine(transform.position + DebugScale * angularVelocityTarget, transform.position + DebugScale * relativeTargetRotationAngle * relativeTargetRotationAxis, Color.blue, 0.5f, false);
            Debug.DrawRay(transform.position, DebugScale * angularVelocityTarget, Color.green, 0.5f, false);
            Debug.DrawRay(transform.position, DebugScale * angularVelocity0, Color.red, 0.5f, false);
        }
        */

        // Apply acceleration.
        if (!float.IsNaN(angularAccelerationStep.x + angularAccelerationStep.y + angularAccelerationStep.z))
            return angularAccelerationStep;
        else
            return Vector3.zero;
    }
}
