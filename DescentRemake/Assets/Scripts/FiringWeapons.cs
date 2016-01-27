using UnityEngine;
using System.Collections;

public class FiringWeapons : MonoBehaviour {

    public GameObject bullet;
    public GameObject missile;
    public GameObject flare;
    private Transform bulletpointleft;
    private Transform bulletpointright;
    private Transform bulletpointupper;
    private Transform missilepoint;
    private float nextfire;
    private float nextmissile;
    private float nextitem;
    private float firerate;
    private float missilerate;
    private float itemrate;
    private string firemode;
    private bool autofire;

	// Use this for initialization
	void Start () {
        missilepoint = this.transform.Find("MissilePoint").transform;
        bulletpointleft = this.transform.Find("BulletPointLeft").transform;
        bulletpointright = this.transform.Find("BulletPointRight").transform;
        bulletpointupper = this.transform.Find("BulletPointUpper").transform;
        firemode = "standard";
        nextfire = 0f;
        nextmissile = 0f;
        nextitem = 0f;
        firerate = 0.2f;
        missilerate = 0.5f;
        itemrate = 1.0f;
        autofire = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (autofire && Time.time > nextfire)
        {
            Instantiate(bullet, bulletpointleft.position, bulletpointleft.rotation);
            Instantiate(bullet, bulletpointright.position, bulletpointright.rotation);
            nextfire = Time.time + firerate;
        }
    }

    public void InitiateStandardShoot(float rateForFire, string modeForFire)
    {
        firerate = rateForFire;
        firemode = modeForFire;
        StandardShoot();
    }

    private void StandardShoot()
    {
        if (Time.time > nextfire) {
            if (firemode == "standard") {
                Instantiate(bullet, bulletpointleft.position, bulletpointleft.rotation);
                Instantiate(bullet, bulletpointright.position, bulletpointright.rotation);
                nextfire = Time.time + firerate;
            }
            else if (firemode == "triple")
            {
                Instantiate(bullet, bulletpointleft.position, bulletpointleft.rotation);
                Instantiate(bullet, bulletpointright.position, bulletpointright.rotation);
                Instantiate(bullet, bulletpointupper.position, bulletpointupper.rotation);
                nextfire = Time.time + firerate;
            }
            else if (firemode == "auto")
            {
                autofire = true;
            }
        }
    }

    public void DisableAutofire()
    {
        autofire = false;
    }

    public void InitiateAlternativeShoot(float rateForMissile)
    {
        missilerate = rateForMissile;
        AlternativeShoot();
    }

    public void InitiateItemActivation(float rateForItem)
    {
        itemrate = rateForItem;
        ItemActivate();
    }

    private void AlternativeShoot()
    {
        if (Time.time > nextmissile)
        {
            Instantiate(missile, missilepoint.position, missilepoint.rotation);
            nextmissile = Time.time + missilerate;
        }
    }

    private void ItemActivate()
    {
        if (Time.time > nextitem)
        {
            Instantiate(flare, missilepoint.position, missilepoint.rotation);
            nextitem = Time.time + itemrate;
        }
    }
}
