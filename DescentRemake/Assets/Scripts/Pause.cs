using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    GameObject pause;
    GameObject player;
    bool paused;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        pause = GameObject.Find("PauseButton");
        pause.SetActive(false);
        paused = false;
	}

    public void ClickPause()
    {
        player.GetComponent<PlayerSpotlight>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerShoot>().enabled = true;
        Time.timeScale = 1f;
        paused = !paused;
        pause.SetActive(false);
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                player.GetComponent<PlayerSpotlight>().enabled = false;
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerShoot>().enabled = false;
                Cursor.visible = true;
                Time.timeScale = 0f;
                paused = !paused;
                pause.SetActive(true);
            }
            else
            {
                player.GetComponent<PlayerSpotlight>().enabled = true;
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerShoot>().enabled = true;
                Time.timeScale = 1f;
                paused = !paused;
                pause.SetActive(false);
                Cursor.visible = false;
            }
        }
	}
}
