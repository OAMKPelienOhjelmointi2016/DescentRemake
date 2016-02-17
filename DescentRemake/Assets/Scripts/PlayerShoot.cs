using UnityEngine;
using System.Collections;

/*  Reads player's inputs and uses class "FiringWeapons" to initiate shooting and item usage.
    Needs inputs:
    "Fire1" to shoot bullets
    "Fire2" to shoot missiles
    "Item" to shoot flares*/

public class PlayerShoot : MonoBehaviour {

    public int bulletCounter = 0;
    [SerializeField]
    private float FireRateForPrimaryFire;
    [SerializeField]
    private float FireRateForAltFire;
    [SerializeField]
    private float FireRateForItems;
    private float nextbullet;
    private float nextmissile;
    private float nextitem;

    //Firemode can be "standard" , "triple" & "auto"
    [SerializeField]
    private string firemode;
    //Item name can be "flare" , "emp" & "decoy"
    [SerializeField]
    private string NameOfEquippedAltWeapon;
    //Item name can be "flare" , "emp" & "decoy"
    [SerializeField]
    private string NameOfEquippedItem;

    private FiringWeapons weapons;
    //private UnityEngine.UI.Image bulletOvImage;
    //private UnityEngine.UI.Image missileOvImage;
    //private UnityEngine.UI.Image itemOvImage;

    // Use this for initialization
    void Start () {
        weapons = this.GetComponent<FiringWeapons>();
        //bulletOvImage = GameObject.Find("BulletOverlay").GetComponent<UnityEngine.UI.Image>();
        //missileOvImage = GameObject.Find("MissileOverlay").GetComponent<UnityEngine.UI.Image>();
        //itemOvImage = GameObject.Find("ItemOverlay").GetComponent<UnityEngine.UI.Image>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > nextbullet)
            {
                weapons.InitiateStandardShoot(FireRateForPrimaryFire, firemode);
                nextbullet = Time.time + FireRateForPrimaryFire;
                bulletCounter++;
            }
        }else if (Input.GetButtonUp("Fire1") && firemode == "auto")
        {
            weapons.DisableAutofire();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            if (Time.time > nextmissile)
            {
                weapons.InitiateAlternativeShoot(FireRateForAltFire);
                nextmissile = Time.time + FireRateForAltFire;
            }
        }
        else if (Input.GetButtonDown("Item"))
        {
            if (Time.time > nextitem) {
                weapons.InitiateItemActivation(NameOfEquippedItem, FireRateForItems);
                nextitem = Time.time + FireRateForItems;
            }
        }
        /*if(bulletOvImage.rectTransform.localScale.y > 0)
        {
            bulletOvImage.rectTransform.localScale = new Vector3(1, Mathf.Lerp(1, 0, firerate), 1);
        }*/
    }
}
