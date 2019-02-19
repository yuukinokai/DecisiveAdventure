using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSmallBoy : Checkpoint
{
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("GiveRabbitPoo", GiveRabbitPoo);
    }

    void GiveRabbitPoo()
    {
        dialogController.Notice("“Wow is that chocolate? Thank you, you can have my ball and this shiny coin.”");
        hero.RemoveItem("RabbitPoo");
        hero.AddCoin(1);
        hero.AddItem("Ball");
    }
}
