using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Bomb : Drop
{
    public float explosionDamage = 100f;  // Damage to collider enemy

    public override void DropBonusWork()
    {
        return;
    }

    public override void DropBonusAfterWork()
    {
        return;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.Instance.Play(soundName, transform.position, 0, 0);
            corutineWork = true;                                        // Stop life time countdown
            
            StartCoroutine(DropBonusTimer(workTimer));                  // Start drops bonus timer

            gameObject.transform.GetChild(0).gameObject.SetActive(false);  // Deactivate drop's mesh
            gameObject.GetComponent<Collider>().enabled = false;  // Deactivate drop's collider
        }
    }

    public override IEnumerator DropBonusTimer(float time)
    {
        DropBonusWork(); // Some drop's action after activation

        effectVFX.Play();  // Start drop's VFX

        yield return new WaitForSeconds(time);

        DropBonusAfterWork();  // Some drop's action after end of bonus timer

        effectVFX.Stop();  // Stop drop's vfx
        effectVFX.Clear(); //
        gameObject.SetActive(false); // Disable drop
    }
}
