using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Lives : Drop
{
    private HudUpdate HUD;

    // Some overide to standart method. If lives is Full to do nothing.
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Lives < 2)
            {
                base.OnTriggerEnter(other);
            }
            else
            {
                gameObject.SetActive(false);
            }
        } 
    }

    public override void Start()
    {
        HUD = GameObject.Find("MainHUD").GetComponent<HudUpdate>();
        base.Start();
    }

    // Add 1 live and Update HUD
    public override void DropBonusWork()
    {
        GameManager.Instance.LivesAction(1);
        HUD.LivesHUDUpdate();
    }

    // After nothing to do
    public override void DropBonusAfterWork()
    {
        return;
    }
}
