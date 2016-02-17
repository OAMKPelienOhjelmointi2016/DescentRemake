using UnityEngine;
using System.Collections;

public class TrackFiringScript : MonoBehaviour {

    private PlayerShoot playerShoot;
    private StatController statController;
    public int bulletCountOld = 0;
    public int bulletCount = 0;
    private bool playerFound = false;


	void Start ()
    {
	}

    public void FindPlayer()
    {
        playerShoot = GetComponent<PlayerShoot>();
        statController = GetComponent<StatController>();
        playerFound = true;

        
    }

    void Update()
    {
        if (playerFound == true)
        {
            bulletCount = playerShoot.bulletCounter;
            if (bulletCount != bulletCountOld)
            {
                statController.SendAmmo();
                bulletCountOld = playerShoot.bulletCounter;
            }
        }
    }
}
