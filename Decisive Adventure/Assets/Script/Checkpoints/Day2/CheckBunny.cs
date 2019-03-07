using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBunny : Checkpoint
{
    [SerializeField] private GameObject bunny;
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("GiveStraw", GiveStrawToBunny);
    }

    void GiveStrawToBunny()
    {
        dialogController.Notice("The bunny ate the dried grass and left some feces and a gold coin. Perhaps it took you for some restaurant.");
        hero.RemoveItem("Straw");
        hero.AddCoin(1);
        hero.AddItem("RabbitPoo");
    }

    override protected void Event1()
    {
        if (isActive)
        {
            if (hero == null)
            {
                Debug.Log("No Hero");
                return;
            }
            hero.AddItem("Meat");
            Destroy(bunny);
            base.Event1();
        }
    }

}
