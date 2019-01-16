using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : Checkpoint {

	private bool leave = false;
	override protected void Event3(){
		base.Event3();
		EventManager.TriggerEvent ("GameOver");
		Vector3 theScale = rigidBody2D.transform.localScale;
		theScale.x *= -1;
		rigidBody2D.transform.localScale = theScale;
		leave = true;
	} 

	void Update(){
		if(leave){
			Vector3 targetVelocity = new Vector2(-1 * 10f, rigidBody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			rigidBody2D.velocity = Vector3.SmoothDamp(rigidBody2D.velocity, targetVelocity, ref _Velocity, 0.5f);
		}
	}
}
