using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHungry : Checkpoint {

	override protected bool ShouldReplace(){
		if(hero == null){
			Debug.Log("No Hero");
			return false;
		}
		if(hero.HasItem("Banana")){
			overriden = true;
			return true;
		}
		return false;
	}

	override protected void Event0(){
		if(overriden == true && isActive){
			hero.RemoveItem("Banana");
			base.Event0();
		}
	} 
}
