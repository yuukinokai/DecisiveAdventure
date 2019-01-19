using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMonkey : Checkpoint{

    /*override protected void Event0(){
		if(isActive){
            EventManager.TriggerEvent("Giving");
            base.Event0();
		}
	} */
    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("GiveBanana", GiveBanana);
    }

    void GiveBanana()
    {
        hero.PartyJoin((Monkey) Monkey.CreateInstance("Monkey"));
        EventManager.TriggerEvent("MonkeyJoin");
        hero.RemoveItem("Banana");
    }

    override protected void Event1(){
		if(isActive){
			if(hero == null){
				Debug.Log("No Hero");
				return;
			}
			hero.AddItem("Meat");
        	//destroy the Monkey
			base.Event1();
		}
	} 

}
