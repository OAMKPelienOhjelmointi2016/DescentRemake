using UnityEngine;
using System.Collections;

public class PlayerSpotlight : MonoBehaviour {

    public bool on = false;
    public float battery;
    public float maxbattery;
    

	// Use this for initialization
	void Start () {

        
	
	}

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.R))
        {
            on = !on;

        }

        if (on)
        {
               GetComponent<Light>().enabled = true;
                //battery -= Time.deltaTime;
               // Debug.Log("Battery life: " + battery);
        }
        
      else if (!on)
        {
            GetComponent<Light>().enabled = false;
            //battery += Time.deltaTime;
           // Debug.Log("Battery life: " + battery);
        }

        //if (battery <= 0)
        {
            //battery = 0;
            //GetComponent<Light>().enabled = false;
            //on = !on;
        }

        //if(battery >= maxbattery /*10*/)
        {
            //battery = maxbattery; /*10;*/

        }



    }

    

}
