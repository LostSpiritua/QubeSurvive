using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float healthStart = 100;                                         // Enemy's health    
    public float speed = 1.5f;                                              // Enemy's move speed
    public float attackPower = 10.0f;                                       // Enemy's attack power
    public float returnPower = 7.0f;                                        // Power with enemy return from player collision
    public float pointsForKill = 1f;                                        // Points price for enemy kill
    public Gradient healthGradient;                                         // Color gradient for enemy health
                                                                          
    private GameObject playerObject;                                        // pointer to player
    private Player player;                                                  // 
    private Rigidbody enemyRB;                                              // 
    private float health;                                                   // Current value of Health
    private Renderer enemyMat;                                              // 
    private HudUpdate HUD;                                                  // 
    private NavMeshAgent enemyAI;  


    // Start is called before the first frame update
    void Start()
    {
        health = healthStart;
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        enemyRB = GetComponent<Rigidbody>();
        enemyMat = gameObject.GetComponentInChildren<Renderer>();
        HUD = GameObject.Find("MainHUD").GetComponent<HudUpdate>();
        enemyAI = GetComponent<NavMeshAgent>();

        ChangeColorByHealth(health);
    }

    // Reset enemy's to default value after spawning from pool
    private void OnEnable()
    {
        health = healthStart;
        if (enemyMat != null)
        {
            ChangeColorByHealth(health);
        }
    }
    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    // Enemy follow for the player
    public virtual void MoveToPlayer()
    {
      //  if (playerObject.activeSelf)
      //  {
      //
      //      transform.LookAt(playerObject.transform.position);
      //
      //      transform.Translate(Vector3.forward * Time.deltaTime * speed);
      //  }

        if (playerObject.activeSelf)
        {
            enemyAI.speed = speed;
            enemyAI.destination = playerObject.transform.position;
        }
    }   

    // Damage that enemy recieve by collision with player
    public virtual void DamageByCollision()
    {
        health = health - player.playerArmor;
        ChangeColorByHealth(health);
        DisableOnLowHealh();
        HUD.BarUpdate();
    }

    // Damage that enemy recieve by player's weapon
    public virtual void DamageByWeapon()
    {
        health -= player.currentWeapon.attackPower;
        ChangeColorByHealth(health);
        DisableOnLowHealh();
    }

    // Damage that enemy by explosion
    public virtual void DamageByExplosion(GameObject explosion)
    {

        if (explosion.GetComponentInParent<Drop_Bomb>() != null)
        {
            var dropExplosion = explosion.GetComponentInParent<Drop_Bomb>();
            health -= dropExplosion.explosionDamage;
            ChangeColorByHealth(health);
            DisableOnLowHealh();
        }
        else
        {
            Debug.LogWarning("Drop_Bomb Script doesn't attach to GameObject");
        }
    }

    // Some activity at collision
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.HealthDamage(attackPower);
            DamageByCollision();
            enemyRB.AddRelativeForce(Vector3.back * returnPower, ForceMode.Impulse); // Push back enemy out of player at collision
        }
    }

    // Control by player's weapon hit 
    public virtual void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            DamageByWeapon();
        }

        if (other.gameObject.CompareTag("Explosion"))
        {
            DamageByExplosion(other);
        }

     }

    // Disable enemy game object when healt low than 0
    public virtual void DisableOnLowHealh()
    {
        if (health <= 0)
        {
            ScoreUp(pointsForKill);
            KillsUp();
            gameObject.SetActive(false);
        }
    }

    // Change enemy's color according enemy's health value
    public virtual void ChangeColorByHealth(float health)
    {
        var mat = enemyMat.material;
        float normalizedHealth = health / healthStart;
        var color = healthGradient.Evaluate(normalizedHealth);
        mat.SetColor("_BaseColor", color);
    }

    // Add some points to whole score value
    public virtual void ScoreUp (float points)
    {
        GameManager.Instance.scores += points;
        var p = GameManager.Instance.scores;
        HUD.ScoresUpdate(p);
    }

    // Add 1 kill to GM kills counter
    public virtual void KillsUp ()
    {
        GameManager.Instance.totalKills += 1;
        var k = GameManager.Instance.totalKills;
        HUD.KillsUpdate(k);
    }
}