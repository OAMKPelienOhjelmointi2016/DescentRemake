using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cutscene : MonoBehaviour {
    GameObject player;
    GameObject spotlight;
    GameObject skipText;
    GameObject invisibleWalls;
    GameObject tutorial;
    GameObject crosshair;
    public Camera cockpit;
    public Camera cockpitHUD;
    public Camera camera1;
    public Camera camera2;
    bool cutsceneFinished = false;
    IEnumerator routine;

	// Use this for initialization
	void Start () {
        tutorial = GameObject.Find("Tutorial");
        tutorial.SetActive(false);
        invisibleWalls = GameObject.Find("Invisible_walls");
        invisibleWalls.SetActive(false);
        routine = cutsceneCamera();
        skipText = GameObject.Find("SkipCutscene");
        player = GameObject.Find("Player");
        spotlight = GameObject.Find("Spotlight");
        crosshair = GameObject.Find("Crosshair");
        crosshair.SetActive(false);
        StartCoroutine(routine);
	}

    void Update()
    {
        if(Input.GetKeyDown("space") && !cutsceneFinished)
        {
            StopCoroutine(routine);
            skipText.SetActive(false);
            player.GetComponent<Animator>().enabled = false;
            player.transform.position = new Vector3(-4.6f , -2.65f, 23.67f);
            player.transform.localEulerAngles = new Vector3(0, 133.6401f, 0);
            spotlight.SetActive(false);
            cockpitHUD.enabled = true;
            cockpit.enabled = true;
            camera1.enabled = false;
            camera2.enabled = false;
            cutsceneFinished = true;
            invisibleWalls.SetActive(true);
            print(player.transform.position);
            tutorial.SetActive(true);
            crosshair.SetActive(true);
        }
    }

    IEnumerator cutsceneCamera()
    {
        //Cutscene - part 1
        player.GetComponent<UIController>().enabled = false;
        cockpitHUD.enabled = false;
        cockpit.enabled = false;
        camera1.enabled = true;
        camera2.enabled = false;
        yield return new WaitForSeconds(6);

        //Cutscene - part 2
        cockpit.enabled = false;
        camera1.enabled = false;
        camera2.enabled = true;
        yield return new WaitForSeconds(2);

        //Cutscene - part 3
        spotlight.SetActive(false);
        cockpitHUD.enabled = true;
        cockpit.enabled = true;
        camera1.enabled = false;
        camera2.enabled = false;
        yield return new WaitForSeconds(8);
        player.GetComponent<UIController>().enabled = true;
        player.GetComponent<Animator>().enabled = false;
        invisibleWalls.SetActive(true);
        cutsceneFinished = true;
        skipText.SetActive(false);
        tutorial.SetActive(true);
        crosshair.SetActive(true);
    }
}
