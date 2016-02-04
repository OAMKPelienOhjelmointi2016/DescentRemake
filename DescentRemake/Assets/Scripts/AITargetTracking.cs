using UnityEngine;
using System.Collections;
using System;

public delegate void TargetAcquisitionEvent(Rigidbody body, float distance, float errorDistance);

public class AITargetTracking : MonoBehaviour, AIMovementController, AITargetObserver {
    public event TargetAcquisitionEvent TargetAquired;

    private Rigidbody target;
    [HideInInspector]
    public Rigidbody Target
    {
        get { return target; }
        set { target = value; }
    }

    [HideInInspector]
    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    [HideInInspector]
    public Vector3 TargetPoint
    {
        get { return targetPoint; }
    }

    protected new Rigidbody rigidbody;
    protected AIMovement movement;
    protected void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movement = GetComponent<AIMovement>();
        targetRotation = transform.rotation;

        movement.Register(AIMovementPriority.TrackingMovement, this);
    }
    protected void OnDestroy()
    {
        if (movement != null)
            movement.Unregister(AIMovementPriority.TrackingMovement);
    }

    private Vector3 CalculateTargetPoint()
    {
        return Target.transform.position + rigidbody.centerOfMass;
    }

    private Quaternion CalculateTargetRotation(Vector3 targetPoint)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position, Vector3.up);
        return targetRotation;
    }

    private Vector3 targetPoint;
    private Quaternion targetRotation;
    public Vector3 AngularAccelerationStep(AIMovementPriority priority, float maxAngularAcceleration, Vector3 lowPriorityAcceleration)
    {
        if(!enabled)
            return lowPriorityAcceleration;

        if (Target != null)
            targetPoint = CalculateTargetPoint();

        targetRotation = CalculateTargetRotation(targetPoint);
        return movement.CalculateAngularAccelerationStep(targetRotation);
    }

    private void SignalTargetAcquisition()
    {
        if (TargetAquired == null || Target == null)
            return;

        Vector3 relativeTargetPoint = targetPoint - transform.position;
        Vector3 forward = transform.forward;
        float distance = Vector3.Dot(relativeTargetPoint, forward);
        if (distance < 0) return;

        float errorDistance = (distance * forward - relativeTargetPoint).magnitude;
        TargetAquired(Target, distance, errorDistance);
    }

    protected void Update()
    {
        SignalTargetAcquisition();

        /*
        Debug.DrawRay(transform.position, 2 * transform.forward, Color.yellow);
        if (Input.GetKeyDown(KeyCode.Keypad5))
            rigidbody.angularVelocity = AngularAcceleration * Mathf.PI * Random.Range(3, 5) * Random.onUnitSphere;
        */
    }

    public Vector3 AccelerationStep(AIMovementPriority priority, float maxAcceleration, Vector3 lowPriorityAcceleration)
    {
        return lowPriorityAcceleration;
    }
}
