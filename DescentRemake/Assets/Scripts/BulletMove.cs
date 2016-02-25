using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BulletMove : MonoBehaviour {

    private Vector3 direction;
    [SerializeField]
    private GameObject bullethiteffect;
    private GameObject player;
    //Projectile's speed
    [SerializeField]
    private float speed = 1000f;
    private float radius = 0.35f;
    private float power = 50.0f;
    private bool enemyshooter = false;
    public int bulletDamage = 5;
    public GameObject firedPlayer;




    // Use this for initialization
    void Start () {
        direction = this.transform.forward;
        player = GameObject.FindGameObjectWithTag("Player");
        //this.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
        this.GetComponent<Rigidbody>().AddForce(direction * speed);
        GameObject.Destroy(this.gameObject, 5f);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0f, 0f, 10f,Space.Self);
	}

    public void EnemyShotThisProjectile()
    {
        enemyshooter = true;
    }

    void OnTriggerEnter(Collider col)
    {



        HealthShield enemy = col.GetComponent<HealthShield>();

        /*if (enemy != null) {


            if (col.tag == "Player")
            {
                enemy.GetComponent<PhotonView>().RPC("takeDmg", PhotonTargets.AllBuffered, bulletDamage);
            }
            else
            {
                
            }
            
        }
        else
            Debug.Log("collisiontriggered");*/
        if (col.gameObject.tag != "Bullet") {
        Vector3 explosionPos = this.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Force);
                
        }
            Object bulletHit = Instantiate(bullethiteffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
            Destroy(bulletHit, 1.0f);
			if (enemy != null) {
                if (col.tag == "Player")
                {
                    if (SceneManagerHelper.ActiveSceneName == "MultiplayerMap1")
                    {
                        enemy.GetComponent<PhotonView>().RPC("takeDmg", PhotonTargets.AllBuffered, bulletDamage);
                        firedPlayer.GetComponent<FiringWeapons>().addHit();

                        if (enemy.GetComponent<HealthShield>().health < 0)
                        {
                            firedPlayer.GetComponent<FiringWeapons>().addKill();
                        }
                    }
                    else
                    {
                        enemy.takeDmg(bulletDamage);
                        if(enemy.health <= 0)
                        {
                            SceneManager.LoadScene("Menu");
                        }
                    }
                }
                else
                {
                    enemy.takeDmg(bulletDamage);
                }
			}
        }
    }

}
