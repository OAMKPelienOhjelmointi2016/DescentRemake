using UnityEngine;
using System.Collections;

public class ShieldEffect : MonoBehaviour {

    public Color colorStart;
    public Color colorEnd;
    public float waitTime = 0;
    public bool triggered = false;
    public bool colliderActive = true;
    public GameObject hostObject;

	// Use this for initialization
	void Start () {

        colorStart = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
        //shield = hostObject.GetComponent<HealthShield>().shield;
        //maxShield = hostObject.GetComponent<HealthShield>().maxShield;
	
	}

    // Update is called once per frame
    void Update()
    {
        if(hostObject.GetComponent<HealthShield>().shield <= 2)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorEnd);
            colorEnd.a = 0.0f;
            colliderActive = false;

        }
        else
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorStart);
            colliderActive = true;
        }
            
        if (triggered == true)
        {
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorEnd);
            colorEnd.a += 0.01f;
            waitTime += Time.deltaTime;

            if (waitTime >= 0.75f)
            {
                colorEnd.a -= 0.02f;

                if (colorEnd.a <= colorStart.a)
                {
                    triggered = false;
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", colorStart);
                    waitTime = 0;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (colliderActive == true)
        {
            if (other.gameObject.tag == "Bullet")
            {
                triggered = true;
            }

            hostObject.GetComponent<HealthShield>().takeDmg(other.gameObject.GetComponent<BulletMove>().bulletDamage);
        }
    }
}

