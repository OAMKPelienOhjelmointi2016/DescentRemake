using UnityEngine;
using System.Collections;
using System;

public class AITargetFollowing : MonoBehaviour, AIMovementController, AITargetObserver {
    private Rigidbody target;
    [HideInInspector]
    public Rigidbody Target
    {
        get { return target; }
        set { target = value; }
    }

    public float MinPreferredDistance;
    public float MaxPreferredDistance;
    public float MaxNonTargetVelocity;

    new protected Rigidbody rigidbody;
    protected AIMovement movement;
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<AIMovement>();

        movement.Register(AIMovementPriority.FollowingMovement, this);
    }
    protected void OnDestroy()
    {
        if(movement != null)
        movement.Unregister(AIMovementPriority.FollowingMovement);
    }

    public Vector3 AccelerationStep(AIMovementPriority priority, float maxAcceleration, Vector3 lowPriorityAcceleration)
    {
        if(!enabled || Target == null)
            return lowPriorityAcceleration;

        //float absoluteMaxVelocity = Mathf.Sqrt(2 * maxAcceleration * (MaxPreferredDistance - MinPreferredDistance));
        Vector3 relativeTargetPosition = Target.transform.position - transform.position;
        float targetDistance = relativeTargetPosition.magnitude;
        Vector3 targetDirection = relativeTargetPosition / targetDistance;
        float targetVelocityComponent = Vector3.Dot(targetDirection, rigidbody.velocity);
        Vector3 nonTargetVelocityComponent = rigidbody.velocity - targetVelocityComponent * targetDirection;

        float boundaryDistance;
        float sign;
        bool accel;
        if( targetDistance < MinPreferredDistance)
        {
            boundaryDistance = MaxPreferredDistance - targetDistance;
            sign = -1;
            accel = true;
        }
        else if(targetDistance > MaxPreferredDistance)
        {
            boundaryDistance = targetDistance - MinPreferredDistance;
            sign = 1;
            accel = true;
        } else if (targetVelocityComponent < 0)
        {
            boundaryDistance = MaxPreferredDistance - targetDistance;
            sign = 1;
            accel = false;
        }
        else
        {
            boundaryDistance = targetDistance - MinPreferredDistance;
            sign = -1;
            accel = false;
        }

        float targetVelocity = sign * Mathf.Sqrt(2 * Mathf.Max(0, boundaryDistance) * maxAcceleration);
        //targetVelocity = Mathf.Clamp(targetVelocity, -absoluteMaxVelocity, absoluteMaxVelocity);
        if (!accel && Mathf.Abs(targetVelocityComponent) < targetVelocity)
            return lowPriorityAcceleration;

        float maxStep = Time.fixedDeltaTime * maxAcceleration;
        float targetAccelStep = targetVelocity - targetVelocityComponent;
        targetAccelStep = Mathf.Clamp(targetAccelStep, -maxStep, maxStep);

        Vector3 accelStep = targetAccelStep * targetDirection;

        //if (targetDirection.IsNan() || float.IsNaN(accelStep) || float.IsNaN(maxStep) || lowPriorityAcceleration.IsNan())
        //    Debug.Log("NaN");

        float nonTargetVelocity = nonTargetVelocityComponent.magnitude;
        if( nonTargetVelocity > MaxNonTargetVelocity )
        {
            Vector3 nonTargetAccelStep = nonTargetVelocityComponent * (Mathf.Max(-maxStep, MaxNonTargetVelocity - nonTargetVelocity) / nonTargetVelocity);
            accelStep = AIMovement.CombineAcceleration(nonTargetAccelStep, accelStep, maxStep);
        }

        Debug.Log("Distance: " + targetDistance + " Component: " + targetVelocityComponent + " Boundary: " + boundaryDistance + " Accel: " + (accelStep / Time.fixedDeltaTime));

        return AIMovement.CombineAcceleration(accelStep, lowPriorityAcceleration, maxStep);
    }

    public Vector3 AngularAccelerationStep(AIMovementPriority priority, float maxAngularAcceleration, Vector3 lowPriorityAcceleration)
    {
        return lowPriorityAcceleration;
    }
}
