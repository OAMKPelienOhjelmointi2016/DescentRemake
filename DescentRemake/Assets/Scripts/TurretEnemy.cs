using UnityEngine;
using System.Collections;

public class TurretEnemy : MonoBehaviour {

    public float shootingDistance = 10;
    public bool useMissiles = false;
    public bool useTripleShot = false;

    public float trackingVelocity;

    public float[] fireRates = {1, 1};

    private GameObject[] players;
    private GameObject nearestPlayer;
    private float nearestPlayerDistance = 1000;
    private FiringWeapons weapons;
    public HealthShield healthShield;

    public Transform turretPipe;
    public Transform turretWeapon;

    private GameObject instantiatedObj1;
    [SerializeField]
    private GameObject missilexplosion;


	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag("Player");
        weapons = this.turretWeapon.GetComponent<FiringWeapons>();
	}
    bool notDestroying = false;
    void LateUpdate() {
        //Destroy turret when health is 0
        if (healthShield.health == 0 && !notDestroying)
        {
            notDestroying = true;
            Destroy(this.gameObject, 0.5f);
            missilexplosion.transform.localScale = new Vector3(3f, 3f, 3f);
            instantiatedObj1 = (GameObject)Instantiate(missilexplosion, this.transform.position, this.transform.rotation);

            Destroy(instantiatedObj1, 1.8f);
        }
    }
	
	// Update is called once per frame
	void Update () {



        foreach (GameObject player in players) {
            if (Vector3.Distance(this.transform.position, player.transform.position) < nearestPlayerDistance)
            {
                nearestPlayerDistance = Vector3.Distance(this.transform.position, player.transform.position);
                nearestPlayer = player;
            }
        }
        if (Vector3.Distance(nearestPlayer.transform.position, this.transform.position) < shootingDistance)
        {
            float distanceToPlayer = Vector3.Distance(nearestPlayer.transform.position, this.transform.position);
            Vector3 nearestVelocity = nearestPlayer.GetComponent<Rigidbody>().velocity;
            System.Random randomizer = new System.Random();
            int anticipationLimit = randomizer.Next(10, 40);
            //Bullet speed + player speed
            //Replace bullet speed with parameter when available
            float travelTimeX = distanceToPlayer / (/*Mathf.Abs(anticipationLimit)*/Mathf.Abs(25) + nearestVelocity.x);
            float travelTimeY = distanceToPlayer / (/*Mathf.Abs(anticipationLimit)*/Mathf.Abs(25) + nearestVelocity.y);
            float travelTimeZ = distanceToPlayer / (/*Mathf.Abs(anticipationLimit)*/Mathf.Abs(25) + nearestVelocity.z);
            float futurePosX = travelTimeX * nearestVelocity.x;
            float futurePosY = travelTimeY * nearestVelocity.y;
            float futurePosZ = travelTimeZ * nearestVelocity.z;
            Vector3 nearestPosition = nearestPlayer.transform.position;
            Vector3 futurePosition = new Vector3(nearestPosition.x + futurePosX, nearestPosition.y + futurePosY, nearestPosition.z + futurePosZ);

            Quaternion targetPos = Quaternion.LookRotation(futurePosition - turretPipe.position, Vector3.right);
            //If you want turret to anticipate player movement, replace nearestPosition with futurePosition
            turretPipe.LookAt(nearestPosition, Vector3.forward);
            turretPipe.localEulerAngles = new Vector3(
                Mathf.Clamp(turretPipe.localEulerAngles.x, 270f, 270f),
                turretPipe.localEulerAngles.y - 90f,
                Mathf.Clamp(turretPipe.localEulerAngles.z, 0f, 0f)
            );
            //If you want turret to anticipate player movement, replace nearestPosition with futurePosition
            turretWeapon.LookAt(nearestPosition, Vector3.back);

            turretWeapon.transform.localEulerAngles = new Vector3(
                Mathf.Clamp(turretWeapon.localEulerAngles.x, 0f, 10f),
                Mathf.Clamp(turretWeapon.localEulerAngles.y, 30f, 120f),
                Mathf.Clamp(turretWeapon.localEulerAngles.z, 0f, 0f));

            if (Physics.Raycast(turretWeapon.transform.position, turretWeapon.forward, shootingDistance)) {
                if (!useTripleShot)
                    weapons.InitiateStandardShoot(fireRates[0], "standard");
                else
                    weapons.InitiateStandardShoot(fireRates[0], "triple");
                if (useMissiles)
                    weapons.InitiateAlternativeShoot(fireRates[1]);
            }
        }
    }
}
