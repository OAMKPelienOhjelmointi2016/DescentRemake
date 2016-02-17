using UnityEngine;
using System.Collections;

/// <summary>
///  Script for health and shield pick ups. This script is attached to the pick ups, and the
///  pick up type is selected from the editor using the pickupType-variable. The player cannot pick these up
///  if his health/shield stats are full. The pick ups dissappear if the player collides with them when not at full
///  health/shield. 
/// 
/// The player must have a collider.
/// 
/// </summary>

public class HealthShieldPickUp : MonoBehaviour {

    public int pickupType; // 1 = Health pick up, 2 = Shield pick up
    public int healamount; // The amount of health that is gained from the pickup
    public int shieldamount; // The amount of shield that is gained from the pickup
    bool picked;

    void Start()
    {
        
    }

    void Update()
    {       

    }
        
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (pickupType == 1)
            {
                GameObject Player = GameObject.Find("PlayerPrefab");
                HealthShield hsclass = Player.GetComponent<HealthShield>();
                
                if (hsclass.health < hsclass.maxHealth)
                {

                    gameObject.SetActive(false);
                    
                    hsclass.health += healamount;
                    if (hsclass.health >= hsclass.maxHealth)
                    {
                        hsclass.health = hsclass.maxHealth;
                    }           
                   
                }
            }

            else if(pickupType == 2)
            {
                GameObject Player = GameObject.Find("PlayerPrefab");
                HealthShield hsclass = Player.GetComponent<HealthShield>();

                if (hsclass.shield < hsclass.maxShield)
                {

                    Destroy(gameObject);

                    hsclass.shield += shieldamount;
                    if (hsclass.shield >= hsclass.maxShield)
                    {
                        hsclass.shield = hsclass.maxShield;
                    }
                    
                }
            }
        }
    }

    
}
