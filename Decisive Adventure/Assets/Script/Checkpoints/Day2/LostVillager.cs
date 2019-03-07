using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostVillager : Checkpoint
{

    override protected void Event0()
    {
        if (isActive)
        {
            if (hero == null)
            {
                Debug.Log("No Hero");
                return;
            }
            hero.AddCoin(2);
            base.Event0();
        }
    }

}

