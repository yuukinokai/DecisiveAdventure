using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	protected GameObject player;
	protected Rigidbody2D rigidBody2D;
	protected Vector3 _Velocity = Vector3.zero;
	protected DialogueController dialogController;
	protected Player hero;

	[SerializeField] protected Dialog _Dialog;
	[SerializeField] protected bool isActive = false;
	[SerializeField] protected bool overriden = false;

   	virtual protected void OnTriggerEnter2D(Collider2D other)
    {
      	if (other.gameObject.CompareTag("Player")) {
			isActive = true;
        	EventManager.TriggerEvent ("Checkpoint");
	 		Debug.Log("check");
			if(_Dialog != null && dialogController != null){
				//Debug.Log("Check Shoud Replave");
				if(ShouldReplace()) {
					dialogController.OverrideDialog(_Dialog);
					//Debug.Log("Shoud Replave");

				}
			}
			EventManager.TriggerEvent ("DoneCheckpoint");
		}
    }

	protected void Start(){
		player = GameObject.Find("Player");
		if(player == null) return;
		rigidBody2D = player.GetComponent<Rigidbody2D>();
		dialogController = DialogueController.GetController();
		hero = Player.GetPlayer();
		if(hero == null) Debug.Log("Hero Not Set");
	}

    protected void OnEnable ()
    {
        EventManager.StartListening ("Button0", Event0);
		EventManager.StartListening ("Button1", Event1);
		EventManager.StartListening ("Button2", Event2);
		EventManager.StartListening ("Button3", Event3);
    }

	virtual protected bool ShouldReplace(){
		return false;
	}

	virtual protected void Event0(){
		if(isActive){
			Debug.Log ("Button 0!");
			isActive = false;
		}
	}
	virtual protected void Event1(){
		if(isActive){
			Debug.Log ("Button 1!");
			isActive = false;
		}
	}
	virtual protected void Event2(){
		if(isActive){
			Debug.Log ("Button 2!");
			isActive = false;
		}
	}
	virtual protected void Event3(){
		if(isActive){
			Debug.Log ("Button 3!");
			isActive = false;
		}
	}
}
