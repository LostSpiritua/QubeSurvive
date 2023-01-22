using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Weapon : Drop
{
    public List<GameObject> weaponsDrop; // List of weapon drops game objects that are children of it

    [ColorUsageAttribute(true, true)]
    public List<Color> sphereColor;   // List of colors for different weapons drop
    public int WeaponIndex;           // Weapon index for random calling from list
    public float speedUp;             // Speed of moving drops Up to camera after pick up
    public GameObject sphere;         // Point to Sphere gameobject weapon drop

    private Player Player;            // Access to player script    
    private bool goUp = false;        // Check for moving to camera  
    private Material sphereMat;       // Pointer to sphere material

    public override void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
    }

    public override void OnEnable()
    {
        corutineWork = false;
        
        base.OnEnable();

        goUp = false;                                           
        WeaponIndex = Random.Range(0, weaponsDrop.Count);   // Set random weapon from list

        foreach (GameObject weapon in weaponsDrop)          // Deactivate all weapon except from random 
        {                                                   //
            weapon.SetActive(false);                        //  
        }

        weaponsDrop[WeaponIndex].SetActive(true);           // Deactivate all weapons except random one choose below

        sphereMat = sphere.GetComponent<Renderer>().material;    // Set own color for every weapons
        sphereMat.SetColor("_Color", sphereColor[WeaponIndex]);  //
    }

    public override void Update()
    {
        MoveDropToCameraUp();                                     
    }                                                                                            

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            corutineWork = true;                                        // Stop life time countdown
            StartCoroutine(DropBonusTimer(workTimer));                  // Start drops bonus timer

        }
    }

    public override IEnumerator DropBonusTimer(float time)
    {
        DropBonusWork(); // Some drop's action after activation

        yield return new WaitForSeconds(time);

        DropBonusAfterWork();  // Some drop's action after end of bonus timer
                
        gameObject.SetActive(false); // Disable drop
    }


    public override void DropBonusWork()
    {
        Player.SetPlayerWeapon(WeaponIndex);                            // Activate weapon in player gameobj

        goUp = true;
        corutineWork = false;
    }

    // Do nothing at that time
    public override void DropBonusAfterWork()
    {
        return;
    }

    // Move weapon drop to camera - should change to animation in future
    private void MoveDropToCameraUp()
    {
        if (goUp)                                                                                
        {                                                                                        
            transform.Translate(Camera.main.transform.position * Time.deltaTime * speedUp);      
                                                                                                 
            if (transform.position.y > 24f) { goUp = false; }                                    
        }                                                                                        
    }

}
