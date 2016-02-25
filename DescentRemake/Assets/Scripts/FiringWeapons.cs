using UnityEngine;
using System.Collections;

public class FiringWeapons : MonoBehaviour
{
    /* Weapon soundeffects */
    public AudioSource gunshot;
    public AudioSource missileshot;

    public GameObject bullet;
    public GameObject missile;
    public GameObject flare;
    public GameObject emp;
    public GameObject decoy;
    private GameObject instanceofcreatedprojectileleft;
    private GameObject instanceofcreatedprojectileright;
    private Transform bulletpointleft;
    private Transform bulletpointright;
    private Transform bulletpointupper;
    private Transform missilepoint;
    private Transform playerpoint;
    private Quaternion decoyrotationaddition;
    private Quaternion decoyrotation;
    private float nextfire;
    private float nextmissile;
    private float nextitem;
    private float firerate;
    private float missilerate;
    private float itemrate;
    private string firemode;
    private string itemname;
    private bool autofire;
    private bool isEnemy = false;
    private string url = "http://oamkpo2016.esy.es/kills";
    private bool bKilled = true;

    public int hitCount = 0;
    public int killCount = 0;
    public int fireCount = 0;

    PhotonView photonView;



    // Use this for initialization
    void Start()
    {
        photonView = PhotonView.Get(this);



        missilepoint = this.transform.Find("MissilePoint").transform;
        bulletpointleft = this.transform.Find("BulletPointLeft").transform;
        bulletpointright = this.transform.Find("BulletPointRight").transform;
        bulletpointupper = this.transform.Find("BulletPointUpper").transform;
        playerpoint = this.transform;
        firemode = "standard";
        nextfire = 0f;
        nextmissile = 0f;
        nextitem = 0f;
        firerate = 0.2f;
        missilerate = 0.5f;
        itemrate = 1.0f;
        autofire = false;

        if (this.gameObject.tag == "Turret")
        {
            isEnemy = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (autofire && Time.time > nextfire)
        {
            Instantiate(bullet, bulletpointleft.position, bulletpointleft.rotation);
            Instantiate(bullet, bulletpointright.position, bulletpointright.rotation);
            nextfire = Time.time + firerate;

        }
        if (bullet != null)
        {
            bullet.GetComponent<BulletMove>().firedPlayer = gameObject;
            missile.GetComponent<MissileMove>().firedPlayer = gameObject;
        }



    }


    public void addHit()
    {
        hitCount++;
    }

    public void addFire(int iFired)
    {
        fireCount = fireCount + iFired;
    }

    public void addKill()
    {
        if (bKilled)
        {
            killCount++;
            Debug.Log("Adding kill");
            GetComponent<ChatManager>().killStreak(GetComponent<ChatManager>().username, killCount);
            StartCoroutine(PostKill());
            bKilled = false;
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
        if (Time.time > nextfire)
        {

            gunshot.Play();
            if (firemode == "standard")
            {

                if (SceneManagerHelper.ActiveSceneName == "SPlevel" || SceneManagerHelper.ActiveSceneName == "Tutorial")
                {
                    instanceofcreatedprojectileleft = Instantiate(bullet, bulletpointleft.position, bulletpointleft.rotation) as GameObject;
                    instanceofcreatedprojectileright = Instantiate(bullet, bulletpointright.position, bulletpointright.rotation) as GameObject;
                }
                else
                {
                    photonView.RPC("BulletFX", PhotonTargets.All, bulletpointright.position, bulletpointright.rotation);
                    photonView.RPC("BulletFX", PhotonTargets.All, bulletpointleft.position, bulletpointleft.rotation);
                }
                if (isEnemy)
                {
                    instanceofcreatedprojectileleft.GetComponent<BulletMove>().EnemyShotThisProjectile();
                    instanceofcreatedprojectileright.GetComponent<BulletMove>().EnemyShotThisProjectile();
                }
                nextfire = Time.time + firerate;
                addFire(2);
            }
            else if (firemode == "triple")
            {
                Instantiate(bullet, bulletpointleft.position, bulletpointleft.rotation);
                Instantiate(bullet, bulletpointright.position, bulletpointright.rotation);
                Instantiate(bullet, bulletpointupper.position, bulletpointupper.rotation);
                nextfire = Time.time + firerate;
                addFire(3);
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

    public void InitiateItemActivation(string nameForItem, float rateForItem)
    {
        itemrate = rateForItem;
        itemname = nameForItem;
        ItemActivate();
    }

    private void AlternativeShoot()
    {
        if (Time.time > nextmissile)
        {
            missileshot.Play();

            if (SceneManagerHelper.ActiveSceneName == "SPlevel" || SceneManagerHelper.ActiveSceneName == "Tutorial")
            {
                Instantiate(missile, missilepoint.position, missilepoint.rotation);
                nextmissile = Time.time + missilerate;
            }
            else
            {
                photonView.RPC("MissileFX", PhotonTargets.All, missilepoint.position, missilepoint.rotation);
                nextmissile = Time.time + missilerate;
                addFire(1);
            }
        }
    }

    private void ItemActivate()
    {
        if (Time.time > nextitem && itemname == "flare")
        {
            Instantiate(flare, missilepoint.position, missilepoint.rotation);
            nextitem = Time.time + itemrate;
        }
        else if (Time.time > nextitem && itemname == "emp")
        {
            Instantiate(emp, playerpoint.position, playerpoint.rotation);
            nextitem = Time.time + itemrate;
        }
        else if (Time.time > nextitem && itemname == "decoy")
        {
            decoyrotation = missilepoint.rotation;
            decoyrotation *= Quaternion.Euler(90, 0, 0);
            Instantiate(decoy, missilepoint.position, decoyrotation);
            nextitem = Time.time + itemrate;
        }
    }

    [PunRPC]
    void BulletFX(Vector3 bulletPointPosition, Quaternion bulletPointRotation)
    {
        Debug.Log(bulletPointPosition);
        Instantiate(bullet, bulletPointPosition, bulletPointRotation);


    }

    [PunRPC]
    void MissileFX(Vector3 MissilePointPosition, Quaternion MissilePointRotation)
    {
        Debug.Log(MissilePointPosition);
        Instantiate(missile, MissilePointPosition, MissilePointRotation);
    }




    IEnumerator PostKill()
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("ID", GetComponent<ChatManager>().id.ToString());
        wwwForm.AddField("kills", "1");
        WWW hs_post = new WWW(url, wwwForm);

        yield return hs_post;
        bKilled = true;

        if (hs_post.error != null)
        {
            Debug.Log("Error posting data to database: " + hs_post.error);
        }

    }
}