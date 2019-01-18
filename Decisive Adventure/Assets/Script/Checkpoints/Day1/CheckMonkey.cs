using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMonkey : Checkpoint{
    
	override protected void Event0(){
		if(isActive){
			//open inventory
			base.Event0();
		}
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
