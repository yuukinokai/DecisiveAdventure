using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBanana : Checkpoint {

	override protected void Event1(){
		if(isActive){
			if(hero == null){
				Debug.Log("No Hero");
				return;
			}
			hero.AddItem("Banana");
        	//destroy the banana
			base.Event1();
		}
	} 
}
