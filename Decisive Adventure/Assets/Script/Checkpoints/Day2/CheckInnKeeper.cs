using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInnKeeper : Checkpoint
{

    override protected bool ShouldReplace()
    {
        if (hero == null)
        {
            Debug.Log("No Hero");
            return false;
        }
        if (hero.GetCoin() < 1)
        {
            overriden = true;
            return true;
        }
        return false;
    }

    override protected void Event1()
    {
        if (overriden == false && isActive)
        {
            hero.LoseCoin(1);
            base.Event0();
        }
    }
}
