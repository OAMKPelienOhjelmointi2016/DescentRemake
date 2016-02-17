using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthShield : MonoBehaviour
{

    public int health;
    public int maxHealth = 100;
    public int shield;
    public int maxShield = 100;
    private float dTime = 0;
    private float nextTimeStep = 0;
    public float TimeBeforeShieldRegen;
    private bool shieldRegenIsEnabled = false;
    public int remainingDmg;
    public Text shieldText;
    public Text healthText;
    public TextMesh stext;
    public TextMesh htext;

    // Use this for initialization
    void Start()
    {
        /*health = maxHealth;
        shield = maxShield;*/
  
    }

    // Update is called once per frame
    void Update()
    {
        dTime += Time.deltaTime;

        if (shieldRegenIsEnabled)
        {
            shieldRegen();
            if (shield > maxShield)
            {
                shieldRegenIsEnabled = false;
            }
        }

        if (health <- 0)
        {
            health = 0;
        }

		if (shieldText != null && healthText != null)
        {
            shieldText.text = "Shield: " + shield;
            healthText.text = "Health: " + health;
        }
		if (htext != null && stext != null) {
			htext.text = "HULL: " + health;
			stext.text = "SHIELD: " + shield;
		}
    }

    // Heal function
    public void heal(int healDone)
    {
        health += healDone;

        if (health >= 100)
        {
            health = maxHealth;
        }

        Debug.Log("Health: " + health);
        Debug.Log("Healing done: " + healDone);
    }

    // Shield regen function
    public void shieldRegen()
    {
        if (dTime > nextTimeStep)
        {
            shield++;
            nextTimeStep += 0.25f;
        }
        
        if (shield > maxShield)
        {
            shield = maxShield;
        }
    }

    // Call when taking damage
    [PunRPC]
    public void takeDmg(int dmgTaken)
    {
        shieldRegenIsEnabled = true;
        nextTimeStep = dTime + TimeBeforeShieldRegen; 

        if (shield <= 0)
        {
            remainingDmg = dmgTaken - shield;
            health -= remainingDmg;
            remainingDmg = 0;
        }

        if (shield == dmgTaken)
        {
            shield = 0;
        }

        if (shield > dmgTaken)
        {
            shield -= dmgTaken;
        }

        else if (shield < dmgTaken && shield > 0)
        {
            remainingDmg = dmgTaken - shield;
            health -= remainingDmg;
            shield = 0;
            remainingDmg = 0;
        }

        Debug.Log("Health: " + health);
        Debug.Log("Shield: " + shield);
        Debug.Log("Remaining Damage: " + remainingDmg);
    }
}