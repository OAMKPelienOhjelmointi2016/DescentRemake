using UnityEngine;
using System.Collections;

public class MissileMove : MonoBehaviour
{
    private Vector3 direction;
    [SerializeField]
    private GameObject missilexplosion;
    private GameObject player;
    //Projectile's speed
    [SerializeField]
    private float speed = 1f;
    private float radius = 15.0f;
    private float power = 100.0f;
    private GameObject instantiatedObj;
    public int missileDamage = 20;

    void Start()
    {
        direction = this.transform.forward;
        player = GameObject.FindGameObjectWithTag("Player");
        //this.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
        GameObject.Destroy(this.gameObject, 10f);
    }

    void Update()
    {
        if (speed < 50)
        {
            speed+=2.5f;
        }
        this.GetComponent<Rigidbody>().AddForce(direction * speed);
    }

    void OnTriggerEnter(Collider col)
    {

        HealthShield enemy = col.GetComponent<HealthShield>();

        if (enemy != null) {


            if (col.tag == "Player")
            {
                enemy.GetComponent<PhotonView>().RPC("takeDmg", PhotonTargets.AllBuffered, missileDamage);
            }
            else
            {
                enemy.takeDmg(missileDamage);
            }

        if (col.gameObject.tag != "Bullet" && col.gameObject.tag != "Player")
        {
            Vector3 explosionPos = this.transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Force);
            }
            instantiatedObj = (GameObject) Instantiate(missilexplosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);

            Destroy(instantiatedObj, 1.8f);
        }
    }
}
