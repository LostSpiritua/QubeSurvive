using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100; // Enemy's health    
    public float speed = 1.5f; // Enemy's move speed
    public float attackPower = 25.0f; // Enemy's attack power
    public float returnPower = 2.0f; // Power with enemy return from player collision

    private GameObject playerObject;
    private Player player;
    private Rigidbody enemyRB;
    
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        enemyRB = GetComponent<Rigidbody>();
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
        float attack = - player.playerArmor + attackPower;

        if (attack > 0)
        {
            player.playerHealth -= attack;
        }

        player.playerArmor -= attackPower;

        if (player.playerArmor < 0)
        {
            player.playerArmor= 0;
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
        
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
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
}