using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckVillager1 : Checkpoint {
   
   	override protected void Event0(){
		if(isActive){
			if(hero == null){
				Debug.Log("No Hero");
				return;
			}
			hero.AddItem("Straw");
			base.Event0();
		}
	} 

}
