using UnityEngine;
using System.Collections;

public class PlayerTracking : MonoBehaviour {

    public LayerMask TrackingLevelLayers;

    // The player we are targeting.
    private PlayerTarget targetedPlayer;
    public PlayerTarget TargetedPlayer
    {
        get { return targetedPlayer; }
    }

    protected new Rigidbody rigidbody;
    protected Tracking tracking;

    protected void Start () {
        tracking = GetComponent<Tracking>();
        updateTargetedPlayerRountine = StartCoroutine(UpdateTargetedPlayerRountine());
    }

    protected void OnDestroy()
    {
        StopCoroutine(updateTargetedPlayerRountine);
    }

    private Coroutine updateTargetedPlayerRountine;
    private IEnumerator UpdateTargetedPlayerRountine()
    {
        float interval = Random.Range(0.6f, 0.8f);
        while (true)
        {
            UpdateTargetedPlayer();
            yield return new WaitForSeconds(interval);
        }
    }

    private void UpdateTargetedPlayer()
    {
        RaycastHit hit;

        // Find nearest visible player.
        targetedPlayer = null;
        float targetedPlayerDistance2 = float.PositiveInfinity;
        foreach (PlayerTarget player in PlayerTarget.Players)
        {
            Vector3 relativePlayerPosition = player.transform.position - transform.position;
            float playerDistance2 = relativePlayerPosition.sqrMagnitude;

            if (playerDistance2 >= targetedPlayerDistance2)
                continue;

            if (Physics.Raycast(transform.position, relativePlayerPosition, out hit, Mathf.Sqrt(playerDistance2), TrackingLevelLayers))
                continue;

            targetedPlayer = player;
            targetedPlayerDistance2 = playerDistance2;
            tracking.TrackedTarget = player.GetComponent<Rigidbody>();
        }
    }

}
