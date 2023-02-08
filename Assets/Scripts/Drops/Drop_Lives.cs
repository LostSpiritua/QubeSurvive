using UnityEngine;

public class Drop_Lives : Drop
{
    private HudUpdate HUD;
    private LvlControl C;

    // Some overide to standart method. If lives is Full to do nothing.
    public override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.Lives < 2)
            {
                base.OnTriggerEnter(other);
            }
        }  
    }
    
    public override void Start()
    {
        HUD = GameObject.Find("MainHUD").GetComponent<HudUpdate>();
        C = GameObject.Find("LvlControl").GetComponent<LvlControl>();
    }

    // Add 1 live and Update HUD
    public override void DropBonusWork()
    {
        C.LivesAction(1);
        HUD.LivesHUDUpdate();
    }

    // After nothing to do
    public override void DropBonusAfterWork()
    {
        return;
    }
}
