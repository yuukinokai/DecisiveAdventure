using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBanana : Checkpoint {
	[SerializeField] private GameObject banana;

	override protected void Event0(){
		if(isActive){
			if(hero == null){
				Debug.Log("No Hero");
				return;
			}
			hero.AddItem("Banana");
        	Destroy(banana);
			base.Event0();
		}
	} 
}
