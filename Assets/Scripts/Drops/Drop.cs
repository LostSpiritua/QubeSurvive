using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Drop : MonoBehaviour
{
    public float lifeTime = 10.0f;                 // Drop's life time after spawning
    public float workTimer = 1f;                   // Default time for drop's effect
    public float rotationSpeed = 270.0f;           // Drop's animation speed
    public ParticleSystem effectVFX;               // Drop's VFX after activation
    public string soundName;

    protected bool corutineWork = false;           // Check to not deactivate drop after life time over

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public virtual void OnEnable()
    {
        StartCoroutine(ActiveTimer(lifeTime));
    }


    // Update is called once per frame
    public virtual void Update()
    {
        transform.GetChild(0).transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }

    // Time of life bonus after spawning
    public virtual IEnumerator ActiveTimer(float time)
    {
        yield return new WaitForSeconds(time);

        if (!corutineWork) 
        { 
            gameObject.SetActive(false);
        }
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (transform.position.z + 0.01f));
        }
    }


    // Some action when player triger drops collider
    public virtual void OnTriggerEnter(Collider other)
    {
        
       
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.Play(soundName, transform.position, 0, 0);
            corutineWork = true;                                            // Stop life time countdown
            var Player = other.transform.parent.transform.parent.transform;             
            NewParentForVFX(Player);                                        // Move drop VFX to Player position

            StartCoroutine(DropBonusTimer(workTimer));                      // Start drops bonus timer

            gameObject.transform.GetChild(0).gameObject.SetActive(false);   // Deactivate drop's mesh
            gameObject.GetComponent<Collider>().enabled = false;            // Deactivate drop's collider
        }
    }

    public virtual IEnumerator DropBonusTimer(float time)
    {
        DropBonusWork();                                                    // Some drop's action after activation

        effectVFX.Play();                                                   // Start drop's VFX

        yield return new WaitForSeconds(time);
       
        DropBonusAfterWork();                                               // Some drop's action after end of bonus timer
       
        effectVFX.Stop();                                                   // Stop drop's vfx
        effectVFX.Clear();                                                  //
        NewParentForVFX(gameObject.transform);                              // Return drop's vfx to drop's gameobject
        corutineWork = false;
        gameObject.SetActive(false);                                        // Disable drop

    }

    public abstract void DropBonusWork(); 
    // Some drop's action after activation

    // Some drop's action after end of bonus timer
    public abstract void DropBonusAfterWork(); 
    
    // Make drop VFX child of Other gameobject
    public virtual void NewParentForVFX(Transform other)
    {
        effectVFX.transform.parent = other;
        effectVFX.transform.position = other.position;
        effectVFX.transform.rotation = Quaternion.Euler(-90, 0, 0);
    }
}   

