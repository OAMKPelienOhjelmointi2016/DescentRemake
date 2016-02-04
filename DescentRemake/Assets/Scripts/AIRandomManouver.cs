using UnityEngine;
using System.Collections;

public class AIRandomManouver : MonoBehaviour, AIMovementController, AITargetObserver
{
    public float MinManouverTime;
    public float MaxManouverTime;

    
    private Rigidbody target;
    //[HideInInspector]
    public Rigidbody Target
    {
        get { return target; }
        set {
            target = value;
            if( target != null )
            {
                if( updateManouverDirection == null )
                    updateManouverDirection = StartCoroutine(UpdateManouverDirection());
            }
            else
            {
                if (updateManouverDirection != null)
                {
                    StopCoroutine(updateManouverDirection);
                    updateManouverDirection = null;
                }
            }
        }
    }

    private AIMovement movement;
    private Vector3 manouverDirection = Vector3.zero;

    // Use this for initialization
    protected void Start () {
        movement = GetComponent<AIMovement>();
        movement.Register(AIMovementPriority.RandomManouver, this);
    }
    protected void OnDestroy()
    {
        if (movement != null)
            movement.Unregister(AIMovementPriority.RandomManouver);
    }

    Coroutine updateManouverDirection;
    private IEnumerator UpdateManouverDirection()
    {
        while (true)
        {
            Vector3 newDirection = Random.insideUnitCircle;
            Quaternion look = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            newDirection = look * newDirection;

            if (Vector3.Dot(manouverDirection, newDirection) >= 0)
                manouverDirection = newDirection;
            else
                manouverDirection = -newDirection;

            yield return new WaitForSeconds(Random.Range(MinManouverTime, MaxManouverTime));
        }
    }

    public Vector3 AccelerationStep(AIMovementPriority priority, float maxAcceleration, Vector3 lowPriorityAcceleration)
    {
        if (!enabled || Target == null)
            return lowPriorityAcceleration;

        Vector3 result = Time.fixedDeltaTime * manouverDirection * maxAcceleration;
        if (result.IsNan())
            Debug.Log("Nan");
        return result;
    }

    public Vector3 AngularAccelerationStep(AIMovementPriority priority, float maxAngularAcceleration, Vector3 lowPriorityAcceleration)
    {
        return lowPriorityAcceleration;
    }

    
    public void Update()
    {
        Debug.DrawRay(transform.position, manouverDirection * 4, Color.black);
    }
    // */
}
