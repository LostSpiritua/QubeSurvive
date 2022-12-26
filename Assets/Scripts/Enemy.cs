using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float healthStart = 100; // Enemy's health    
    public float speed = 1.5f; // Enemy's move speed
    public float attackPower = 10.0f; // Enemy's attack power
    public float returnPower = 7.0f; // Power with enemy return from player collision

    private GameObject playerObject;
    private Player player;
    private Rigidbody enemyRB;
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        health = healthStart;
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        enemyRB = GetComponent<Rigidbody>();

    }

    private void OnEnable()
    {
        health = healthStart;
    }
    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    // Enemy follow for the player
    private void MoveToPlayer()
    {
        if (playerObject.activeSelf)
        {

            transform.LookAt(playerObject.transform.position);

            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    // Some calculation on enemy's collision with player
    private void Attack()
    {
        float attack = -player.playerArmor + attackPower;

        if (attack > 0)
        {
            player.playerHealth -= attack;
        }

        player.playerArmor -= attackPower;

        if (player.playerArmor < 0)
        {
            player.playerArmor = 0;
        }

        if (player.playerHealth <= 0)
        {
            playerObject.SetActive(false);
        }
    }

    // Damage that enemy recieve by collision with player
    public void DamageByCollision()
    {
        health = health - player.playerArmor;
        DisableOnLowHealh();
    }

    // Damage that enemy recieve by player's weapon
    public void DamageByWeapon()
    {
        health -= player.currentWeapon.attackPower;
        DisableOnLowHealh();
    }

    // Some activity at collision
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Attack();
            DamageByCollision();
            enemyRB.AddRelativeForce(Vector3.back * returnPower, ForceMode.Impulse); // Push back enemy out of player at collision
        }
    }

    // Control by player's weapon hit 
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {

            DamageByWeapon();
        }
    }

    // Disable enemy game object when healt low than 0
    private void DisableOnLowHealh()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}