using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBigBoy : Checkpoint
{
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("GiveBall", GiveRabbitPoo);
    }

    void GiveRabbitPoo()
    {
        dialogController.Notice("“A ball is all I ever wanted! You can have this piece of rusty sword and my snack money.”");
        hero.RemoveItem("Ball");
        hero.AddCoin(1);
        hero.AddItem("RustySword");
    }
}
